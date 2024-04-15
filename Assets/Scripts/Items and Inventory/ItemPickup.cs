using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] Inventory inventory;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item") && !inventory.IsFull())
        {
            Item item = other.gameObject.GetComponent<ItemComponent>().Item;
            if (inventory.AddItem(item))
            {
                Destroy(other.gameObject); // Destroy the item after picking it up
            }
        }
    }
}
