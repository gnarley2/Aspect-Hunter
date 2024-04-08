using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EquipmentPanel : MonoBehaviour
{
    [SerializeField] Transform equipmentSlotsParent;
    [SerializeField] EquipmentSlot[] equipmentSlots;
    [SerializeField] ItemDescription itemDescription;

    public event Action<Item> OnItemRightClickedEvent;

    void OnValidate()
    {
        equipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
    }

    void Awake()
    {
        foreach (var slot in equipmentSlots)
        {
            slot.OnRightClickEvent += OnItemRightClickedEvent;
            slot.OnLeftClickEvent += HandleItemLeftClick;
        }
    }
    
    void HandleItemLeftClick(Item item)
    {
        if (itemDescription != null && item != null)
            itemDescription.SetDescription(item.description, item.name);
    }

    

    public bool AddItem(EquippableItem item, out EquippableItem previousItem)
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentSlots[i].ItemType == item.ItemType)
            {
                previousItem = (EquippableItem)equipmentSlots[i].item;
                equipmentSlots[i].item = item;
                return true;
            }
        }
        previousItem = null;
        return false;
    }

        public bool RemoveItem(EquippableItem item)
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentSlots[i].item == item)
            {
                equipmentSlots[i].item = null;
                return true;
            }
        }
        return false;
    }
}
