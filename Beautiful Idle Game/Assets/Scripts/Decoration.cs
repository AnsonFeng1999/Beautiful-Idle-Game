using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Decoration : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col)
    {
        var overlayTile = col.gameObject.GetComponent<OverlayTile>();
        // if game object has an overlay tile destory it
        if (overlayTile)
        {
            var key = MapManager.Instance.map.FirstOrDefault(x => x.Value == overlayTile).Key;
            MapManager.Instance.map.Remove(key);
            Destroy(col.gameObject);
        }
    }
}