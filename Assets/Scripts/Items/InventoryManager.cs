using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    //public List<Item> Items = new List<Item>();
    public Dictionary<Item, int> Items = new Dictionary<Item, int>();
    public GameObject player;
    // public GameObject lanternPrefab;
    public GameObject itemPrefab;
    public Image[] itemIcons;

    private int numberOfItems = 0;

    private void Awake()
    {
        Instance = this;
        Transform itemsParent = GameObject.Find("itemsParent").transform;
        itemIcons = new Image[itemsParent.childCount];
        for (int i = 0; i < itemsParent.childCount; i++)
        {
            itemIcons[i] = itemsParent.GetChild(i).Find("itembutton/icon").GetComponent<Image>();
        }
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

        UpdateInventoryUI(item.icon);

        Debug.Log("Inventory Items:");
        foreach (KeyValuePair<Item, int> entry in Items)
        {
            Debug.Log(entry.Key.itemName + ": " + entry.Value);
        }

        // if (item.itemName == "Lantern" && numberOfItems==0)
        // {
        //     numberOfItems++;
        //     // Instantiate the lantern prefab and attach it to the player
        //     GameObject lantern = Instantiate(lanternPrefab, player.transform);
        //     lantern.transform.localPosition = new Vector3(0, 0, 0); // Adjust the position as needed
        // }
        if (item.itemName == "Lantern" && numberOfItems == 0)
        {
            numberOfItems++;
            // Instantiate the lantern prefab and attach it to the player
            GameObject i = Instantiate(itemPrefab, player.transform);
            i.transform.localPosition = new Vector3(0, 0, 0); // Adjust the position as needed
        }
    }

    void UpdateInventoryUI(Sprite icon)
    {
        foreach (var itemIcon in itemIcons)
        {
            if (itemIcon.sprite == null) // This finds the first empty slot
            {
                itemIcon.sprite = icon;
                break; // Exit the loop after setting the icon
            }
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

        numberOfItems--;
        // if (item.itemName == "Lantern")
        // {
        //     numberOfItems--;
        // }
    }
}
