using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    static readonly string[] shootDirections = {"Run N", "Run NW", "Run W", "Run SW", "Run S", "Run SE", "Run E", "Run NE"};
    
    [Header("Turret Attribute")]
    public float damage;
    public int range;
    public int type; // Type of turret, the same as index of the prefab list atm.
    public float price;
    public OverlayTile mountLocation;
    Animator animator;
    
    private int level;
    [SerializeField] private bool slowEffect;
    private List<OverlayTile> tilesInRange;
    [SerializeField] private OverlayTile target;    // Testing
    [SerializeField] private float turretCoolDown;
    private float turretHeat;
    
    private void Awake()
    {
        //cache the animator component
        animator = GetComponentInChildren<Animator>();
    }
    
    void Start()
    {
        Vector2Int mountLocaVec = new(mountLocation.gridLocation.x, mountLocation.gridLocation.y);
        tilesInRange = MapManager.Instance.GetTilesInRange(mountLocaVec, range);
        turretHeat = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (turretHeat > 0f)
        {
            turretHeat -= Time.deltaTime;
        }

        if (target)
        {
            if (!target.enemyOn)
            {
                target.beingShot = false;
                target.shouldSlowed = false;
                target = null;
            }
            else if (turretHeat <= 0)
            {
                // Play firing animation if not playing
                // face towards the tile
                var direction =  target.gridLocation - mountLocation.gridLocation;
                SetDirection(new Vector2(direction.x, direction.y));
                WeaponAttack();
            }
        }
        else
        {
            target = FindTarget();
            // if (animator.isPlaying()) {Stop playing animation}
        }
    }
    
    public void SetDirection(Vector2 direction) {
        //measure the magnitude of the input.
        string[] directionArray = shootDirections;
        int lastDirection = DirectionToIndex(direction, 8);
        //tell the animator to play the requested state
        animator.Play(directionArray[lastDirection]);
    } 
    
    //helper functions

    //this function converts a Vector2 direction to an index to a slice around a circle
    //this goes in a counter-clockwise direction.
    public static int DirectionToIndex(Vector2 dir, int sliceCount){
        //get the normalized direction
        Vector2 normDir = dir.normalized;
        //calculate how many degrees one slice is
        float step = 360f / sliceCount;
        //calculate how many degress half a slice is.
        //we need this to offset the pie, so that the North (UP) slice is aligned in the center
        float halfstep = step / 2;
        //get the angle from -180 to 180 of the direction vector relative to the Up vector.
        //this will return the angle between dir and North.
        float angle = Vector2.SignedAngle(Vector2.up, normDir);
        //add the halfslice offset
        angle += halfstep;
        //if angle is negative, then let's make it positive by adding 360 to wrap it around.
        if (angle < 0){
            angle += 360;
        }
        //calculate the amount of steps required to reach this angle
        float stepCount = angle / step;
        //round it, and we have the answer!
        return Mathf.FloorToInt(stepCount);
    }

    public void WeaponBuild(OverlayTile overlayTile)
    {
        transform.position = new Vector3(overlayTile.transform.position.x,
                                         overlayTile.transform.position.y + 0.0001f,
                                         overlayTile.transform.position.z);
        GetComponentInChildren<SpriteRenderer>().sortingOrder = overlayTile.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
        mountLocation = overlayTile;
        overlayTile.isBlocked = true;
        overlayTile.turret = GetComponent<TurretBehavior>();
    }

    public void WeaponRemove(OverlayTile overlayTile)
    {
        overlayTile.isBlocked = false;
        overlayTile.turret = null;
    }

    private OverlayTile FindTarget()
    {
        foreach (var tile in tilesInRange)
        {
            if (tile.enemyOn) { return tile;}
        }
        return null;
    }

    private void WeaponAttack()
    {
        if (turretHeat <= 0f)
        {
            target.damageOnThisTile += damage;
            target.beingShot = true;
            target.shouldSlowed = slowEffect;
            turretHeat = turretCoolDown;
            
        }
    }
}