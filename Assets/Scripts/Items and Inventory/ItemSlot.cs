using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image image;

    public event Action<Item> OnRightClickEvent;
    public event Action<Item> OnLeftClickEvent;

    Item _item;
    public Item item {
        get { return _item; }
        set {
            _item = value;
            if (_item == null)
                image.enabled = false;
            else {
                image.sprite = _item.Icon;
                image.enabled = true;
            }
        }
    }

    protected virtual void OnValidate()
    {
        if (image == null)
            image = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (item != null && OnLeftClickEvent != null)
                OnLeftClickEvent(item);
        }
        else if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null && OnRightClickEvent != null)
                OnRightClickEvent(item);
        }
    }
}
