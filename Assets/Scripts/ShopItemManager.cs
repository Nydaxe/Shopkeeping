using System.Collections.Generic;
using UnityEngine;

public class ShopItemManager : MonoBehaviour
{
    public static ShopItemManager instance;
    public Dictionary<Vector2Int, GameObject> shopItems;

    [SerializeField] Transform bottomLeftShop;
    [SerializeField] Transform topRightShop;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    void Start()
    {
        shopItems = new Dictionary<Vector2Int, GameObject>();

        Tile topRightTile = GridManager.grid.GetTileWithWorldPosition(topRightShop.position);
        Tile bottomLeftTile = GridManager.grid.GetTileWithWorldPosition(bottomLeftShop.position);

        Vector2Int topRight = new Vector2Int(topRightTile.x,topRightTile.y);
        Vector2Int bottomLeft = new Vector2Int(bottomLeftTile.x,bottomLeftTile.y);

        for (int x = bottomLeft.x; x <= topRight.x; x++)
        {
            for (int y = bottomLeft.y; y <= topRight.y; y++)
            {
                Tile tile = GridManager.grid.GetTile(x,y);
                tile.OnAddObject += AddItem;
                tile.OnRemoveObject += RemoveItem;
            }
        }
    }

    void AddItem(Vector2Int gridPosition, GameObject item)
    {
        if(item.GetComponent<ShopItem>() == null)
         return;
         
        shopItems.Add(gridPosition, item);
    }

    void RemoveItem(Vector2Int gridPosition, GameObject item)
    {
        if(item.GetComponent<ShopItem>() == null)
            return;
            
        shopItems.Remove(gridPosition);
    }
}
