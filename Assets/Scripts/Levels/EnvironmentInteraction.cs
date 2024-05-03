using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentInteraction : MonoBehaviour, IDamageable
{
    [SerializeField] private bool canDestroy = true;
    
    [Serializable]
    public class EnvironmentTriggerElement
    {
        public AspectType type;
        public string animName;
    }

    [SerializeField] private List<EnvironmentTriggerElement> Elements = new List<EnvironmentTriggerElement>();

    public Action OnTrigger;
    private Animator anim;
    private bool isActivated = false;

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
        if (isActivated) return;
        foreach (EnvironmentTriggerElement element in Elements)
        {
            if (element.type == aspectType)
            {
                isActivated = true;
                anim.Play(element.animName);
                OnTrigger?.Invoke();
                if (canDestroy) Destroy(gameObject, 2f);
                break;
            }
        }
    }
}
