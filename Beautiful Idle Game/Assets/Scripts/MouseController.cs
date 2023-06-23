using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.Tilemaps;

public class MouseController : MonoBehaviour
{
    [Header("Reference")]
    public GameObject cursor;
    [SerializeField] public OverlayTile overlayTile;
    [SerializeField] private MapManager mapManager;
    [SerializeField] private WeaponBuildManager weaponBuildManager;

    [Header("Building Management")]
    public Boolean isMounting;
    public int towerIndex;

    void Start()
    {
        mapManager = MapManager.Instance;
        weaponBuildManager = GetComponent<WeaponBuildManager>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        RaycastHit2D? hit = GetFocusedOnTile();

        if (hit.HasValue)
        {
            overlayTile = hit.Value.collider.gameObject.GetComponent<OverlayTile>();
            cursor.transform.position = overlayTile.transform.position;
            cursor.GetComponent<SpriteRenderer>().sortingOrder = overlayTile.GetComponent<SpriteRenderer>().sortingOrder;
            
            #region Mounting
            
            if (isMounting)
            {
                TurretRangeShow(overlayTile, towerIndex, 1);
            }
            
            if (isMounting && Input.GetMouseButtonDown(0))
            {
                weaponBuildManager.MountWeapons(towerIndex, overlayTile);
                isMounting = false;
            }

            #endregion

            #region Hovering
            
            if (overlayTile.turret)
            {
                TurretRangeShow(overlayTile, overlayTile.turret.type, 1);
            }
            
            #endregion
        }
    }

    public RaycastHit2D? GetFocusedOnTile()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2d = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2d, Vector2.zero)
                                       .Where(i => i.collider.gameObject.GetComponent<SpriteRenderer>() != null)
                                       .ToArray();

        if(hits.Length > 0)
        {
            return hits.Where(i => i.collider.gameObject.GetComponent<SpriteRenderer>() != null)
                       .OrderByDescending(i => i.collider.transform.position.z).First();
        }

        return null;
    }

    private void TurretRangeShow(OverlayTile overlayTile, int towerIndex, float alpha)
    {
        var currentTurret = weaponBuildManager.weaponPrefabs[towerIndex].GetComponent<TurretBehavior>();
        Vector2Int currGrid = new(overlayTile.gridLocation.x, overlayTile.gridLocation.y);
        foreach (OverlayTile tile in mapManager.GetTilesInRange(currGrid, currentTurret.range))
        {
            tile.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
        }
    }

    public void SetIsMountingAndIndex(int _index)
    {
        isMounting = true;
        towerIndex = _index;
    }
}