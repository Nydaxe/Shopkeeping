using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    [SerializeField] int InventorySlotAmount;
    [SerializeField] PlayerInputs inputs;
    [SerializeField] GameObject player;
    [SerializeField] float placeRange;

    public GameObject[] inventory {get; private set;}

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    
    void Start()
    {
        inputs.PlaceItem += PlaceItem;
        inventory = new GameObject[InventorySlotAmount];
    }

    public bool SlotOpen()
    {
        foreach(GameObject slot in inventory)
        {
            if(slot == null)
            {
                return true;
            }
        }

        return false;
    }

    public void AddItem(GameObject item)
    {
        item.transform.parent = gameObject.transform;
        item.transform.position = gameObject.transform.position;

        for(int i = 0; i < inventory.Length; i++)
        {
            if(inventory[i] == null)
            {
                inventory[i] = item;
                return;
            }
        }
    }

    void PlaceItem()
    {
        for(int i = 0; i < inventory.Length; i++)
        {
            if(inventory[i] == null)
            {
                continue;
            }

            Pickup pickup = inventory[i].GetComponent<Pickup>();

            inventory[i] = null;

            pickup.Place(new Vector2(player.transform.position.x + placeRange * player.transform.localScale.x, player.transform.position.y));
            
            return;
        }
    }
}
