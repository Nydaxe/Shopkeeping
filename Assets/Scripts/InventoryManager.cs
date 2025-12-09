using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    [SerializeField] int InventorySlotAmount;

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
        item.SetActive(false);

        for(int i = 0; i < inventory.Length; i++)
        {
            if(inventory[i] == null)
            {
                inventory[i] = item;
                return;
            }
        }
    }
}
