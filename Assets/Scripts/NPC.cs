using System.IO;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class NPC : MonoBehaviour
{
    [SerializeField] AStarPathfindingMachine pathfinding;
    [SerializeField] Placeable placeable;
    [SerializeField] float pathTracingDelay;
    [SerializeField] Transform testPathEnd;
    List<Tile> pathToTrace;


    void OnMouseDown()
    {
        pathToTrace = pathfinding.FindPath(placeable.occupiedTile, GridManager.grid.GetTileWithWorldPosition(testPathEnd.position), GridManager.grid);

        TracePath(pathToTrace);
    }

    async void TracePath(List<Tile> path)
    {
        Debug.Log("Tracing Path");
        while (path.Count > 0)
        {
            await Awaitable.WaitForSecondsAsync(pathTracingDelay);
            
            if(!PathValid(path))
            {
                List<Tile> createdPath = CreatePath(path[path.Count - 1]);
                if(createdPath != null)
                {
                    TracePath(createdPath);
                }
                return;
            }

            Tile nextTile = path[0];
            placeable.Place(nextTile);
            transform.position = nextTile.centerPosition;
            path.Remove(nextTile);

        }
    }

    bool PathValid(List<Tile> path)
    {
        foreach(Tile tile in path)
        {
            if(tile.IsOccupied()) 
                return false;
        }
        return true;
    }

    List<Tile> CreatePath(Tile targetTile)
    {
        List<Tile> newPath = pathfinding.FindPath(placeable.occupiedTile, GridManager.grid.GetTileWithWorldPosition(testPathEnd.position), GridManager.grid);

        if(pathToTrace == null)
        {
            Debug.Log("Path Invalid");
        }

        return newPath;
    }
}
