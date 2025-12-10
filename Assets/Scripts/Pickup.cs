using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] Collider2D thisCollider;
    [SerializeField] Placeable placeable;

    PlayerInputs playerInputs;

    public void PickUp()
    {
        Debug.Log("yo4");
        if(!InventoryManager.instance.SlotOpen())
        {
            Debug.Log("full");
            return;
        }

        if(placeable != null)
            placeable.Remove();

        if(thisCollider != null)
            thisCollider.enabled = false;

        InventoryManager.instance.AddItem(this.gameObject);
    }

    public void Place(Vector2 position)
    {
        gameObject.transform.parent = null;

        transform.position = position;
        
        if(thisCollider != null)
            thisCollider.enabled = true;

        if(placeable != null)
            placeable.Place(GridManager.grid.GetTileWithWorldPosition(transform.position));
    }
}
