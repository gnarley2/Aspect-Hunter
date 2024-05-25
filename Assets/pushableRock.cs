using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushableRock : MonoBehaviour
{
    private Rigidbody2D rb;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
         
            
                // Deactivate the Rigidbody2D component
                rb.isKinematic = true;
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
            
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
      
      
                // Reactivate the Rigidbody2D component
                rb.isKinematic = false;
            
        }
    }
}
