using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menufallingitems : MonoBehaviour
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
            transform.Translate(Vector3.up * fallSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Stop Box2")
        {
            hasStopped = true; // Stop the movement
            //anim.SetTrigger("Splat");
            StartCoroutine(DestroyWithDelay());
        }
    }
    private IEnumerator DestroyWithDelay()
    {
        // Delay before destroying the projectile
        yield return new WaitForSeconds(0.05f); // Adjust the delay time as needed

        Destroy(gameObject);
    }
}
