using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    [Header("Turret Attribute")]
    public float damage;
    public int range;
    public int level;
    public int type; // Type of turret, the same as index of the prefab list atm.
    public float price;
    public OverlayTile mountLocation;
    private List<OverlayTile> tilesInRange;
    [SerializeField] private OverlayTile target;
    private float turretCoolDown;

    void Start()
    {
        Vector2Int mountLocaVec = new(mountLocation.gridLocation.x, mountLocation.gridLocation.y);
        tilesInRange = MapManager.Instance.GetTilesInRange(mountLocaVec, range);
        foreach (var tile in tilesInRange) { tile.damageOnThisTile += damage; }
        turretCoolDown = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (turretCoolDown > 0f)
        {
            turretCoolDown -= Time.deltaTime;
        }

        if (target)
        {
            if (!target.enemyOn)
            {
                target.beingShot = false;
                target = null;
            }
            else if (turretCoolDown <= 0)
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
        if (turretCoolDown <= 0f)
        {
            target.beingShot = true;
            turretCoolDown = 1f;
        }        
    }
}
