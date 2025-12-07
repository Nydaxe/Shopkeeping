using UnityEditor;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static Grid grid;
    [SerializeField] int xSize;
    [SerializeField] int ySize;
    [SerializeField] float tileSize;
    [SerializeField] Vector2 origin;
    

    void Awake()
    {
        grid = new Grid(xSize, ySize, origin, tileSize);
    }

    void OnDrawGizmos()
    {
        for(int y = 0; y <= ySize; y++)
        {
            for(int x = 0; x <= xSize; x++)
            {
                Debug.DrawLine(new Vector2(origin.x + x * tileSize, origin.y + y * tileSize), new Vector2(origin.x + x * tileSize + .1f, origin.y + y * tileSize + .1f), Color.white);
            }
        }
    }
}
