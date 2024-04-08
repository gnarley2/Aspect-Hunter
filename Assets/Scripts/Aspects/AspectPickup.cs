using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectPickup : MonoBehaviour
{
    public Aspect Aspect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Picking up aspect...");
            Pickup();
        }
    }

    void Pickup()
    {
        AspectManager.Instance.Add(Aspect);
        Destroy(gameObject);
    }
}