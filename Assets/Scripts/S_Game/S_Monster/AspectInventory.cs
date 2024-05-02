using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AspectInventory : MonoBehaviour
{
    public static AspectInventory Instance;
    
    [Serializable]
    public class SingleAspectInventory
    {
        public AspectType type;
        public int number = 0;

        public bool isCombination = false;
        public AspectType type1;
        public AspectType type2;

        public SingleAspectInventory(AspectType type)
        {
            this.type = type;
        }

    }
    

    public List<SingleAspectInventory> AspectInventories = new List<SingleAspectInventory>();

    private void Awake()
    {
        Instance = this;
    }

    [ContextMenu("Create Inventory based on type")]
    private void CreateInventory()
    {
        var aspectTypes = Enum.GetValues(typeof(AspectType)).Cast<AspectType>();
        AspectDatabase database = GetComponent<AspectDatabase>();
        foreach (var aspectType in aspectTypes)
        {
            SingleAspectInventory singleAspectInventory = database.CreateInventoryCombination(aspectType);
            if (singleAspectInventory == null) singleAspectInventory = new SingleAspectInventory(aspectType);
            
            AspectInventories.Add(singleAspectInventory);
        }
    }

    
    public void AddAspect(MonsterDetails details)
    {
        AddAspect(details.type, details.GetLoot());
    }
    
    public void AddAspect(AspectType type, int amount)
    {
        int index = FindAspect(type);
        AspectInventories[index].number += amount;
    }

    bool CanUseAspect(AspectType type, int amount, out int index)
    {
        index = FindAspect(type);
        return CanUseAspect(index, amount);
    }
    
    bool CanUseAspect(int index, int amount)
    {
        return AspectInventories[index].number >= amount;
    }

    public bool UseAspect(AspectType type, int amount)
    {
        int index = FindAspect(type);
        
        if (AspectInventories[index].isCombination)
        {
            if (CanUseAspect(AspectInventories[index].type1, amount, out int index1) &&
                CanUseAspect(AspectInventories[index].type2, amount, out int index2))
            {
                return UseAspect(index1, amount) && UseAspect(index2, amount);
            }
        }
        
        return UseAspect(index, amount);
    }
    
    public bool UseAspect(int index, int amount)
    {
        if (AspectInventories[index].number >= amount)
        {
            AspectInventories[index].number -= amount;
            return true;
        }

        return false;
    }
    

    public int FindAspect(AspectType type)
    {
        for (int i = 0; i < AspectInventories.Count; i++)
        {
            if (AspectInventories[i].type == type)
            {
                return i;
            }
        }

        Debug.LogError("Can't not find aspect");
        return -1;
    }
}
