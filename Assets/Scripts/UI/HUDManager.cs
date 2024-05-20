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
    [SerializeField] private Text aspectCount1;
    [SerializeField] private Text aspectCount2;
    [SerializeField] private Text aspectCount3;

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
        UpdateAspectCount();
    }

    public void UpdateAspectSlot(EquippableItem item, bool isEquipping, int index)
    {
        if (item.EquipmentType != EquipmentType.Aspect)
            return;

        if (isEquipping)
        {
            if (index == 1)
            {
                aspectSlot1.sprite = item.Icon;
                aspectSlot1.color = new Color(1, 1, 1, 1);
                slotToItemMap[aspectSlot1] = item;
                slotToAspectMap[1] = item.aspectType;
            }
            else if (index == 2)
            {
                aspectSlot2.sprite = item.Icon;
                aspectSlot2.color = new Color(1, 1, 1, 1);
                slotToItemMap[aspectSlot2] = item;
                slotToAspectMap[2] = item.aspectType;
            }
        }
        else
        {
            if (index == 1)
            {
                aspectSlot1.sprite = null;
                aspectSlot1.color = new Color(1, 1, 1, 0); // Set the slot color to transparent
                slotToItemMap.Remove(aspectSlot1);
                slotToAspectMap[1] = AspectType.None;
            }
            else if (index == 2)
            {
                aspectSlot2.sprite = null;
                aspectSlot2.color = new Color(1, 1, 1, 0); // Set the slot color to transparent
                slotToItemMap.Remove(aspectSlot2);
                slotToAspectMap[2] = AspectType.None;
            }
        }

        UpdateCombination();
        UpdateAspectCount();
    }

    private void UpdateCombination()
    {
        if (slotToAspectMap.ContainsKey(1) && slotToAspectMap[1] != AspectType.None &&
            slotToAspectMap.ContainsKey(2) && slotToAspectMap[2] != AspectType.None)
        {
            slotToAspectMap[3] = AspectDatabase.Instance.GetCombination(slotToAspectMap[1], slotToAspectMap[2]);
            if (slotToAspectMap[3] == AspectType.None) return;
            
            EquippableItem combinationItem = AspectDatabase.Instance.GetEquippableAspect(slotToAspectMap[3]);

            if (combinationItem != null)
            {
                aspectSlot3.sprite = combinationItem.Icon;
                aspectSlot3.color = new Color(1, 1, 1, 1);
                slotToItemMap[aspectSlot3] = combinationItem;
            }
            else
            {
                aspectSlot3.sprite = null;
                aspectSlot3.color = new Color(1, 1, 1, 0);
                slotToItemMap.Remove(aspectSlot3);
                slotToAspectMap[3] = AspectType.None;
            }
        }
        else
        {
            aspectSlot3.sprite = null;
            aspectSlot3.color = new Color(1, 1, 1, 0);
            slotToAspectMap[3] = AspectType.None;
            slotToItemMap.Remove(aspectSlot3);
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

    public void UpdateAspectCount()
    {
        aspectCount1.text = slotToAspectMap.ContainsKey(1) && slotToAspectMap[1] != AspectType.None ? AspectInventory.Instance.GetAspectCount(slotToAspectMap[1]).ToString() : "";
        aspectCount2.text = slotToAspectMap.ContainsKey(2) && slotToAspectMap[2] != AspectType.None ? AspectInventory.Instance.GetAspectCount(slotToAspectMap[2]).ToString() : "";
        aspectCount3.text = slotToAspectMap.ContainsKey(3) && slotToAspectMap[3] != AspectType.None ? AspectInventory.Instance.GetAspectCount(slotToAspectMap[3]).ToString() : "";
    }
}