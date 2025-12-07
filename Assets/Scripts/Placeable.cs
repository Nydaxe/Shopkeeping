using Unity.VisualScripting;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    public Tile tile {get; private set;}

    void Place(Tile tile)
    {
        if(AbleToPlace(tile))
        {
            tile.AddItem(this.gameObject);
        }
    }

    bool AbleToPlace(Tile tile)
    {
        return tile.occupied;
    }

    void DebugLogTileCordinate()
    {
        Debug.Log($"x:{tile.x} y:{tile.y}");
    }
}
