using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private Image aspectSlot1;
    [SerializeField] private Image aspectSlot2;

    private Dictionary<EquippableItem, Image> itemToSlotMap = new Dictionary<EquippableItem, Image>();

    public void UpdateAspectSlot(EquippableItem item, bool isEquipping)
    {
        if (item.EquipmentType != EquipmentType.Aspect)
            return;

        if (isEquipping)
        {
            // Assign the item to the first available slot
            if (aspectSlot1.sprite == null)
            {
                aspectSlot1.sprite = item.Icon;
                itemToSlotMap[item] = aspectSlot1;
            }
            else if (aspectSlot2.sprite == null)
            {
                aspectSlot2.sprite = item.Icon;
                itemToSlotMap[item] = aspectSlot2;
            }
        }
        else
        {
            // Find the item in the map and clear the slot
            if (itemToSlotMap.TryGetValue(item, out Image slot))
            {
                slot.sprite = null;
                itemToSlotMap.Remove(item);
            }
        }
    }
}