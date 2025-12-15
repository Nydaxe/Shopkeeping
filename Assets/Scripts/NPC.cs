using System.IO;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        if(pathToTrace == null)
        {
            Debug.Log("NPC Path Invalid");
            return;
        }
        
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
            placeable.Remove();
            placeable.Place(nextTile);
            transform.position = nextTile.centerPosition;
            path.Remove(nextTile);
        }
    }

    bool PathValid(List<Tile> path)
    {
        for (int i = path.Count; i > 0; i--)
        {
            if(i == path.Count)
            continue;

            if(path[i].IsOccupied()) 
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
