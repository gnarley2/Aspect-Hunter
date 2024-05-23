using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UsableItem : Item
{
    protected Core playerCore;

    public virtual void Initialize(Core core)
    {
        playerCore = core;
    }
    
    public abstract bool Use();
}
