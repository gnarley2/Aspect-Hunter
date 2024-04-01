using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public Dictionary<Item, int> inventory = new Dictionary<Item, int>();
    public GameObject player;
    public GameObject lanternPrefab;

    [SerializeField] private GameObject inventoryItem;
    [SerializeField] private Transform itemsParent;

    private int numberOfLanterns = 0;

    private void Update()
    {

    }

    private void Awake()
    {
        Instance = this;
        if (itemsParent == null)
        {
            Debug.LogError("ItemsParent is not assigned in the InventoryManager");
        }
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

        if (item.itemName == "Lantern" && numberOfLanterns==0)
        {
            numberOfLanterns++;
            // Instantiate the lantern prefab and attach it to the player
            GameObject lantern = Instantiate(lanternPrefab, player.transform);
            lantern.transform.localPosition = new Vector3(0, 0, 0); // Adjust the position as needed
        }

        GameObject itemUI = Instantiate(inventoryItem, itemsParent);
        itemUI.GetComponentInChildren<Image>().sprite = item.icon;
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

        if (item.itemName == "Lantern")
        {
            numberOfLanterns--;
        }
    }

}

