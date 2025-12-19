using System.Collections.Generic;
using UnityEngine;
using System;

public class Tile
{
    public List<GameObject> contents {get; private set;}
    public float size{get; private set;}
    public Vector2 centerPosition{get; private set;}
    public int x {get; private set;}
    public int y {get; private set;}
    public bool occupied = false;

    public Action<Vector2Int, GameObject> OnAddObject;
    public Action<Vector2Int> OnRemoveObject;


    public Tile(Vector2 cordinate, Vector2 centerPosition, float size)
    {
        this.x = (int)cordinate.x;
        this.y = (int)cordinate.y;
        this.centerPosition = centerPosition;
        this.size = size;
        contents = new List<GameObject>{};
    }

    public void AddItem(GameObject item)
    {
        contents.Add(item);
        OnAddObject?.Invoke(new Vector2Int(x,y),item);

        Occupier occupier = item.GetComponent<Occupier>();
        if(occupier != null && occupier.enabled)
        {
            occupied = true;
        }
    }

    public void RemoveItem(GameObject item)
    {
        contents.Remove(item);
        OnRemoveObject?.Invoke(new Vector2Int(x,y));

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
