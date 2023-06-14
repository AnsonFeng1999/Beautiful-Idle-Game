using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileHover : MonoBehaviour
{
    [SerializeField]
    private TileBase hoverTile;  // The tile to change to when the mouse hovers over
    [SerializeField]
    private Tilemap tilemap;    // Reference to the tilemap component
    private TileBase previousTile;
    private Vector3Int previousMousePos;

    private void Update()
    {
        // Cast a ray from the mouse position to the worl
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPosition = tilemap.WorldToCell(pos);
        gridPosition.z = 0;
        if (tilemap.HasTile(gridPosition) && !pos.Equals(previousMousePos))
        {
            if (previousTile)
            {
                tilemap.SetTile(previousMousePos, previousTile); // Remove previous tile
            }
            previousTile = tilemap.GetTile(gridPosition); // save current previous tile
            tilemap.SetTile(gridPosition, hoverTile);
            previousMousePos = gridPosition;
        }
    }
}
