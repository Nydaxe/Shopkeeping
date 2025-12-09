using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] string playerTag = "Player";
    PlayerInputs playerInputs;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == playerTag)
        {
            if(playerInputs == null)
            {
                playerInputs = collision.gameObject.GetComponent<PlayerInputs>();    
            }

            playerInputs.InputPickup += PickUp;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == playerTag)
        {
            GameObject player = collision.gameObject;

            if(playerInputs == null)
            {
                playerInputs = collision.gameObject.GetComponent<PlayerInputs>();    
            }

            player.GetComponent<PlayerInputs>().InputPickup -= PickUp;
        }
    }

    void PickUp()
    {
        if(!InventoryManager.instance.SlotOpen())
        {
            Debug.Log("full");
            return;
        }

        playerInputs.InputPickup -= PickUp;
        InventoryManager.instance.AddItem(this.gameObject);
    }
}
