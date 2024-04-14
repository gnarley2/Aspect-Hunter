using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison_Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 25f; // Speed of the projectile
    private Vector3 direction;
    private Vector3 initialPosition;
    private float maxDistance = 10f; // Initial max distance
    public GameObject poisonAoe;
    private int damage = 1;
    private Transform playerTransform; // Reference to the player's transform
    public float distanceMod = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Find the player object
    }

    // Update is called once per frame
    void Update()
    {
        // Update maxDistance dynamically based on player's position and mouse click
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float distanceToMouse = Vector3.Distance(playerTransform.position, mousePosition);
            maxDistance = distanceToMouse * distanceMod;
        }

        // Move the projectile in the specified direction
        transform.Translate(direction * (speed * Time.deltaTime), Space.World);

        // Freeze rotation
        transform.rotation = Quaternion.identity;

        if (Vector3.Distance(initialPosition, transform.position) >= maxDistance)
        {
            Instantiate(poisonAoe, transform.position, Quaternion.identity);
            Destroy(gameObject); // Destroy the projectile
        }
    }

    public void Initialize(Vector3 newDirection, int damage)
    {
        SetDirection(newDirection);
        this.damage = damage;
    }

    // Set the direction of movement for the projectile
    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(damage, IDamageable.DamagerTarget.Player, Vector2.zero);
            Instantiate(poisonAoe, transform.position, Quaternion.identity);
            Destroy(gameObject); // Destroy the projectile
        }
        if (other.name != "Player" && other.tag != "Light" && other.tag != "Item" && other.tag != "Aspect" && other.tag != "Projectile")
        {
            Instantiate(poisonAoe, transform.position, Quaternion.identity);
            Destroy(gameObject); // Destroy the projectile
        }
    }
}
