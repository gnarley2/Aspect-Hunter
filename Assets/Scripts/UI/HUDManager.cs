using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private Image aspectSlot1;
    [SerializeField] private Image aspectSlot2;

    Vector2 aspect1Size;
    Vector2 aspect2Size;

    private Dictionary<EquippableItem, Image> itemToSlotMap = new Dictionary<EquippableItem, Image>();

    void Start()
    {
        aspect1Size = aspectSlot1.rectTransform.sizeDelta;
        aspect2Size = aspectSlot2.rectTransform.sizeDelta;
    }

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
            else
            {
                // Both slots are occupied, replace the first slot
                EquippableItem firstItem = itemToSlotMap.Keys.FirstOrDefault();
                if (firstItem != null)
                {
                    Image firstSlot = itemToSlotMap[firstItem];
                    firstSlot.sprite = item.Icon;
                    itemToSlotMap.Remove(firstItem);
                    itemToSlotMap[item] = firstSlot;
                }
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

    void Update()
    {
        if (Input.GetKeyDown("1") && aspectSlot1.sprite != null)
        {
            aspectSlot1.rectTransform.sizeDelta = new Vector2(80, 80);
            aspectSlot2.rectTransform.sizeDelta = aspect2Size; // Reset aspectSlot2 to its original size

            //current projectile index change to whatever is equipped
        }
        else if (Input.GetKeyDown("2") && aspectSlot2.sprite != null)
        {
            aspectSlot2.rectTransform.sizeDelta = new Vector2(80, 80);
            aspectSlot1.rectTransform.sizeDelta = aspect1Size; // Reset aspectSlot1 to its original size

            //current projectile index change to whatever is equipped
        }


        //aspectdatabase reference here to update 3rd slot

        //fix size when no aspects in slot
    }
}