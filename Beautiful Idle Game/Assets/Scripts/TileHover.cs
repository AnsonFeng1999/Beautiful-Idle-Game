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
    private TileBase _previousTile;
    private Vector3Int _previousMousePos;

    private void Update()
    {
        // Cast a ray from the mouse position to the worl
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPosition = tilemap.WorldToCell(pos);
        gridPosition.z = 0;
        if (tilemap.HasTile(gridPosition) && !pos.Equals(_previousMousePos))
        {
            if (_previousTile)
            {
                tilemap.SetTile(_previousMousePos, _previousTile); // Remove previous tile
            }
            _previousTile = tilemap.GetTile(gridPosition); // save current previous tile
            tilemap.SetTile(gridPosition, hoverTile);
            _previousMousePos = gridPosition;
        }
    }
}
