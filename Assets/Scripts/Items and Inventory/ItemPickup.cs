using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
	[SerializeField] Item item;
	[SerializeField] Inventory inventory;

    private bool isInRange;
	private bool isEmpty;

    private void OnValidate()
	{
		if (inventory == null)
			inventory = FindObjectOfType<Inventory>();
	}

	private void Update()
	{
		if (isInRange)
		{
			if (!isEmpty)
			{
				inventory.AddItem(item);
                Destroy(gameObject);
				isEmpty = true;
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
