using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] Placeable placeable;

    PlayerInputs playerInputs;

    public void PickUp()
    {
        if(!InventoryManager.instance.SlotOpen())
        {
            Debug.Log("full");
            return;
        }

        if(placeable != null)
            placeable.Remove();

        InventoryManager.instance.AddItem(this.gameObject);
    }

    public void Place(Vector2 position)
    {
        gameObject.transform.parent = null;

        transform.position = position;

        if(placeable != null)
            placeable.Place(GridManager.grid.GetTileWithWorldPosition(transform.position));
    }
}
