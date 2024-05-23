using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestiaryPanel : MonoBehaviour
{
    [SerializeField] MonsterTooltip monsterTooltip;
    [SerializeField] MonsterSlot[] monsterSlots;
    [SerializeField] MonsterData[] monsterDataArray; // Array of MonsterData to assign to slots

    private void Awake()
    {
        for (int i = 0; i < monsterSlots.Length; i++)
        {
            if (i < monsterDataArray.Length)
            {
                monsterSlots[i].MonsterData = monsterDataArray[i];
                Debug.Log("Assigned MonsterData to slot " + i + ": " + monsterDataArray[i].monsterDetails.name); // Debug log
            }
            else
            {
                monsterSlots[i].MonsterData = null;
                Debug.LogWarning("No MonsterData for slot " + i); // Debug log
            }

            monsterSlots[i].OnPointerEnterEvent += ShowTooltip;
            monsterSlots[i].OnPointerExitEvent += HideTooltip;
        }
    }

    private void ShowTooltip(MonsterSlot monsterSlot)
    {
        Debug.Log("ShowTooltip called"); // Debug log
        MonsterData monsterData = monsterSlot.MonsterData;
        if (monsterData != null)
        {
            Debug.Log("Showing tooltip for monster: " + monsterData.monsterDetails.name); // Debug log
            monsterTooltip.ShowTooltip(monsterData.monsterDetails, monsterData.currentHealth);
        }
        else
        {
            Debug.LogWarning("MonsterData is null"); // Debug log
        }
    }

    private void HideTooltip(MonsterSlot monsterSlot)
    {
        Debug.Log("HideTooltip called"); // Debug log
        monsterTooltip.HideTooltip();
    }
}