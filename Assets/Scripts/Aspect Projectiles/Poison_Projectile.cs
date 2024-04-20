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
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // Move the projectile in the specified direction
        transform.Translate(direction * (speed * Time.deltaTime), Space.World);

        // Check if the projectile has reached the maximum distance
        if (Vector3.Distance(initialPosition, transform.position) >= maxDistance)
        {
            // Instantiate the poison AOE effect
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
        if (other.TryGetComponent<IDamageable>(out IDamageable target)&& other.tag=="Enemy")
        {
            if (target.GetDamagerType() == IDamageable.DamagerTarget.Player) return;

            target.TakeDamage(damage, IDamageable.DamagerTarget.Player, Vector2.zero);
            Instantiate(poisonAoe, transform.position, Quaternion.identity);
            Destroy(gameObject); // Destroy the projectile
        }

    }
}
