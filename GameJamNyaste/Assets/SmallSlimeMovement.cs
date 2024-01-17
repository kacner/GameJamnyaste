using System.Collections;
using UnityEngine;

public class SmallSlimeMovement : MonoBehaviour
{
    public float jumpForce = 5f;
    public float jumpInterval = 2f;
    private Rigidbody2D rb;
    private Transform player;
    private bool isJumping = false;
    public float JumpPower = 10;
    public float minForce = 5f;
    public float maxForce = 10f;
    public float knockbackAngle = 45f;
    public Animator animator;
    public GameObject deathEffect;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ApplyRandomKnockback();
        StartCoroutine(JumpCoroutine());

    }

    void ApplyRandomKnockback()
    {
        // Generate random force magnitude
        float forceMagnitude = Random.Range(minForce, maxForce);

        // Convert fixed angle to radians
        float angle = knockbackAngle * Mathf.Deg2Rad;

        // Calculate force components based on angle
        float forceX = forceMagnitude * Mathf.Cos(angle);
        float forceY = forceMagnitude * Mathf.Sin(angle);

        Vector2 knockbackForce = new Vector2(forceX, forceY);

        rb.AddForce(knockbackForce, ForceMode2D.Impulse);
    }

    void Update()
    {
        float horizontalVelocity = rb.velocity.x;

        // Check if the slime is on the ground (you might need to adjust this based on your game setup)
        if (Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            isJumping = false;
        }
        animator.SetBool("Jumping", isJumping);

    }


    IEnumerator JumpCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(jumpInterval);
            JumpTowardsPlayer();
        }
    }

    void HandleSlimeDeath()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has the specified tag
        if (other.gameObject.CompareTag("Bullet"))
        {
            HandleSlimeDeath();
        }
    }

    void JumpTowardsPlayer()
    {
        if (!isJumping)
        {
            Vector2 force = new Vector2(rb.velocity.x, JumpPower);
            rb.AddForce(force, ForceMode2D.Impulse);
            Vector2 direction = (player.position - transform.position).normalized;
            rb.AddForce(direction * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }
    }
}