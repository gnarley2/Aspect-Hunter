using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison_Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 25f; // Speed of the projectile
    private Vector3 direction;
    private Vector3 initialPosition;
    public float maxDistance = 10f; // Initial max distance
    public GameObject poisonAoe;
    private int damage = 1;


    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the projectile in the specified direction
        transform.Translate(direction * (speed * Time.deltaTime), Space.World);
        transform.rotation = Quaternion.identity;
   
        if (Vector3.Distance(initialPosition, transform.position) >= maxDistance)
        {
            Vector3 aoePosition = transform.position;
            aoePosition.z = 0; // Ensure the z-position is set to 0
            aoePosition.y += 0.5f;
            Instantiate(poisonAoe, aoePosition, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void Initialize(Vector3 newDirection, int damage, Transform playerTransform)
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
