using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private Image aspectSlot1;
    [SerializeField] private Image aspectSlot2;
    [SerializeField] private Image aspectSlot3;

    Vector2 aspect1Size;
    Vector2 aspect2Size;
    Vector2 aspect3Size;

    private Dictionary<Image, EquippableItem> slotToItemMap = new Dictionary<Image, EquippableItem>();
    private Dictionary<int, AspectType> slotToAspectMap = new Dictionary<int, AspectType>();

    void Start()
    {
        aspect1Size = aspectSlot1.rectTransform.sizeDelta;
        aspect2Size = aspectSlot2.rectTransform.sizeDelta;
        aspect3Size = aspectSlot3.rectTransform.sizeDelta;
    }

    public void UpdateAspectSlot(EquippableItem item, bool isEquipping, int index)
    {
        if (item.EquipmentType != EquipmentType.Aspect)
            return;

        if (isEquipping)
        {
            // Assign the item to the first available slot
            if (index == 1)
            {
                aspectSlot1.sprite = item.Icon;
                slotToItemMap[aspectSlot1] = item;
                slotToAspectMap[1] = item.aspectType;
            }
            else if (index == 2)
            {
                aspectSlot2.sprite = item.Icon;
                slotToItemMap[aspectSlot2] = item;
                slotToAspectMap[2] = item.aspectType;
            }
            // else
            // {
            //     // Both slots are occupied, replace the first slot
            //     EquippableItem firstItem = slotToItemMap.Keys.FirstOrDefault();
            //     if (firstItem != null)
            //     {
            //         Image firstSlot = slotToItemMap[firstItem];
            //         firstSlot.sprite = item.Icon;
            //         slotToItemMap.Remove(firstItem);
            //         slotToItemMap[item] = firstSlot;
            //     }
            // }
        }
        else
        {
            Image slot = slotToItemMap.FirstOrDefault(x => x.Value == item).Key;
            // Find the item in the map and clear the slot
            if (slot != null)
            {
                slot.sprite = null;
                slotToItemMap.Remove(slot);
            }
        }

        if (slotToAspectMap.ContainsKey(1) &&  slotToAspectMap[1] != null && slotToAspectMap.ContainsKey(2) && slotToAspectMap[2] != null)
        {
            slotToAspectMap[3] =
                AspectDatabase.Instance.GetCombination(slotToAspectMap[1], slotToAspectMap[2]);
            EquippableItem combinationItem = AspectDatabase.Instance.GetEquippableAspect(slotToAspectMap[3]);

            if (combinationItem != null)
            {
                aspectSlot3.sprite = combinationItem.Icon;
                slotToItemMap[aspectSlot3] = combinationItem;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("1") && aspectSlot1.sprite != null)
        {
            aspectSlot1.rectTransform.sizeDelta = new Vector2(80, 80);
            aspectSlot2.rectTransform.sizeDelta = aspect2Size;
            aspectSlot3.rectTransform.sizeDelta = aspect3Size;
            
            // Change projectile index based on the aspect in slot 1
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerCombat playerCombat = player.GetComponent<PlayerCombat>();
            EquippableItem itemInSlot1 = slotToItemMap.FirstOrDefault(x => x.Key == aspectSlot1).Value;
            playerCombat.SetCurrentProjectileIndexBasedOnAspect(itemInSlot1);
            Debug.Log(itemInSlot1);
        }
        else if (Input.GetKeyDown("2") && aspectSlot2.sprite != null)
        {
            aspectSlot2.rectTransform.sizeDelta = new Vector2(80, 80);
            aspectSlot1.rectTransform.sizeDelta = aspect1Size;
            aspectSlot3.rectTransform.sizeDelta = aspect3Size;
            
            // Change projectile index based on the aspect in slot 2
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerCombat playerCombat = player.GetComponent<PlayerCombat>();
            EquippableItem itemInSlot2 = slotToItemMap.FirstOrDefault(x => x.Key == aspectSlot2).Value;
            playerCombat.SetCurrentProjectileIndexBasedOnAspect(itemInSlot2);
            Debug.Log(itemInSlot2);
        }
        else if (Input.GetKeyDown("3") && aspectSlot3.sprite != null)
        {
            aspectSlot3.rectTransform.sizeDelta = new Vector2(80, 80);
            aspectSlot1.rectTransform.sizeDelta = aspect1Size;
            aspectSlot2.rectTransform.sizeDelta = aspect2Size;
            
            // Change projectile index based on the aspect in slot 2
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerCombat playerCombat = player.GetComponent<PlayerCombat>();
            EquippableItem itemInSlot3 = slotToItemMap.FirstOrDefault(x => x.Key == aspectSlot3).Value;
            playerCombat.SetCurrentProjectileIndexBasedOnAspect(itemInSlot3);
            Debug.Log(itemInSlot3);
        }
    }
}