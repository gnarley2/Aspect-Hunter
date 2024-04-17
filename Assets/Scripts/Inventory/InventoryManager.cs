using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public Dictionary<Item, int> Items = new Dictionary<Item, int>();
    public List<EnemyData> Monsters = new List<EnemyData>();
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

    #region Item
    
    public void AddItem(Item item)
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

        //this adds a lantern prefab //Do not change
        if (numberOfLanterns==0 && (item.itemName == "Lantern"|| item.itemName == "Lantern 2"))
        {
             numberOfLanterns++;
            GameObject lantern = Instantiate(lanternPrefab, player.transform);
            lantern.transform.localPosition = new Vector3(0, 0, 0); // Adjust the position as needed
        }
        if (item.itemName == "Flashlight")
        {
            
            GameObject flashlight = Instantiate(FlashlightPrefab, player.transform);
            flashlight.transform.localPosition = new Vector3(0, 0, 0); // Adjust the position as needed
        }
        if (item.itemName == "LanternBug" && numberOfLanternBugs == 0)
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

    public void RemoveItem(Item item)
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
            if (item.itemName == "Lantern")
            {
                numberOfLanterns--;
            }
        }
    }

    public bool FindItem()
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

    #endregion

    #region Monster

    public void AddMonster(EnemyData data)
    {
        Monsters.Add(data);
    }

    public void RemoveMonster(int index)
    {
        Monsters.RemoveAt(index);
    }
    
    

    #endregion
}
