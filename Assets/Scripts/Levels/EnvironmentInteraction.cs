using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentInteraction : MonoBehaviour, IDamageable
{
    [SerializeField] private bool canPlayOnce = true;
    [SerializeField] private bool canDestroy = true;
    [SerializeField] private bool canPush = false;

    [Serializable]
    public class EnvironmentTriggerElement
    {
        public AspectType type;
        public string animName;
    }

    [SerializeField] private List<EnvironmentTriggerElement> Elements = new List<EnvironmentTriggerElement>();

    public Action OnTrigger;
    private Animator anim;
    private Rigidbody2D rb;
    private bool isActivated = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Check if the current game object's name is "Fire_Rock"
            if (gameObject.name == "Fire_Rock")
            {
                // Deactivate the Rigidbody2D component
                rb.isKinematic = true;
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the collided object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Check if the current game object's name is "Fire_Rock"
            if (gameObject.name == "Fire_Rock")
            {
                // Reactivate the Rigidbody2D component
                rb.isKinematic = false;
            }
        }
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
        if (isActivated && canPlayOnce) return;

        foreach (EnvironmentTriggerElement element in Elements)
        {
            if (element.type == aspectType)
            {
                isActivated = true;
                PlayAnim(element);
                Push(attackDirection);

                OnTrigger?.Invoke();

                Destroy();
                break;
            }
        }
    }

    void Destroy()
    {
        if (canDestroy) Destroy(gameObject, 2f);
    }

    void PlayAnim(EnvironmentTriggerElement element)
    {
        if (!String.IsNullOrWhiteSpace(element.animName)) anim.Play(element.animName);
    }

    void Push(Vector2 attackDirection)
    {
        if (!canPush) return;
        Debug.Log(attackDirection);
        float force = 100f;
        rb.AddForce(attackDirection * force, ForceMode2D.Impulse);
    }


}
