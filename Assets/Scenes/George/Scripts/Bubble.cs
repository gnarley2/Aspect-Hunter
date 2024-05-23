using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Bubble : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private int damage = 15;
    private Vector2 moveDirection;
    private IDamageable.DamagerTarget currentTarget = IDamageable.DamagerTarget.Enemy;

    private void OnEnable()
    {
        Invoke("Destroy", 3f);
    }

    void Start()
    {
        moveSpeed = 5f;           
    }

    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable target) && target.GetDamagerType() != currentTarget)
        {
            if (target.GetDamagerType() == currentTarget) return;
            if (target.GetDamagerType() == IDamageable.DamagerTarget.TamedMonster &&
                currentTarget == IDamageable.DamagerTarget.Player) return;
            if (target.GetDamagerType() == IDamageable.DamagerTarget.Player &&
                currentTarget == IDamageable.DamagerTarget.TamedMonster) return;
            
            target.TakeDamage(damage, currentTarget, Vector2.zero);
            Destroy();
        }
    }
}
