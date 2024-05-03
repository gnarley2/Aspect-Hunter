using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentInteraction : MonoBehaviour, IDamageable
{
    [Serializable]
    public class EnvironmentTriggerElement
    {
        public AspectType type;
        public string animName;
    }

    [SerializeField] private List<EnvironmentTriggerElement> Elements = new List<EnvironmentTriggerElement>();

    private Animator anim;
    private bool isDestroyed = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    public Health GetHealth()
    {
        return null;
    }

    public IDamageable.DamagerTarget GetDamagerType()
    {
        return IDamageable.DamagerTarget.Trap;
    }

    public IDamageable.KnockbackType GetKnockbackType()
    {
        return IDamageable.KnockbackType.none;
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public void TakeDamage(int damage, IDamageable.DamagerTarget damagerType, Vector2 attackDirection,
        AspectType aspectType = AspectType.None)
    {
        if (isDestroyed) return;
        
        foreach (EnvironmentTriggerElement element in Elements)
        {
            if (element.type == aspectType)
            {
                isDestroyed = true;
                anim.Play(element.animName);
                Destroy(gameObject, 2f);
                break;
            }
        }
    }
}
