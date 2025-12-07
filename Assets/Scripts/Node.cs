using UnityEngine;

public class Node
{
    public Tile tile;
    public int gCost;
    public int hCost;
    public Node parent;
    public int fCost => gCost + hCost;

    public Node(Tile tile, int gCost = 0, int hCost = 0)
    {
        this.tile = tile;
        this.gCost = gCost;
        this.hCost = hCost;
        this.parent = null;
    }
}
