using UnityEngine;
using UnityEditor;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Transform = UnityEngine.Transform;
using System;

public enum EquipmentType
{
    LightSource, // New generalized type
    Flare,
    Key,
    Aspect,
}

public enum LightSourceType
{
    Lantern,
    FlashLight,
    LanternBug,
    None
}

[CreateAssetMenu]
public class EquippableItem : Item
{
    public EquipmentType EquipmentType;
    public LightSourceType LightSourceType; // New property for light source subtype
    public AspectType aspectType = AspectType.None;
    public string Description;
    public GameObject LanternBugPrefab;
    public GameObject LanternPrefab;
    public GameObject FlashLightPrefab;
    public event Action OnEquipped;
    public event Action OnUnequipped;

    public void Equip(CharacterPanel c)
    {
        if (EquipmentType == EquipmentType.LightSource)
        {
            switch (LightSourceType)
            {
                case LightSourceType.LanternBug:
                    if (LanternBugPrefab != null)
                    {
                        GameObject lanternBug = Instantiate(LanternBugPrefab);
                        lanternBug.transform.parent = GameManager.Instance.transform;
                    }
                    break;
                case LightSourceType.Lantern:
                    if (LanternPrefab != null)
                    {
                        GameObject player = GameObject.FindWithTag("Player");
                        GameObject lanternInstance = Instantiate(LanternPrefab);
                        lantern lanternScript = lanternInstance.GetComponent<lantern>();

                        if (lanternScript != null)
                        {
                            lanternScript.isLanternEquipped = true; // Set the boolean variable to true
                        }
                    }
                    break;
                case LightSourceType.FlashLight:
                    if (FlashLightPrefab != null)
                    {
                        GameObject flashlight = Instantiate(FlashLightPrefab);
                        flashlight.transform.parent = GameManager.Instance.transform;
                    }
                    break;
            }
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

        OnEquipped?.Invoke();
    }

    public void Unequip(CharacterPanel c)
    {
        if (EquipmentType == EquipmentType.LightSource)
        {
            switch (LightSourceType)
            {
                case LightSourceType.LanternBug:
                    Debug.Log("Finding LanternBug instance...");
                    GameObject lanternBugInstance = GameObject.Find("LanternBug_Active(Clone)");

                    if (lanternBugInstance != null)
                    {
                        if (!lanternBugInstance.activeSelf)
                        {
                            lanternBugInstance.SetActive(true);
                        }

                        Destroy(lanternBugInstance);
                    }
                    break;
                case LightSourceType.FlashLight:
                    GameObject flashlightInstance = GameObject.Find("Flashlight_Active(Clone)");
                    if (flashlightInstance != null)
                    {
                        Destroy(flashlightInstance);
                    }
                    break;
                case LightSourceType.Lantern:
                    // Add any specific unequip logic for lantern if necessary
                    break;
            }
        }

        OnUnequipped?.Invoke();
    }
}