using UnityEngine;

public class Keyscript : MonoBehaviour
{

    // This function is called when the collider of this object comes in contact with another collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other collider has the specified player tag
        if (other.CompareTag("Player"))
        {
            // If it does, destroy this game object
            Destroy(gameObject);
        }
    }
}