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
    private bool isIdle = false;

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
        if (isIdle) return;
        
        if (Vector2.Distance(transform.position, currentDestination) < 0.1f)
        {
            StartCoroutine(Idle());
        }
        else
        {
            rb.velocity = (currentDestination - (Vector2)transform.position).normalized * speed * Time.fixedDeltaTime;
        }
    }
    
    IEnumerator Idle()
    {
        isIdle = true;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(2f);

        isIdle = false;
        GetRandomDestination();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
