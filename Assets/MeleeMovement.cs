using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMovement : MonoBehaviour
{
    private Vector3 direction;
    private GameObject player;
    private Transform playerTransform;
    [SerializeField] public float radius = 2f; // Radius of the arc
    [SerializeField] public float speed = 20f; // Speed of movement along the arc
    [SerializeField] private float maxAngle = 20f;
    private float angle = 0f; // Current angle on the circle
  
    private float initialAngle = 0f; // Initial angle for the arc

    // Start is called before the first frame update
    void Start()
    {
       
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Ensure that playerTransform is not null before proceeding
        if (playerTransform == null)
        {
            return;
        }

        MeleeArc();
    }

    void MeleeArc()
    {
        angle += speed * Time.deltaTime;
        float currentAngle = initialAngle + angle;
        float x = Mathf.Cos(currentAngle * Mathf.Deg2Rad) * radius;
        float y = Mathf.Sin(currentAngle * Mathf.Deg2Rad) * radius;
        transform.position = playerTransform.position + new Vector3(x, y, 0f);

        if (angle >= maxAngle)
        {
            Destroy(gameObject); // Destroy the melee object
        }
    }
    public void SetInitialDirection(Vector3 initialDirection)
    { 
        initialAngle = Mathf.Atan2(initialDirection.y, initialDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, initialDirection);
    

        
    }
}
