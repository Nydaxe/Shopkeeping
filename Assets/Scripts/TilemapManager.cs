using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapImporter : MonoBehaviour
{
    public Tilemap unityTilemap;
    public Grid grid;

    void Start()
    {
        ImportTilemapIntoCustomGrid();
    }

    void ImportTilemapIntoCustomGrid()
    {
        foreach (Vector3Int position in unityTilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = unityTilemap.GetTile(position);

            if (tile == null)
            {
                return;
            }

            int x = position.x;
            int y = position.y;
            // Bounds check
            if (x >= 0 && x < grid.xSize && y >= 0 && y < grid.ySize)
            {
                grid.tiles[x,y].occupied = true;
            }
        }
    }
}