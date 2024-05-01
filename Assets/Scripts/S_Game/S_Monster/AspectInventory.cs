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
    }

    public List<SingleAspectInventory> AspectInventories = new List<SingleAspectInventory>();

    private void Awake()
    {
        Instance = this;
        
        var aspectTypes = Enum.GetValues(typeof(AspectType)).Cast<AspectType>();
        foreach (var aspectType in aspectTypes)
        {
            SingleAspectInventory singleAspectInventory = new SingleAspectInventory();
            singleAspectInventory.type = aspectType;
            AspectInventories.Add(singleAspectInventory);
        }
    }
    
    public void AddAspect(AspectType type, int amount)
    {
        int index = FindAspect(type);
        AspectInventories[index].number += amount;
    }

    public void UseAspect(AspectType type, int amount)
    {
        int index = FindAspect(type);
        AspectInventories[index].number -= amount;
    }
    
    
    public bool HasAspect(AspectType type)
    {
        int index = FindAspect(type);
        return AspectInventories[index].number > 0;
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
