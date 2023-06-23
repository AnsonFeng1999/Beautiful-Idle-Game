using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayTile : MonoBehaviour
{
    [Header("Manhatten Attribute")]
    public int G;
    public int H;
    
    public int F
    {
        get { return G + H; }
    }

    [Header("Tile Attribute")]
    public bool isBlocked;
    public bool enemyOn;
    public bool beingShot;
    public float damageOnThisTile;
    public OverlayTile previous;
    public Vector3Int gridLocation;
    public bool shouldSlowed;

    [Header("Reference")]
    public TurretBehavior turret;

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        if (!enemyOn) { 
            beingShot = false;
            shouldSlowed = false;
        }
    }
}
