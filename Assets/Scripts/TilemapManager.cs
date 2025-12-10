using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapImporter : MonoBehaviour
{
    [SerializeField] Tilemap unityTilemap;

    void Start()
    {
        ImportCollidableTiles();
    }

    void ImportCollidableTiles()
    {
        BoundsInt bounds = unityTilemap.cellBounds;

        foreach (Vector3Int cellPos in bounds.allPositionsWithin)
        {
            TileBase tileBase = unityTilemap.GetTile(cellPos);

            if (!unityTilemap.HasTile(cellPos))
                continue;

            int x = cellPos.x;
            int y = cellPos.y;

            // Bounds check against custom grid
            if (x < GridManager.grid.origin.x || x > GridManager.grid.xSize*GridManager.grid.tileSize + GridManager.grid.origin.x || y < GridManager.grid.origin.y || y >= GridManager.grid.ySize*GridManager.grid.tileSize + GridManager.grid.origin.x)
                continue;
            // Occupy the grid cell
            GridManager.grid.GetTileWithWorldPosition(new Vector2(x,y)).occupied = true;
        }
    }
}