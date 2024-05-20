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

    void Awake()
    {
 
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


}