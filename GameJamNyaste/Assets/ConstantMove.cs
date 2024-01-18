using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMove : MonoBehaviour
{
    public Vector2 movementDirection = new Vector2(1f, 0f); // Change this vector to specify the movement direction
    public float movementSpeed = 5f; // Change this value to set the movement speed
    private bool touchingPlayer = false;

    private void Start()
    {
        InvokeRepeating("TouchCheck", 1f, 3f);
    }

    void Update()
    {
        if (!touchingPlayer)
        {
            // Calculate the new position based on the direction and speed
            Vector3 newPosition = transform.position + new Vector3(movementDirection.x, movementDirection.y, 0f) * movementSpeed * Time.deltaTime;

            // Update the position of the GameObject
            transform.position = newPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            touchingPlayer = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touchingPlayer = true;
        }
    }

    private void TouchCheck()
    {
        touchingPlayer = false;
    }
}