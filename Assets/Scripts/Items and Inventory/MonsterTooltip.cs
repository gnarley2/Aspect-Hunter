using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterTooltip : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Text MonsterNameText;
    [SerializeField] Image Aspect;
    [SerializeField] Text DetailsText;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        ToggleUI(false);
    }

    public void ToggleUI(bool isActive)
    {
        if (isActive)
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        else
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void ShowTooltip(MonsterDetails monsterDetails)
    {
        image.sprite = monsterDetails.icon;
        MonsterNameText.text = "Name: " + monsterDetails.name.ToString();
        if (monsterDetails.item != null)
        {
            Aspect.sprite = monsterDetails.item.Icon;
            Aspect.color = new Color(1, 1, 1);
        }
        else
        {
            Aspect.sprite = null;
            Aspect.color = new Color(1, 1, 1, 0);
        }

        DetailsText.text = monsterDetails.description;
        
        ToggleUI(true);
    }

    public void HideTooltip()
    {
        Debug.Log("Hiding tooltip"); // Debug log
        ToggleUI(false);
    }
}