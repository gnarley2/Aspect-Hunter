using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AspectManager : MonoBehaviour
{
    public static AspectManager Instance;
    public Dictionary<Aspect, int> aspectInventory = new Dictionary<Aspect, int>();
    public GameObject aspectPrefab;
    public Image[] aspectIcon;

    private void Awake()
    {
        Instance = this;
        Transform aspectParent = GameObject.Find("AspectParent").transform;
        aspectIcon = new Image[aspectParent.childCount];
        for (int i = 0; i < aspectParent.childCount; i++)
        {
            aspectIcon[i] = aspectParent.GetChild(i).Find("itembutton/icon").GetComponent<Image>();
        }
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

        UpdateAspectInventoryUI(aspect.icon);

        Debug.Log("Aspect Inventory:");
        foreach (KeyValuePair<Aspect, int> entry in aspectInventory)
        {
            Debug.Log(entry.Key.aspectName + ": " + entry.Value);
        }
    }

    void UpdateAspectInventoryUI(Sprite icon)
    {
        foreach (var aspectIcons in aspectIcon)
        {
            if (aspectIcons.sprite == null) // This finds the first empty slot
            {
                aspectIcons.sprite = icon;
                break; // Exit the loop after setting the icon
            }
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
