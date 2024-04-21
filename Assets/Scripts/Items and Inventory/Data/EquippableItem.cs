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

    public void Equip(CharacterPanel c)
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
            // Get the name of the item
            string itemName = ItemName; // Assuming ItemName is the name of the item

            // Access PlayerCombat script
            GameObject player = GameObject.FindWithTag("Player");
            PlayerCombat playerCombat = player.GetComponent<PlayerCombat>();

            // Check the name of the item and adjust currentType accordingly
            if (itemName == "FireAspect")
            {
                playerCombat.currentProjectileIndex = 0;
            }
            else if (itemName == "FrostAspect")
            {
                playerCombat.currentProjectileIndex = 1;
            }
            else if (itemName == "ShockAspect")
            {
                playerCombat.currentProjectileIndex = 3;
            }
            else if (itemName == "PoisonAspect")
            {
                playerCombat.currentProjectileIndex = 2;
            }
            else if (itemName == "WaterAspect")
            {
                playerCombat.currentProjectileIndex = 4;
            }
        }


    }

    public void Unequip(CharacterPanel c)
    {
        //add code to remove buffs
        if (EquipmentType == EquipmentType.LanternBug)
        {
            Debug.Log("Finding LanternBug instance...");
            GameObject lanternBugInstance = GameObject.Find("LanternBug_Active(Clone)");


            if (lanternBugInstance != null)
            {
                // Activate the LanternBug instance if it's inactive
                if (!lanternBugInstance.activeSelf)
                {
                    lanternBugInstance.SetActive(true);
                }

                // Now destroy the LanternBug instance
                Destroy(lanternBugInstance);
            }
        }
    

        if (EquipmentType == EquipmentType.FlashLight && FlashLightPrefab != null)
        {
            GameObject flashlightInstance = GameObject.FindWithTag("FlashLight");
           
            Destroy(flashlightInstance);

        }
    }
}