using UnityEngine;
using UnityEngine.Serialization;

public class ItemChest : MonoBehaviour
{
    [SerializeField] Item item;
    [FormerlySerializedAs("inventory")] [SerializeField] InventoryUI inventoryUI;
    [SerializeField] KeyCode itemPickupKeyCode = KeyCode.E;
    private bool isInRange;
    private bool isEmpty;
    private void OnValidate()
    {
        if (inventoryUI == null)
            inventoryUI = FindObjectOfType<InventoryUI>();

    }
    private void Update()
    {
        if (isInRange && !isEmpty && Input.GetKeyDown(itemPickupKeyCode) && !inventoryUI.IsFull())
        {
            inventoryUI.AddItem(Instantiate(item));
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