using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] Item item;

    private bool isInRange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Inventory.Instance.IsFull()) return;
            
            Inventory.Instance.AddItem(Instantiate(item));
            Destroy(gameObject);
        }
    }



}