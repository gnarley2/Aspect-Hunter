using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necrotic_Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 25f; // Speed of the projectile
    private AspectType m_AspectType = AspectType.Fire;
    private Vector3 direction;
    private Vector3 initialPosition;
    public float maxDistance = 10f;
    public GameObject hitAnimation;
    private int damage = 10;

    private Transform targetTransform;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // Move the projectile in the specified direction
        transform.Translate(direction * (speed * Time.deltaTime), Space.World);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);


        if (Vector3.Distance(initialPosition, transform.position) >= maxDistance)
        {

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
            if (target.GetDamagerType() == IDamageable.DamagerTarget.Player) return;

            target.TakeDamage(damage, IDamageable.DamagerTarget.Player, Vector2.zero, m_AspectType);
            // Destroy(gameObject);
            Transform enemyTransform = other.transform;
            float yOffset = 1.0f;


            if (other.tag == "Enemy")
            {
                GameObject hitAnimationInstance = Instantiate(hitAnimation,
                    enemyTransform.position + Vector3.up * yOffset, Quaternion.identity, enemyTransform);
                Necrotic_Effect necroticEffect = hitAnimationInstance.GetComponent<Necrotic_Effect>();
                necroticEffect.Initialize(target);
                StartCoroutine(DestroyWithDelay());
            }

        }

        if (other.tag == "Item" || other.tag == "Aspect")
        {
            Instantiate(hitAnimation, transform.position, Quaternion.identity);
        }

        if (other.tag == "Enemy")
        {

        }
        if (other.tag == "Environment")
        {
           // Instantiate(hitAnimation, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyWithDelay()
    {
        // Delay before destroying the projectile
        yield return new WaitForSeconds(0.05f); // Adjust the delay time as needed

        Destroy(gameObject);
    }
}
