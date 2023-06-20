using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MouseController : MonoBehaviour
{
    [Header("Reference")]
    public GameObject cursor;
    [SerializeField] public OverlayTile overlayTile;

    [Header("Building Management")]
    public Boolean isMounting;
    public int towerIndex;
    
    // Update is called once per frame
    void LateUpdate()
    {
        RaycastHit2D? hit = GetFocusedOnTile();

        if (hit.HasValue)
        {
            overlayTile = hit.Value.collider.gameObject.GetComponent<OverlayTile>();
            cursor.transform.position = overlayTile.transform.position;
            cursor.GetComponent<SpriteRenderer>().sortingOrder = overlayTile.GetComponent<SpriteRenderer>().sortingOrder;
            
            if (isMounting)
            {
                // show range of turrets
            }
            
            if (isMounting && Input.GetMouseButtonDown(0))
            {
                GetComponent<WeaponBuildManager>().MountWeapons(towerIndex, overlayTile);
            }
            else if (Input.GetMouseButtonDown(0))
            {
                overlayTile.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
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
}