using UnityEngine;
using Kryz.CharacterStats;

public enum EquipmentType
{
	Lantern,
	Aspect,
	FireAspect,
	FrostAspect
}

[CreateAssetMenu]
public class EquippableItem : Item
{
	public string ItemName;
	public EquipmentType EquipmentType;

	public void Equip(InventoryManagerUI c)
	{
		//add code for buffs
	}

	public void Unequip(InventoryManagerUI c)
	{
		//add code to remove buffs
	}
}