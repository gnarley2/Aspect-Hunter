using UnityEngine;
using Kryz.CharacterStats;
using UnityEditor;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Transform = UnityEngine.Transform;

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
    public GameObject LanternBugPrefab;
    public GameObject LanternPrefab;
    public GameObject FlashLightPrefab;

    public void Equip(InventoryManager c)
    {
        //add code for buffs
        if (EquipmentType == EquipmentType.LanternBug && LanternBugPrefab != null)
        { 
            Instantiate(LanternBugPrefab);

        }
        if (EquipmentType == EquipmentType.Lantern && LanternPrefab != null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            GameObject lanternInstance = Instantiate(LanternPrefab);
            lantern lanternScript = lanternInstance.GetComponent<lantern>();

            if (lanternScript != null)
            {
                lanternScript.isLanternEquipped = true; // Set the boolean variable to true
            }
        }
        if (EquipmentType == EquipmentType.FlashLight && FlashLightPrefab != null)
        {
          Instantiate(FlashLightPrefab);

        }



    }

    public void Unequip(InventoryManager c)
    {
        //add code to remove buffs
    }
}