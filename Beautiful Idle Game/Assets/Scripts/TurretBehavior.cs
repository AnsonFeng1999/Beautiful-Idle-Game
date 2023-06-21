using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    [Header("Turret Attribute")]
    public int strength;
    public int range;
    public int level;
    public int type; // Type of turret, the same as index of the prefab list atm.
    public float price;
    public OverlayTile mountLocation;

    private List<OverlayTile> tilesInRange;

    //// Start is called before the first frame update
    //void Start()
    //{

    //}
    void Awake()
    {
        Vector2Int mountLocaVec = new(mountLocation.gridLocation.x, mountLocation.gridLocation.y);
        tilesInRange = MapManager.Instance.GetTilesInRange(mountLocaVec, range);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
