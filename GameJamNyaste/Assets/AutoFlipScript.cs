using UnityEngine;

public class AutoFlipScript : MonoBehaviour
{
    // Reference to the Rigidbody2D component
    private Rigidbody2D rb;

    // Reference to the SpriteRenderer component
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        // Get the Rigidbody2D and SpriteRenderer components
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (rb == null || spriteRenderer == null)
        {
            // If Rigidbody2D or SpriteRenderer is not found, log an error
            Debug.LogError("Rigidbody2D or SpriteRenderer component not found on the GameObject!");
        }
    }

    private void Update()
    {
        // Check the horizontal velocity to determine if the sprite is moving to the left
        float horizontalVelocity = rb.velocity.x;

        // Flip the sprite based on the direction of movement
        if (horizontalVelocity < 0)
        {
            // If moving left, flip the sprite
            Flip(true);
        }
        else if (horizontalVelocity > 0)
        {
            // If moving right, ensure the sprite is not flipped
            Flip(false);
        }
        // If horizontalVelocity is 0, the sprite maintains its current orientation
    }

    private void Flip(bool facingLeft)
    {
        // Flip the sprite's X-axis scale based on the specified direction
        spriteRenderer.flipX = facingLeft;
    }
}