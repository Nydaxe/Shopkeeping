using Unity.VisualScripting;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    [SerializeField] bool moveToTile = true;
    public Tile occupiedTile {get; private set;}

    public void Place(Tile tile)
    {
        if(!AbleToPlace(tile))
        {
            return;
        }

        tile.AddItem(gameObject);
        occupiedTile = tile;

        if(moveToTile)
        {
            transform.position = tile.centerPosition;
        }
    }

    bool AbleToPlace(Tile tile)
    {
        return !tile.IsOccupied();
    }

    void Remove()
    {
        occupiedTile.RemoveItem(gameObject);
    }

    void DebugLogTileCordinate()
    {
        Debug.Log($"x:{occupiedTile.x} y:{occupiedTile.y}");
    }

    //! PLACEHOLDER
    void Start()
    {
        Place(GridManager.grid.GetTileWithWorldPosition(transform.position));
    }

    void OnMouseDown()
    {
        Remove();
        Place(GridManager.grid.GetTile(occupiedTile.x - 1, occupiedTile.y));
    }
}