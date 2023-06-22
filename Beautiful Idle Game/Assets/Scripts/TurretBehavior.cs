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
    [SerializeField] private float turretCoolDown;
    [SerializeField] private float turretHeat;

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
            turretHeat = turretCoolDown;
        }        
    }
}
