using System.IO;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class NPCPathingMachine : MonoBehaviour
{
    [SerializeField] AStarPathfindingMachine pathfinding;
    [SerializeField] Placeable placeable;
    [SerializeField] float pathTracingDelay;
    List<Tile> pathToTrace;
    public bool moving { get; private set; }
    public event Action OnFinishedMovement;


    public void Go(Vector2 position)
    {
        if(moving)
        {
            Debug.Log("too many NPC Go calls");
        }

        pathToTrace = pathfinding.FindPath(placeable.occupiedTile, GridManager.grid.GetTileWithWorldPosition(position), GridManager.grid);
        if(pathToTrace == null)
        {
            Debug.Log("NPC Path Invalid");
            return;
        }
        
        moving = true;
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
            path.RemoveAt(0);
        }
        moving = false;
        OnFinishedMovement?.Invoke();
        Debug.Log("NPC done moving");
    }

    bool PathValid(List<Tile> path)
    {
        for (int i = 1; i < path.Count; i++)
        {
            if (path[i].IsOccupied())
                return false;
        }
        return true;
    }

    List<Tile> CreatePath(Tile targetTile)
    {

        List<Tile> newPath = pathfinding.FindPath(placeable.occupiedTile, targetTile, GridManager.grid);

        if (newPath == null || newPath.Count == 0)
            return null;

        return newPath;
    }

}
