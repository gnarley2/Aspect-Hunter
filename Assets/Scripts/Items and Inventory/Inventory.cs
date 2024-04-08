using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<Item> items;
    [SerializeField] Transform itemsParent;
    [SerializeField] ItemSlot[] itemSlots;

    public event Action<Item> OnItemRightClickedEvent;

    void Awake()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].OnRightClickEvent += OnItemRightClickedEvent;
        }
    }
    
    public void SubscribeToItemSlots(Action<Item> onLeftClickAction)
    {
        foreach (var slot in itemSlots)
        {
            slot.OnLeftClickEvent += onLeftClickAction;
        }
    }

    void OnValidate() 
    {
        if (itemsParent != null)
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();

        RefreshUI();
    }

    void RefreshUI()
    {
        int i = 0;
        for (; i < items.Count && i < itemSlots.Length; i++)
            itemSlots[i].item = items[i];

        for (; i < itemSlots.Length; i++)
            itemSlots[i].item = null;
    }

    public bool AddItem(Item item)
    {
        if (IsFull())
            return false;

        items.Add(item);
        RefreshUI();
        return true;
    }

    public bool RemoveItem(Item item)
    {
        if (items.Remove(item))
        {
            RefreshUI();
            return true;
        }
        return false;
    }

    public bool IsFull()
    {
        return items.Count >= itemSlots.Length;
    }
}
