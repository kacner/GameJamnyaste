using UnityEngine;
using UnityEngine.SceneManagement;

public class doorscript : MonoBehaviour
{
    [SerializeField] private string nextSceneName; // Name of the next scene to load

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            // Load the next scene
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        // Check if the next scene name is not empty
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            // Load the next scene
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Next scene name is not set in the Door script.");
        }
    }
}
