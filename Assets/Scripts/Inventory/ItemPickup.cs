using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item Item;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup();
        }
    }
    
    void Pickup()
    {
        InventoryManager.Instance.AddItem(Item);
        Destroy(gameObject);
    }

}
