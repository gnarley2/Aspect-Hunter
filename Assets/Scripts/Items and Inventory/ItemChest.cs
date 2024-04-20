using UnityEngine;
using UnityEngine.UI;

public class ItemChest : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] Inventory inventory;
    [SerializeField] KeyCode itemPickupKeyCode = KeyCode.E;
    [SerializeField] Sprite closeChest;
    [SerializeField] Sprite openChest;
    SpriteRenderer chestImage;
    private bool isInRange;
    private bool isEmpty;

    private void OnValidate()
    {
        if (inventory == null)
            inventory = FindObjectOfType<Inventory>();

    }

    void Start()
    {
        chestImage = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isInRange && !isEmpty && Input.GetKeyDown(itemPickupKeyCode) && !inventory.IsFull())
        {
            inventory.AddItem(Instantiate(item));
            isEmpty = true;
        }

        if (isEmpty)
            chestImage.sprite = openChest;
        else 
            chestImage.sprite = closeChest;
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