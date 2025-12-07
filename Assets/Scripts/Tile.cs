using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public List<GameObject> Contents;
    public float size{get; private set;}
    public Vector2 centerPosition{get; private set;}
    public Vector2 cordinate{get; private set;}
    public bool occupied = false;


    public Tile(Vector2 cordinate, Vector2 centerPosition, float size)
    {
        this.cordinate = cordinate;
        this.centerPosition = centerPosition;
        this.size = size;
    }

    public void AddItem(GameObject item)
    {
        Contents.Add(item);

        Occupier occupier = item.GetComponent<Occupier>();
        if(occupier != null && occupier.enabled)
        {
            occupied = true;
        }
    }

    public void RemoveItem(GameObject item)
    {
        Contents.Add(item);

        Occupier occupier = item.GetComponent<Occupier>();
        if(occupier != null && occupier.enabled)
        {
            occupied = false;
        }
    }

    public bool IsOccupied()
    {
        return occupied;
    }
}
