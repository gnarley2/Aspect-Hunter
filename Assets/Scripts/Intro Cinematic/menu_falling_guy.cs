using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_falling_guy : MonoBehaviour
{
    public float fallSpeed = 5f;
    private bool hasStopped = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStopped)
        {
            // Move the game object straight downwards
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Stop Box")
        {
            hasStopped = true; // Stop the movement
            //anim.SetTrigger("Splat");
           // StartCoroutine(DestroyWithDelay());
        }
    }
}
