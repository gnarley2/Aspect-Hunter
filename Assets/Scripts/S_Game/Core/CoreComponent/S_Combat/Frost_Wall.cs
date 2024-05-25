using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Frost_Wall : MonoBehaviour
{
    [SerializeField] private float wallDuration = 5f; // Duration of the wall in seconds
    private Animator animator;
    private int damage = 1;
    private Rigidbody2D rb;

    public float collisionIgnoreDuration = 0.1f;
    private GameObject player;
    private bool isEnemyColliding = false;
    private bool isPlayerColliding = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Collider2D playerCollider = player.GetComponent<Collider2D>();

        Collider2D frostWallCollider = GetComponent<Collider2D>();

        if (frostWallCollider != null && playerCollider != null)
        {
            Physics2D.IgnoreCollision(frostWallCollider, playerCollider, true);
            StartCoroutine(ReenableCollision(frostWallCollider, playerCollider));
        }

        animator = GetComponent<Animator>();
        StartCoroutine(DestroyWallAfterDuration());
    }

    public void Initialize(Vector3 newDirection, int damage)
    {
        this.damage = damage;
        // Lock the rotation of the Frost Wall
        transform.rotation = Quaternion.identity;
    }

    IEnumerator DestroyWallAfterDuration()
    {
        yield return new WaitForSeconds(wallDuration);
        animator.SetBool("Destroy", true);
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    IEnumerator ReenableCollision(Collider2D frostWallCollider, Collider2D playerCollider)
    {
        yield return new WaitForSeconds(collisionIgnoreDuration);
        Physics2D.IgnoreCollision(frostWallCollider, playerCollider, false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            isEnemyColliding = true;
            UpdateConstraints();
        }
        else if (collision.collider.CompareTag("Player"))
        {
            isPlayerColliding = true;
            UpdateConstraints();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            isEnemyColliding = false;
            UpdateConstraints();
        }
        else if (collision.collider.CompareTag("Player"))
        {
            isPlayerColliding = false;
            UpdateConstraints();
        }
    }

    private void UpdateConstraints()
    {
        if (isPlayerColliding)
        {
            // Allow movement if the player is colliding
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else if (isEnemyColliding)
        {
            // Freeze all constraints if only an enemy is colliding
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            // Reset constraints if neither the player nor an enemy is colliding
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}