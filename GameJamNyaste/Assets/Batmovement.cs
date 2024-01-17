using UnityEngine;

public class Batmovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float zickZackRange = 2f; // Maximum deviation from the straight path
    public float changeDirectionInterval = 2f;

    private Rigidbody2D rb;
    private Transform player;
    private Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null)
        {
            Debug.LogError("Player not found. Make sure the player has the 'Player' tag.");
        }

        InvokeRepeating("ChangeDirection", 0f, changeDirectionInterval);
    }

    void Update()
    {
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector2 targetDirection = (player.position - transform.position).normalized;

            // Add some zick-zack movement to the direction
            float zickZackOffset = Random.Range(-zickZackRange, zickZackRange);
            Vector2 zickZackVector = new Vector2(targetDirection.y, -targetDirection.x) * zickZackOffset;

            moveDirection = (targetDirection + zickZackVector).normalized;
            rb.velocity = moveDirection * moveSpeed;
        }
    }

    void HandleSlimeDeath()
    {
        Destroy(gameObject);
    }

    void ChangeDirection()
    {
        // This function can be used to introduce more randomness in the movement
        // You can modify this function as needed for your game
        float randomAngle = Random.Range(0f, 360f);
        moveDirection = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));
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