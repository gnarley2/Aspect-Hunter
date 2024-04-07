using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Data/Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public string category;
    public int value;
    public Sprite icon;
}
