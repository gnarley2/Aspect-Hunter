using UnityEngine;
public class ItemChest : MonoBehaviour
{
	[SerializeField] Item item;
	[SerializeField] Inventory inventory;
	[SerializeField] KeyCode itemPickupKeyCode = KeyCode.E;
	private bool isInRange;
	private bool isEmpty;
	private void OnValidate()
	{
		if (inventory == null)
			inventory = FindObjectOfType<Inventory>();

	}
	private void Update()
	{
		if (isInRange && !isEmpty && Input.GetKeyDown(itemPickupKeyCode) && !inventory.IsFull())
		{
			inventory.AddItem(Instantiate(item));
			isEmpty = true;
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