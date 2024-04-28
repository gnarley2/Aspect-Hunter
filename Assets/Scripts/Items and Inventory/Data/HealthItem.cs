using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Item/HealthItem")]
public class HealthItem : UsableItem
{
    public int healAmount = 10;
    protected Health playerHealth;

    public override void Initialize(Core core)
    {
        base.Initialize(core);
        playerHealth = core.GetCoreComponent<Health>();
    }

    public bool CanUse()
    {
        if (playerHealth.GetPercent() >= 1)
        {
            InformationPanel.Instance.ShowInformation("Player Health is full");
            return false;
        }
        return true;
    }

    public override bool Use()
    {
        if (!CanUse()) return false;
        
        playerHealth.Heal(healAmount);
        return true;
    }
}
