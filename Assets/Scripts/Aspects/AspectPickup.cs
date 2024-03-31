using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectPickup : MonoBehaviour
{
    public Aspect Aspect;
    public Collider2D Collider;

    void Start()
    {

    }

    void Pickup()
    {
        AspectManager.Instance.Add(Aspect);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Pickup();

        }
    }
}
