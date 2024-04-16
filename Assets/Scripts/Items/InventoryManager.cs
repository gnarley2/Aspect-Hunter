using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public Dictionary<Item, int> Items = new Dictionary<Item, int>();
    public GameObject player;
    public GameObject itemPrefab;
    public Image[] itemIcons;
 
   //this is to control adding lights to the player
    private int numberOfLanterns= 0;
    public GameObject lanternPrefab;
    public GameObject FlashlightPrefab;
    public GameObject LanternBugPrefab;
    private int numberOfFlashlights = 0;
    private int numberOfLanternBugs = 0;
    
    
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

        UpdateInventoryUI(item.Icon);

        Debug.Log("Inventory Items:");
        foreach (KeyValuePair<Item, int> entry in Items)
        {
            Debug.Log(entry.Key.ItemName + ": " + entry.Value);
        }

        //this adds a lantern prefab //Do not change
        if (numberOfLanterns==0 && (item.ItemName == "Lantern"|| item.ItemName == "Lantern 2"))
        {
             numberOfLanterns++;
            GameObject lantern = Instantiate(lanternPrefab, player.transform);
            lantern.transform.localPosition = new Vector3(0, 0, 0); // Adjust the position as needed
        }
        if (item.ItemName == "Flashlight")
        {
            
            GameObject flashlight = Instantiate(FlashlightPrefab, player.transform);
            flashlight.transform.localPosition = new Vector3(0, 0, 0); // Adjust the position as needed
        }
        if (item.ItemName == "LanternBug" && numberOfLanternBugs == 0)
        {
            numberOfFlashlights++;
            GameObject lanternbug = Instantiate(LanternBugPrefab, player.transform);
            lanternbug.transform.localPosition = new Vector3(0, 0, 0); // Adjust the position as needed
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
            if (item.ItemName == "Lantern")
            {
                numberOfLanterns--;
            }
        }
    }

    public bool Find()
    {
        foreach (var item in Items)
        {
            if (item.Key.name == "Key")
            {
                Debug.Log(item.Key.name);
                return true;
            }
        }

        return false;
    }
}
