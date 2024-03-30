using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item Item)
    {
        Items.Add(Item);
        Debug.Log("Item Added to Inventory");
        string TotalInventory = "";
        foreach (Item i in Items)
        {
            if (TotalInventory == "")
            {
                TotalInventory += i.itemName;
            }
            else
            {
                TotalInventory += ", " + i.itemName;
            }
        }
        Debug.Log("Inventory - " + TotalInventory); // Assuming Item has a property called Name
    }

    public void Remove(Item Item)
    {
        Items.Remove(Item);
    }
}
