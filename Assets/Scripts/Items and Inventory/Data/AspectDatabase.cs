using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AspectType
{
    Fire = 1,
    Frost = 2,
    Shock = 3,
    Poison = 4,
    Water = 5,
    None = 0,
    // Combination
    Steam = 100,
    Blast = 200,
    Radiation = 300,
    Gas = 400,
    Paralysis = 500,
    Pollution = 600,
    Superconductor = 700,
    Necrotic = 800,
    Corrosion = 900,
    IceSpike = 1000,
}

public class AspectDatabase : MonoBehaviour
{
    public static AspectDatabase Instance;
    
    [System.Serializable]
    public class AspectCombination
    {
        public AspectType aspect1;
        public AspectType aspect2;
        public AspectType aspect3;

        public bool IsEqual(AspectType aspectType1, AspectType aspectType2)
        {
            return (aspect1 == aspectType1 && aspect2 == aspectType2) ||
                   (aspect2 == aspectType1 && aspect1 == aspectType2);
        }
    }

    void Awake()
    {
        Instance = this;
    }

    [SerializeField] private List<EquippableItem> EquippableAspects = new List<EquippableItem>();
    [SerializeField] private List<AspectCombination> AspectCombinations = new List<AspectCombination>();

    public EquippableItem GetEquippableAspect(AspectType aspectType)
    {
        foreach (EquippableItem item in EquippableAspects)
        {
            if (item.aspectType == aspectType)
            {
                return item;
            }
        }

        Debug.LogError("Can not find equippable aspect");
        return null;
    }

    public AspectType GetCombination(AspectType aspectType1, AspectType aspectType2)
    {
        foreach (AspectCombination combination in AspectCombinations)
        {
            if (combination.IsEqual(aspectType1, aspectType2))
            {
                return combination.aspect3;
            }
        }

        Debug.LogError("Cant find combination for {aspectType1} and {aspectType2}");
        return AspectType.None;
    }
    
    public bool IsCombination(AspectType aspectType)
    {
        foreach (AspectCombination combination in AspectCombinations)
        {
            if (combination.aspect3 == aspectType)
            {
                return true;
            }
        }

        Debug.LogError("Cant find combination");
        return false;
    }

    public AspectInventory.SingleAspectInventory CreateInventoryCombination(AspectType type)
    {
        AspectInventory.SingleAspectInventory inventory = new AspectInventory.SingleAspectInventory(type);
        foreach (AspectCombination combination in AspectCombinations)
        {
            if (combination.aspect3 == inventory.type)
            {
                inventory.isCombination = true;
                inventory.type1 = combination.aspect1;
                inventory.type2 = combination.aspect2;
                return inventory;
            }
        }

        return null;
    }
}
