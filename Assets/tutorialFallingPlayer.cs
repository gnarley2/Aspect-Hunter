using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialFallingPlayer : MonoBehaviour
{
    public float fallSpeed = 5f; // Speed at which the object falls
    public float destroyDelay = 5f; // Delay before destroying the object after stopping

    private bool hasStopped = false; // Flag to indicate if the object has stopped


    public Animator anim;
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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
            anim.SetTrigger("Splat");
            StartCoroutine(DestroyWithDelay());
        }
    }
    private IEnumerator DestroyWithDelay()
    {
        // Delay before destroying the projectile
        yield return new WaitForSeconds(destroyDelay); // Adjust the delay time as needed
        Instantiate(prefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
