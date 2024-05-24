using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Player_Start : MonoBehaviour
{
    
    private SpriteRenderer spriteRenderer;
   [SerializeField] private float delayTime = 2f; // Delay time in seconds
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false; // Disable the SpriteRenderer initially
        timer = 0f; // Reset the timer
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // Increment the timer

        if (timer >= delayTime) // Check if the delay time has elapsed
        {
            spriteRenderer.enabled = true; // Enable the SpriteRenderer
            enabled = false; // Disable this script to prevent further updates
        }
    }
}