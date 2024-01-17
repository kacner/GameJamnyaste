using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float Timer = 0.5f;
    void Start()
    {
        // Invoke the DestroyObject method after 0.5 seconds
        Invoke("DestroyObject", Timer);
    }

    void DestroyObject()
    {
        // Destroy the game object this script is attached to
        Destroy(gameObject);
    }
}