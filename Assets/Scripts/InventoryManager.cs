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
    [SerializeField] float itemCarryingVisualMargin;
    [SerializeField] GameObject hands;
    [SerializeField] AudioClip placingSound;
    [SerializeField] AudioClip pickingUpSound;

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
        int slotNumber = 0;

        for(int i = 0; i < inventory.Length; i++)
        {
            if(inventory[i] == null)
            {
                slotNumber = i+1;
                inventory[i] = item;
                break;
            }
        }

        AudioManager.instance.PlaySoundEffect(pickingUpSound, .7f, .8f, 1f);
        item.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + slotNumber * itemCarryingVisualMargin);
        item.transform.parent = gameObject.transform;

        UpdateHands();
    }

    void PlaceItem()
    {
        for(int i = inventory.Length-1; i >= 0; i--)
        {
            if(inventory[i] == null)
            {
                continue;
            }
            
            Pickup pickup = inventory[i].GetComponent<Pickup>();
            Vector2 placingPosition = new Vector2(player.transform.position.x + placeRange * player.transform.localScale.x, player.transform.position.y);

            if(GridManager.grid.GetTileWithWorldPosition(placingPosition).IsOccupied())
            {
                continue;
            }

            AudioManager.instance.PlaySoundEffect(placingSound, .7f, .8f, 1f);
            inventory[i] = null;
            pickup.Place(placingPosition);
            
            UpdateHands();

            return;
        }
    }

    void UpdateHands()
    {
        foreach(GameObject item in inventory)
        {
            if(item != null)
            {
                hands.SetActive(true);
                return;
            }
        }
        hands.SetActive(false);
    }
}
