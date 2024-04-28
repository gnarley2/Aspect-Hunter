using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] Item item;

    private bool isInRange;
    private UsableItem usableItem;
    private Core core;
    
    private void Awake()
    {
        core = GameObject.FindWithTag("Player").GetComponentInChildren<Core>();
    }

    private void Start()
    {
        Init();
    }

    void Init()
    {
        usableItem = item as UsableItem;
        if (usableItem)
        {
            usableItem.Initialize(core);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (usableItem)
            {
                UseItem();
            }
            else
            {
                AddToInventory();
            }
        }
    }

    void AddToInventory()
    {
        if (Inventory.Instance.IsFull()) return;
            
        Inventory.Instance.AddItem(Instantiate(item));
        Destroy(gameObject);
    }

    void UseItem()
    {
        if (usableItem.Use())
        {
            Destroy(gameObject);
        }
    }

}