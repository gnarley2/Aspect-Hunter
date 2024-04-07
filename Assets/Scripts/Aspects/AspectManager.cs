using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectManager : MonoBehaviour
{
    public static AspectManager Instance;
    public Dictionary<Aspect, int> aspectInventory = new Dictionary<Aspect, int>();


    private void Awake()
    {
        Instance = this;
    }

    public void Add(Aspect aspect)
    {
        if (aspectInventory.ContainsKey(aspect))
        {
            aspectInventory[aspect]++;
        }
        else
        {
            aspectInventory.Add(aspect, 1);
        }

        InventoryManager.Instance.UpdateAspectInventoryUI(aspect);

        Debug.Log("Aspect Inventory:");
        foreach (KeyValuePair<Aspect, int> entry in aspectInventory)
        {
            Debug.Log(entry.Key.aspectName + ": " + entry.Value);
        }

    }

    public void Remove(Aspect aspect)
    {
        if (aspectInventory.ContainsKey(aspect))
        {
            if (aspectInventory[aspect] > 1)
            {
                aspectInventory[aspect]--;
            }
            else
            {
                aspectInventory.Remove(aspect);
            }


        }
    }

}
