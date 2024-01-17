using UnityEngine;

public class meatman : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float upwardForceOnWallHit = 5f;
    public float minJumpInterval = 2f;
    public float maxJumpInterval = 5f;
    public Transform wallCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isFacingRight = true;
    private bool isHittingWall;
    private bool isGrounded;
    private float jumpTimer;
    int HP = 5;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ResetJumpTimer();
    }

    private void Update()
    {
        // Check if the enemy is on the ground
        isGrounded = Physics2D.OverlapCircle(transform.position, 0.1f, groundLayer);

        // Check if the enemy is hitting a wall
        isHittingWall = Physics2D.Raycast(wallCheck.position, transform.right * (isFacingRight ? 1 : -1), 0.1f, groundLayer);

        // Flip the enemy if hitting a wall
        if (isHittingWall)
        {
            Flip();
            ApplyUpwardForceOnWallHit();
        }

        // Move the enemy
        Move();

        // Jump when hitting a wall and on the ground
        if (isHittingWall && isGrounded)
        {
            Jump();
        }

        // Randomly jump
        jumpTimer -= Time.deltaTime;
        if (jumpTimer <= 0)
        {
            Jump();
            ResetJumpTimer();
        }
    }

    private void Move()
    {
        float horizontalInput = isFacingRight ? 1f : -1f;
        Vector2 movement = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        rb.velocity = movement;
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void ApplyUpwardForceOnWallHit()
    {
        rb.velocity = new Vector2(rb.velocity.x, upwardForceOnWallHit);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void ResetJumpTimer()
    {
        jumpTimer = Random.Range(minJumpInterval, maxJumpInterval);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has the specified tag
        if (other.gameObject.CompareTag("Bullet"))
        {
            HandleSlimeDeath();
        }
    }
    void HandleSlimeDeath()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            HP--;
        }
    }
}