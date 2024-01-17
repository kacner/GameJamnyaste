using UnityEngine;

public class AbductedChild : MonoBehaviour
{
    // Reference to the parent GameObject
    public GameObject parentObject;

    void Update()
    {
        // Check if the parentObject reference is set
        if (parentObject != null)
        {
            // Set the childObject's position relative to the parentObject
            transform.position = parentObject.transform.position; // Adjust the position as needed
        }
    }
}