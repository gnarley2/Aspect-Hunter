using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Monster : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float speed;

    private Vector2 currentDestination;
    private Vector2 startPos;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        startPos = transform.position;
        GetRandomDestination();
    }

    void GetRandomDestination()
    {
        currentDestination = Random.insideUnitCircle * radius + startPos;
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        if (Vector2.Distance(transform.position, currentDestination) < 0.1f)
        {
            GetRandomDestination();
        }
        else
        {
            rb.velocity = (currentDestination - (Vector2)transform.position).normalized * speed * Time.fixedDeltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
