using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Serializable]
    public class SingleMonsterInventory
    {
        public MonsterData monsterData;
        public bool isInInventory = true;
    }
    
    public static InventoryManager Instance;
    public Dictionary<Item, int> Items = new Dictionary<Item, int>();
    public List<SingleMonsterInventory> Monsters = new List<SingleMonsterInventory>();
    public GameObject player;
    public GameObject itemPrefab;
    public Image[] itemIcons;
 
   //this is to control adding lights to the player
    private int numberOfLanterns= 0;
    public GameObject lanternPrefab;

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
        if (item.itemName == "Lantern" && numberOfLanterns == 0)
        {
             numberOfLanterns++;
            GameObject lantern = Instantiate(lanternPrefab, player.transform);
            lantern.transform.localPosition = new Vector3(0, 0, 0); // Adjust the position as needed
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

    public void AddMonster(MonsterData data)
    {
        SingleMonsterInventory monsterInventory = new SingleMonsterInventory();
        monsterInventory.monsterData = data;
        
        Monsters.Add(monsterInventory);
    }

    public void RemoveMonster(int index)
    {
        Monsters.RemoveAt(index);
    }
    
    public bool HasMonster(int index)
    {
        if (index < 0 || index >= Monsters.Count) return false;

        return true;
    }

    public MonsterData GetMonster(int index)
    {
        if (index < 0 || index >= Monsters.Count) return null;

        return Monsters[index].monsterData;
    }

    public int GetMonsterIndex(MonsterName name)
    {
        for (int i = 0; i < Monsters.Count; i++)
        {
            if (Monsters[i].monsterData.monsterDetails.name == name)
            {
                return i;
            }
        }

        Debug.LogError("Can't find this monster index");
        return -1;
    }

    public GameObject ReleaseMonster(MonsterName name)
    {
        return ReleaseMonster(GetMonsterIndex(name));
    }

    public GameObject ReleaseMonster(int index)
    {
        if (index < 0 || index >= Monsters.Count)
        {
            Debug.LogError("Out of index monster inventory");
            return null;
        }

        if (Monsters[index].isInInventory)
        {
            Monsters[index].isInInventory = false;

            MonsterData data = Monsters[index].monsterData;
            
            GameObject prefab = Instantiate(MonsterDatabase.Instance.GetMonsterPrefab(data.monsterDetails.name), transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<Monster>().Initialize(data);
            
            return prefab;
        }
        else
        {
            Debug.LogError("Monster have been released");
        }
        
        return null;
    }

    #endregion
}
