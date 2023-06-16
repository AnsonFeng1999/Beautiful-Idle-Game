using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class PathFinder
{

    public List<OverlayTile> FindPath(OverlayTile start, OverlayTile end, float jumpHeight)
    {
        List<OverlayTile> openList = new List<OverlayTile>();
        List<OverlayTile> closedList = new List<OverlayTile>();
        
        openList.Add(start);

        while (openList.Count > 0)
        {
            OverlayTile current = openList.OrderBy(x => x.F).First();

            openList.Remove(current);
            closedList.Add(current);

            if (current == end)
            {
                // finalize
                return GetFinishedList(start, end);
            }

            var neighborTiles = GetNeighbourTiles(current);
            
            foreach (var neighbour in neighborTiles)
            {
                if (neighbour.isBlocked || closedList.Contains(neighbour) ||
                    Mathf.Abs(neighbour.gridLocation.z - current.gridLocation.z) > jumpHeight)
                {
                    continue;
                }

                neighbour.G = GetManhattenDistance(start, neighbour);
                neighbour.H = GetManhattenDistance(end, neighbour);
                neighbour.previous = current;
                
                if (!openList.Contains(neighbour))
                {
                    openList.Add(neighbour);
                }
            }

        }
        return new List<OverlayTile>();
    }

    private List<OverlayTile> GetFinishedList(OverlayTile start, OverlayTile end)
    {
        List<OverlayTile> finishedList = new List<OverlayTile>();

        OverlayTile currentTile = end;

        while (currentTile != start)
        {
            finishedList.Add(currentTile);
            currentTile = currentTile.previous;
        }

        finishedList.Reverse();
        
        return finishedList;
    }

    private int GetManhattenDistance(OverlayTile start, OverlayTile neighbour)
    {
        return Mathf.Abs(start.gridLocation.x - neighbour.gridLocation.x) +
               Math.Abs(start.gridLocation.y - neighbour.gridLocation.y);
    }

    private List<OverlayTile> GetNeighbourTiles(OverlayTile current)
    {
        var map = MapManager.Instance.map;
        List<OverlayTile> neighbors = new List<OverlayTile>();
        
        // top
        Vector2Int locationToCheck = new Vector2Int(
            current.gridLocation.x,
            current.gridLocation.y + 1
        );

        if (map.ContainsKey(locationToCheck))
        {
            neighbors.Add(map[locationToCheck].GetComponent<OverlayTile>());   
        }
        
        // bottom
        locationToCheck = new Vector2Int(
            current.gridLocation.x,
            current.gridLocation.y - 1
        );

        if (map.ContainsKey(locationToCheck))
        {
            neighbors.Add(map[locationToCheck].GetComponent<OverlayTile>());   
        }
        
        // right
        locationToCheck = new Vector2Int(
            current.gridLocation.x + 1,
            current.gridLocation.y
        );

        if (map.ContainsKey(locationToCheck))
        {
            neighbors.Add(map[locationToCheck].GetComponent<OverlayTile>());   
        }
        
        // left
        locationToCheck = new Vector2Int(
            current.gridLocation.x - 1,
            current.gridLocation.y
        );

        if (map.ContainsKey(locationToCheck))
        {
            neighbors.Add(map[locationToCheck].GetComponent<OverlayTile>());   
        }
        
        return neighbors;
    }
}
