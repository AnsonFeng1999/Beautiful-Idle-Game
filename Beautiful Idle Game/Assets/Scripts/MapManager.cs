using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;
    public static MapManager Instance { get { return _instance; } }

    public GameObject overlayPrefab;
    public GameObject overlayContainer;
    

    public Dictionary<Vector2Int, OverlayTile> map = new();
    
    // public bool ignoreBottomTiles;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } 
        else
        {
            _instance = this;
            // setup map
            var tileMap = gameObject.GetComponentInChildren<Tilemap>();
            BoundsInt bounds = tileMap.cellBounds;
            for (int z = bounds.max.z; z >= bounds.min.z; z--)
            {
                for (int y = bounds.min.y; y < bounds.max.y; y++)
                {
                    for (int x = bounds.min.x; x < bounds.max.x; x++)
                    {
                        if (z == 0)
                            return;

                        var tileLocation = new Vector3Int(x, y, z);
                        var tileKey = new Vector2Int(x, y);
                        if (tileMap.HasTile(tileLocation) && !map.ContainsKey(tileKey))
                        {
                            var overlayTile = Instantiate(overlayPrefab, overlayContainer.transform);
                            var overlay = overlayTile.GetComponent<OverlayTile>();
                            var cellWorldPosition = tileMap.GetCellCenterWorld(tileLocation);
                            overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z+1);
                            overlayTile.GetComponent<SpriteRenderer>().sortingOrder = tileMap.GetComponent<TilemapRenderer>().sortingOrder;
                            overlay.gridLocation = tileLocation;
                            map.Add(tileKey, overlay);
                        }
                    }
                }
            }
        }
    }


    // Getting tiles in 4 directions
    public List<OverlayTile> GetSurroundingTiles(Vector2Int originTile)
    {
        var surroundingTiles = new List<OverlayTile>();


        Vector2Int TileToCheck = new Vector2Int(originTile.x + 1, originTile.y);
        if (map.ContainsKey(TileToCheck))
        {
            if (Mathf.Abs(map[TileToCheck].transform.position.z - map[originTile].transform.position.z) <= 1)
                surroundingTiles.Add(map[TileToCheck]);
        }

        TileToCheck = new Vector2Int(originTile.x - 1, originTile.y);
        if (map.ContainsKey(TileToCheck))
        {
            if (Mathf.Abs(map[TileToCheck].transform.position.z - map[originTile].transform.position.z) <= 1)
                surroundingTiles.Add(map[TileToCheck]);
        }

        TileToCheck = new Vector2Int(originTile.x, originTile.y + 1);
        if (map.ContainsKey(TileToCheck))
        {
            if (Mathf.Abs(map[TileToCheck].transform.position.z - map[originTile].transform.position.z) <= 1)
                surroundingTiles.Add(map[TileToCheck]);
        }

        TileToCheck = new Vector2Int(originTile.x, originTile.y - 1);
        if (map.ContainsKey(TileToCheck))
        {
            if (Mathf.Abs(map[TileToCheck].transform.position.z - map[originTile].transform.position.z) <= 1)
                surroundingTiles.Add(map[TileToCheck]);
        }

        return surroundingTiles;
    }

    public List<OverlayTile> GetTilesInRange(Vector2Int originTile, int range)
    {
        OverlayTile originTileLocation = map[originTile];
        var tileInRange = new List<OverlayTile>();
        int breath = 0;

        tileInRange.Add(originTileLocation);

        var queue = new Queue<OverlayTile>();
        queue.Enqueue(originTileLocation);
        while (breath < range)
        {
            // A list to store adjacentTiles
            var adjacentTiles = new List<OverlayTile>();

            // Pop the queue until it's empty
            while (queue.Count > 0)
            {
                // Tile pop from the queue, then find adjacent tiles of it.
                OverlayTile currentTile = queue.Dequeue();
                List<OverlayTile> temp = GetSurroundingTiles(new Vector2Int(currentTile.gridLocation.x, currentTile.gridLocation.y));
                // "temp" might contains tiles already visited, we only need add non-visited tiles
                foreach (var tile in temp)
                {
                    if (!tileInRange.Contains(tile))
                    {
                        adjacentTiles.Add(tile);
                    }
                }
                temp.Clear();
            }

            // Remove duplicates
            adjacentTiles = adjacentTiles.Distinct().ToList();

            // Once we find all adjacent tiles of tiles in the queue(now the queue is empty),
            // we add them to the result list
            tileInRange.AddRange(adjacentTiles);

            // Then we push them into the queue for the next breath and clear current list.
            adjacentTiles.ForEach(tile => queue.Enqueue(tile));
            adjacentTiles.Clear();
            breath++;
        }
        return tileInRange;
    }
}
