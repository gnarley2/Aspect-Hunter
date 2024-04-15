using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChest : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private KeyCode itemPickupKeyCode = KeyCode.E;

    private bool isInRange;
    private bool isEmpty;

    private Inventory inventory;

    private void Awake()
    {
        inventory = GameManager.Instance.GetInventory();
    }

    private void Update()
    {
        if (isInRange && Input.GetKeyDown(itemPickupKeyCode))
        {
            if (!isEmpty && inventory != null && !inventory.IsFull())
            {
                bool wasAdded = inventory.AddItem(Instantiate(item));
                if (wasAdded)
                {
                    Debug.Log("Item picked up!");
                    isEmpty = true; // Mark the chest as empty
                }
                else
                {
                    Debug.Log("Inventory full, cannot pick up item!");
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCollision(collision.gameObject, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CheckCollision(collision.gameObject, false);
    }

    private void CheckCollision(GameObject gameObject, bool state)
    {
        if (gameObject.CompareTag("Player"))
        {
            isInRange = state;
        }
    }
}