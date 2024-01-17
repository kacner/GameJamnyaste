using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    public void InitializeHealth(float initialHealth)
    {
        maxHealth = initialHealth;
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // Implement any additional logic here, such as updating UI or triggering effects

        Debug.Log($"{gameObject.name} took {damage} damage. Current Health: {currentHealth}");

        // Check for object defeat
        if (currentHealth <= 0)
        {
            DefeatObject();
        }
    }

    private void DefeatObject()
    {
        // Implement defeat logic here
        Debug.Log($"{gameObject.name} is defeated!");
        Destroy(gameObject); // For simplicity, just destroy the object
    }
}
