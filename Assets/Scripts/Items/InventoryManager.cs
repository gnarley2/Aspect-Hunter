using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public Dictionary<Item, int> inventory = new Dictionary<Item, int>();


    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        if (inventory.ContainsKey(item))
        {
            inventory[item]++;
        }
        else
        {
            inventory.Add(item, 1);
        }
        Debug.Log("Inventory Items:");
        foreach (KeyValuePair<Item, int> entry in inventory)
        {
            Debug.Log(entry.Key.itemName + ": " + entry.Value);
        }

    }

    public void Remove(Item item)
    {
        if (inventory.ContainsKey(item))
        {
            if (inventory[item] > 1)
            {
                inventory[item]--;
            }
            else
            {
                inventory.Remove(item);
            }


        }
    }

}

