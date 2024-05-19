using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class CharacterPanel : MonoBehaviour
{
    [FormerlySerializedAs("inventory")] [SerializeField] InventoryUI inventoryUI;
    [SerializeField] EquipmentPanel equipmentPanel;
    [SerializeField] ItemTooltip itemTooltip;
    [SerializeField] Image draggableItem;
    [SerializeField] HUDManager hudManager;

    private ItemSlot dragItemSlot;

    private void Awake()
    {
        if (itemTooltip == null)
            itemTooltip = FindObjectOfType<ItemTooltip>();


        // Pointer Enter
        inventoryUI.OnPointerEnterEvent += ShowTooltip;
        equipmentPanel.OnPointerEnterEvent += ShowTooltip;
        // Pointer Exit
        inventoryUI.OnPointerExitEvent += HideTooltip;
        equipmentPanel.OnPointerExitEvent += HideTooltip;
        // Begin Drag
        inventoryUI.OnBeginDragEvent += BeginDrag;
        equipmentPanel.OnBeginDragEvent += BeginDrag;
        // End Drag
        inventoryUI.OnEndDragEvent += EndDrag;
        equipmentPanel.OnEndDragEvent += EndDrag;
        // Drag
        inventoryUI.OnDragEvent += Drag;
        equipmentPanel.OnDragEvent += Drag;
        // Drop
        inventoryUI.OnDropEvent += Drop;
        equipmentPanel.OnDropEvent += Drop;
    }

    private void ShowTooltip(ItemSlot itemSlot)
    {
        EquippableItem equippableItem = itemSlot.Item as EquippableItem;
        if (equippableItem != null)
        {
            itemTooltip.ShowTooltip(equippableItem);
        }
    }

    private void HideTooltip(ItemSlot itemSlot)
    {
        if (itemTooltip.gameObject.activeSelf)
        {
            itemTooltip.HideTooltip();
        }
    }

    private void BeginDrag(ItemSlot itemSlot)
    {
        if (itemSlot.Item != null)
        {
            dragItemSlot = itemSlot;
            draggableItem.sprite = itemSlot.Item.Icon;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.gameObject.SetActive(true);
        }
    }

    private void Drag(ItemSlot itemSlot)
    {
        draggableItem.transform.position = Input.mousePosition;
    }

    private void EndDrag(ItemSlot itemSlot)
    {
        dragItemSlot = null;
        draggableItem.gameObject.SetActive(false);
    }

    private void Drop(ItemSlot dropItemSlot)
    {
        if (dragItemSlot == null) return;

        if (dropItemSlot.CanReceiveItem(dragItemSlot.Item) && dragItemSlot.CanReceiveItem(dropItemSlot.Item))
        {
            EquippableItem dragItem = dragItemSlot.Item as EquippableItem;
            EquippableItem dropItem = dropItemSlot.Item as EquippableItem;

            EquipmentSlot dropEquipmentSlot = dropItemSlot as EquipmentSlot;
            EquipmentSlot dragEquipmentSlot = dragItemSlot as EquipmentSlot;

            if (dropEquipmentSlot)
            {
                if (dropItem != null)
                {
                    dropItem.Unequip(this);
                    hudManager.UpdateAspectSlot(dropItem, false, dropEquipmentSlot.index); // Update HUD for the unequipped item
                }
                if (dragItem != null)
                {
                    dragItem.Equip(this);
                    hudManager.UpdateAspectSlot(dragItem, true, dropEquipmentSlot.index); // Update HUD for the equipped item
                }
            }

            if (dragEquipmentSlot)
            {
                if (dropItem != null)
                {
                    dropItem.Equip(this);
                    hudManager.UpdateAspectSlot(dropItem, true, dragEquipmentSlot.index); // Update HUD for the equipped item
                }
                if (dragItem != null)
                {
                    dragItem.Unequip(this);
                    hudManager.UpdateAspectSlot(dragItem, false, dragEquipmentSlot.index); // Update HUD for the unequipped item
                }
            }

            Item draggedItem = dragItemSlot.Item;
            dragItemSlot.Item = dropItemSlot.Item;
            dropItemSlot.Item = draggedItem;
        }
    }

    public void Equip(EquippableItem item)
    {
        if (inventoryUI.RemoveItem(item))
        {
            EquippableItem previousItem;
            if (equipmentPanel.AddItem(item, out previousItem))
            {
                if (previousItem != null)
                {
                    previousItem.Unequip(this);
                    inventoryUI.AddItem(previousItem);
                }
                item.Equip(this);
            }
            else
            {
                inventoryUI.AddItem(item);
            }
        }
    }

    public void Unequip(EquippableItem item)
    {
        if (!inventoryUI.IsFull() && equipmentPanel.RemoveItem(item))
        {
            item.Unequip(this);
            inventoryUI.AddItem(item);
        }
    }
}