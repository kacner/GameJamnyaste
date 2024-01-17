using System.Collections;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    public float jumpForce = 5f;
    public float jumpInterval = 2f;
    private Rigidbody2D rb;
    private Transform player;
    private bool isJumping = false;
    public float JumpPower = 10;
    public Animator animator;

    public GameObject deathPrefab;
    public GameObject deathEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Assuming your player has the "Player" tag
        StartCoroutine(JumpCoroutine());
    }

    void Update()
    {
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
        Instantiate(deathPrefab, transform.position, Quaternion.identity);
        Instantiate(deathPrefab, transform.position, Quaternion.identity);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
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
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has the specified tag
        if (other.gameObject.CompareTag("Bullet"))
        {
            HandleSlimeDeath();
        }
    }
}