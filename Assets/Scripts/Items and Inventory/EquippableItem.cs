using UnityEngine;
using Kryz.CharacterStats;

public enum EquipmentType
{
    Lantern,
    FlashLight,
    LanternBug,
    Flare,

    Key,

    Aspect,

}

[CreateAssetMenu]
public class EquippableItem : Item
{
    public EquipmentType EquipmentType;

    public void Equip(InventoryManager c)
    {
        //add code for buffs
    }

    public void Unequip(InventoryManager c)
    {
        //add code to remove buffs
    }
}