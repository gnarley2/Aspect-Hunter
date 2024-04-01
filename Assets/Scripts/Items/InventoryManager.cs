using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    //public List<Item> Items = new List<Item>();
    public Dictionary<Item, int> Items = new Dictionary<Item, int>();
    public GameObject player;
    public GameObject lanternPrefab;

    private int numberOfLanterns = 0;

    private void Update()
    {

    }

    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        if (Items.ContainsKey(item))
        {
            Items[item]++;
        }
        else
        {
            Items.Add(item, 1);
        }



        Debug.Log("Inventory Items:");
        foreach (KeyValuePair<Item, int> entry in Items)
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



    }

    public void Remove(Item item)
    {
        if (Items.ContainsKey(item))
        {
            if (Items[item] > 1)
            {
                Items[item]--;
            }
            else
            {
                Items.Remove(item);
            }


        }

        if (item.itemName == "Lantern")
        {
            numberOfLanterns--;
        }
    }
}
