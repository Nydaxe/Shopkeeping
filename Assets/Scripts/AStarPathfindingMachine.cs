using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AStarPathfindingMachine : MonoBehaviour
{
    const int HORIZONTAL_MOVE_COST = 10;
    const int DIAGONAL_MOVE_COST = 14;

    [SerializeField] bool canTravelDiagonals = false;

    List<Node> openList = new List<Node>();
    List<Node> closedList = new List<Node>();

    Node endNode = null;

    public List<Tile> FindPath(Tile startTile, Tile endTile, Grid grid)
    {
        openList.Clear();
        closedList.Clear();

        if (startTile == null || endTile == null || endTile.IsOccupied())
        {
            Debug.LogError("Invalid start or end tile.");
            return null;
        }

        endNode = new Node(endTile);
        openList.Add(new Node(startTile, 0, CalculateDistance(startTile, endTile)));

        while (openList.Count > 0)
        {
            Node currentNode = lowestCostNode();

            if (currentNode.tile == endTile)
            {
                openList.Clear();
                closedList.Clear();
                return RetracePath(currentNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            SearchNeighbors(currentNode, grid, canTravelDiagonals);
        }
        openList.Clear();
        closedList.Clear();
        Debug.Log("No path found.");
        return null;
    }

    void SearchNeighbors(Node currentNode, Grid grid, bool includeDiagonals = false)
    {
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if(!includeDiagonals && x != 0 && y != 0) continue; // Skip diagonals if not allowed
                if (x == 0 && y == 0) continue; // Skip the current tile

                Tile neighborTile = grid.GetTile(currentNode.tile.x + x, currentNode.tile.y + y);

                if (neighborTile != null && !neighborTile.IsOccupied())
                {
                    if (closedList.Exists(n => n.tile == neighborTile)) continue;

                    int moveCost = (x != 0 && y != 0) ? DIAGONAL_MOVE_COST : HORIZONTAL_MOVE_COST;
                    int tentativeGCost = currentNode.gCost + moveCost;

                    Node neighborNode = openList.Find(n => n.tile == neighborTile);
                    if (neighborNode != null)
                    {
                        if (tentativeGCost < neighborNode.gCost)
                        {
                            neighborNode.gCost = tentativeGCost;
                            neighborNode.hCost = CalculateDistance(neighborTile, endNode.tile);
                            neighborNode.parent = currentNode;
                        }
                        continue;
                    }

                    // New neighbor
                    neighborNode = new Node(neighborTile, tentativeGCost, CalculateDistance(neighborTile, endNode.tile));
                    neighborNode.parent = currentNode;
                    openList.Add(neighborNode);
                }
            }
        }
    }

    int CalculateDistance(Tile currentTile, Tile targetTile)
    {
        int xDistance = Mathf.Abs(currentTile.x - targetTile.x);
        int yDistance = Mathf.Abs(currentTile.y - targetTile.y);
        int remainingdistance = Mathf.Abs(xDistance - yDistance);
        return Mathf.Min(xDistance, yDistance) * DIAGONAL_MOVE_COST + remainingdistance * HORIZONTAL_MOVE_COST;
    }

    Node lowestCostNode()
    {
        Node lowestCostNode = openList[0];
        foreach (Node node in openList)
        {
            if (node.fCost < lowestCostNode.fCost || (node.fCost == lowestCostNode.fCost && node.hCost < lowestCostNode.hCost))
            {
                lowestCostNode = node;
            }
        }
        return lowestCostNode;
    }

    List<Tile> RetracePath(Node endNode)
    {
        List<Tile> path = new List<Tile>();
        Node currentNode = endNode;
        while (currentNode != null)
        {
            path.Add(currentNode.tile);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        return path;
    }
}
