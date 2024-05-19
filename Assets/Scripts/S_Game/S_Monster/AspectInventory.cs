using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AspectInventory : MonoBehaviour
{
    public static AspectInventory Instance;
    private string errorMessage = "";
    
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
        foreach (SingleAspectInventory aspect in AspectInventories)
        {
            aspect.number = 100;
        }
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

    private void Start()
    {
        Inventory.Instance.OnAddItem += OnAddItem;
    }

    private void OnAddItem(Item addedItem)
    {
        EquippableItem item = addedItem as EquippableItem;
        if (item && item.EquipmentType == EquipmentType.Aspect)
        {
            AddAspect(item.aspectType, 4);
        }
    }

    void ResetErrorMessage()
    {
        errorMessage = "";
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
        if (CanUseAspect(index, amount)) return true;

        errorMessage += $"Not enough {amount} {type}\n";
        return false;
    }
    
    bool CanUseAspect(int index, int amount)
    {
        return AspectInventories[index].number >= amount;
    }

    public bool UseAspect(AspectType type, int amount)
    {
        ResetErrorMessage();

        int index = FindAspect(type);

        if (AspectInventories[index].isCombination)
        {
            bool canUse1 = CanUseAspect(AspectInventories[index].type1, amount, out int index1);
            bool canUse2 = CanUseAspect(AspectInventories[index].type2, amount, out int index2);

            if (canUse1 && canUse2)
            {
                AspectInventories[index1].number -= amount;
                AspectInventories[index2].number -= amount;
                FindObjectOfType<HUDManager>()?.UpdateAspectCount();
                return true;
            }

            InformationPanel.Instance.ShowInformation(errorMessage);
            return false;
        }
        else
        {
            if (CanUseAspect(index, amount))
            {
                AspectInventories[index].number -= amount;
                FindObjectOfType<HUDManager>()?.UpdateAspectCount();
                return true;
            }
        }

        InformationPanel.Instance.ShowInformation($"Not enough {amount} {type}");
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

    public int GetAspectCount(AspectType type)
    {
        int index = FindAspect(type);
        if (index != -1)
        {
            return AspectInventories[index].number;
        }
        return 0;
    }
}
