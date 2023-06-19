using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MouseController : MonoBehaviour
{
    public GameObject cursor;
    public Boolean isMounting;
    public int towerIndex;
    
    // Start is called before the first frame update
    // Update is called once per frame
    void LateUpdate()
    {
        RaycastHit2D? hit = GetFocusedOnTile();

        if (hit.HasValue)
        {
            GameObject overlayTile = hit.Value.collider.gameObject;
            cursor.transform.position = overlayTile.transform.position;
            cursor.GetComponent<SpriteRenderer>().sortingOrder = overlayTile.GetComponent<SpriteRenderer>().sortingOrder;

            if (Input.GetMouseButtonDown(0))
            {
                overlayTile.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                if (isMounting)
                {
                    GetComponent<WeaponBuildManager>().MountWeapons(towerIndex, overlayTile);
                }
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