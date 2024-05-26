using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    [SerializeField] private List<Item> items = new List<Item>();

    public Action<Item> OnAddItem;

    [SerializeField] private int maxItem = 17;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(this);
    }
    
    public bool AddItem(Item item)
    {
        items.Add(item);
        OnAddItem?.Invoke(item);
        return true;
    }

    public bool RemoveItem(int index)
    {
        if (items.Count <= index) return false;
        
        items.RemoveAt(index);
        return true;
    }
    
    public bool IsFull()
    {
        return items.Count > maxItem;
    }

    public bool HasItem(Item item)
    {
        bool hasItem = items.Contains(item);
        if (hasItem)
        {
            Debug.Log($"Item {item.name} is already in the inventory.");
        }
        else
        {
            Debug.Log($"Item {item.name} is not in the inventory.");
        }
        return hasItem;
    }
}
