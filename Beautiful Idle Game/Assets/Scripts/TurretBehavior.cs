using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    [Header("Turret Attribute")]
    public float damage;
    public int range;
    public int type; // Type of turret, the same as index of the prefab list atm.
    public float price;
    public OverlayTile mountLocation;
    
    private int level;
    [SerializeField] private bool slowEffect;
    private List<OverlayTile> tilesInRange;
    [SerializeField] private OverlayTile target;    // Testing
    [SerializeField] private float turretCoolDown;
    private float turretHeat;

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
                WeaponAttack();
            }
        }
        else
        {
            target = FindTarget();
            // if (animator.isPlaying()) {Stop playing animation}
        }
    }

    public void WeaponBuild(OverlayTile overlayTile)
    {
        transform.position = new Vector3(overlayTile.transform.position.x,
                                         overlayTile.transform.position.y + 0.0001f,
                                         overlayTile.transform.position.z);
        GetComponent<SpriteRenderer>().sortingOrder = overlayTile.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
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
