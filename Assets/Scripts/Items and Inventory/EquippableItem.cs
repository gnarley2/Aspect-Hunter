using UnityEngine;
using Kryz.CharacterStats;
using UnityEditor;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Transform = UnityEngine.Transform;
using static PlayerCombat;

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
    [SerializeField] private ProjectileType aspectProjectileType = ProjectileType.Fire; // Assuming ProjectileType is an enum
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


        if (EquipmentType == EquipmentType.Aspect)
        {
            GameObject player = GameObject.FindWithTag("Player");
            PlayerCombat playerCombat = player.GetComponent<PlayerCombat>();


            EquippableItem aspectItem = this;
            if (aspectItem.ItemName == "FireAspect")
            {
                playerCombat.currentProjectileType = ProjectileType.Fire;
            }
            if (aspectItem.ItemName == "FrostAspect")
            {
                playerCombat.currentProjectileType = ProjectileType.Frost;
            }
            if (aspectItem.ItemName == "ShockAspect")
            {
                playerCombat.currentProjectileType = ProjectileType.Shock;
            }
            if (aspectItem.ItemName == "PoisonAspect")
            {
                playerCombat.currentProjectileType = ProjectileType.Poison;
            }
            if (aspectItem.ItemName == "WaterAspect")
            {
                playerCombat.currentProjectileType = ProjectileType.Water;
            }
        }


    }

    public void Unequip(InventoryManager c)
    {
        //add code to remove buffs
        if (EquipmentType == EquipmentType.FlashLight && FlashLightPrefab != null)
        {
            GameObject flashlightInstance = GameObject.FindWithTag("FlashLight");
            if (flashlightInstance != null)
            {
                // Destroy the instantiated Flashlight GameObject
                Destroy(flashlightInstance);
            }
        }

        if (EquipmentType == EquipmentType.LanternBug && LanternBugPrefab != null)
        {
            GameObject lanterbugInstance = GameObject.FindWithTag("LanternBug");
            if (lanterbugInstance != null)
            {
                // Destroy the instantiated Flashlight GameObject
                Destroy(lanterbugInstance);
            }

        }
    }
}