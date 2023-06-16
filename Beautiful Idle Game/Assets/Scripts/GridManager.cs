using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;

    [SerializeField] private Tile baseTilePrefab, offsetTilePrefab;

    private void Start()
    {
        CreateGrid();
    }
    void CreateGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y <_height; y++)
            {
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                if (isOffset)
                {
                    var spawnedTile = Instantiate(baseTilePrefab, new Vector3(x, -0.82f, y), Quaternion.identity);
                    spawnedTile.name = $"Tile {x} {y}";
                }
                else
                {
                    var spawnedTile = Instantiate(offsetTilePrefab, new Vector3(x, -0.82f, y), Quaternion.identity);
                    spawnedTile.name = $"Tile {x} {y}";
                }
            }
        }
    }
}
