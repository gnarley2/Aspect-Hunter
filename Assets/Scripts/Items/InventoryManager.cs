using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    private Text itemName;
    private Image itemIcon;
    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item Item)
    {
        Items.Add(Item);
       // ListItems();
        //debug messages to just check if adding correctly
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
        Debug.Log("Inventory - " + TotalInventory); 
    }

    public void Remove(Item Item)
    {
        Items.Remove(Item);
    }


    public void ListItems()
    {


     
    }
}

