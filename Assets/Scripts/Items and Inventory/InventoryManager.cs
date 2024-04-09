using UnityEngine;
using UnityEngine.UI;
using Kryz.CharacterStats;

public class InventoryManager : MonoBehaviour
{
	public CharacterStat Strength;
	public CharacterStat Agility;
	public CharacterStat Intelligence;
	public CharacterStat Vitality;

	[SerializeField] Inventory inventory;
	[SerializeField] EquipmentPanel equipmentPanel;
	[SerializeField] StatPanel statPanel;
	[SerializeField] ItemTooltip itemTooltip;
	[SerializeField] Image draggableItem;

	private ItemSlot dragItemSlot;

	private void Awake()
	{
		if (itemTooltip == null)
			itemTooltip = FindObjectOfType<ItemTooltip>();

		statPanel.SetStats(Strength, Agility, Intelligence, Vitality);
		statPanel.UpdateStatValues();

		// Setup Events:
		// Right Click
		inventory.OnRightClickEvent += Equip;
		equipmentPanel.OnRightClickEvent += Unequip;
		// Pointer Enter
		inventory.OnPointerEnterEvent += ShowTooltip;
		equipmentPanel.OnPointerEnterEvent += ShowTooltip;
		// Pointer Exit
		inventory.OnPointerExitEvent += HideTooltip;
		equipmentPanel.OnPointerExitEvent += HideTooltip;
		// Begin Drag
		inventory.OnBeginDragEvent += BeginDrag;
		equipmentPanel.OnBeginDragEvent += BeginDrag;
		// End Drag
		inventory.OnEndDragEvent += EndDrag;
		equipmentPanel.OnEndDragEvent += EndDrag;
		// Drag
		inventory.OnDragEvent += Drag;
		equipmentPanel.OnDragEvent += Drag;
		// Drop
		inventory.OnDropEvent += Drop;
		equipmentPanel.OnDropEvent += Drop;
	}

	private void Equip(ItemSlot itemSlot)
	{
		EquippableItem equippableItem = itemSlot.Item as EquippableItem;
		if (equippableItem != null)
		{
			Equip(equippableItem);
		}
	}

	private void Unequip(ItemSlot itemSlot)
	{
		EquippableItem equippableItem = itemSlot.Item as EquippableItem;
		if (equippableItem != null)
		{
			Unequip(equippableItem);
		}
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
		this.enabled = false;
	}

	private void Drop(ItemSlot dropItemSlot)
	{
		if (dropItemSlot.CanReceiveItem(dragItemSlot.Item) && dragItemSlot.CanReceiveItem(dropItemSlot.Item))
		{
			EquippableItem dragItem = dragItemSlot.Item as EquippableItem;
			EquippableItem dropItem = dropItemSlot.Item as EquippableItem;

			if (dropItemSlot is EquipmentSlot)
			{
				if (dragItem != null) dragItem.Equip(this);
				if (dropItem != null) dropItem.Unequip(this);
			}
			if (dragItemSlot is EquipmentSlot)
			{
				if (dragItem != null) dragItem.Unequip(this);
				if (dropItem != null) dropItem.Equip(this);
			}
			statPanel.UpdateStatValues();

			Item draggedItem = dragItemSlot.Item;
			dragItemSlot.Item = dropItemSlot.Item;
			dropItemSlot.Item = draggedItem;
		}
	}

	public void Equip(EquippableItem item)
	{
		if (inventory.RemoveItem(item))
		{
			EquippableItem previousItem;
			if (equipmentPanel.AddItem(item, out previousItem))
			{
				if (previousItem != null)
				{
					inventory.AddItem(previousItem);
					previousItem.Unequip(this);
					statPanel.UpdateStatValues();
				}
				item.Equip(this);
				statPanel.UpdateStatValues();
			}
			else
			{
				inventory.AddItem(item);
			}
		}
	}

	public void Unequip(EquippableItem item)
	{
		if (!inventory.IsFull() && equipmentPanel.RemoveItem(item))
		{
			item.Unequip(this);
			statPanel.UpdateStatValues();
			inventory.AddItem(item);
		}
	}
}
