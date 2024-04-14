using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 25f; // Speed of the projectile
    private Vector3 direction;
    private Vector3 initialPosition;
    public float maxDistance = 10f;
    public GameObject hitAnimation;
    private int damage = 10;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        // Move the projectile in the specified direction
        transform.Translate(direction * (speed * Time.deltaTime), Space.World);

        if (Vector3.Distance(initialPosition, transform.position) >= maxDistance)
        {
           // Instantiate(hitAnimation, transform.position, Quaternion.identity);
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
        // Optionally, rotate the projectile to face the direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(damage, IDamageable.DamagerTarget.Player, Vector2.zero);
        }

        if (other.tag == "Item"||other.tag=="Aspect")
        {
            Instantiate(hitAnimation, transform.position, Quaternion.identity);
        }

        if (other.name != "Player" && other.tag != "Light"&& other.tag!="Item"&&other.tag!="Aspect")
        {
            Instantiate(hitAnimation, transform.position, Quaternion.identity);
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
