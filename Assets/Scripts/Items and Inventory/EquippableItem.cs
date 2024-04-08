using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    Aspect,
    Lantern
}

[CreateAssetMenu]
public class EquippableItem : Item
{
    public ItemType ItemType;
}
