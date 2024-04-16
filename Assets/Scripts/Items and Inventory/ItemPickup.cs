using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
	[SerializeField] Item item;
	[SerializeField] Inventory inventory;

    private bool isInRange;

    private void OnValidate()
	{
		if (inventory == null)
			inventory = FindObjectOfType<Inventory>();
	}

	private void Update()
	{
		if (isInRange && !inventory.IsFull())
		{
            inventory.AddItem(Instantiate(item));
            Destroy(gameObject);
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
