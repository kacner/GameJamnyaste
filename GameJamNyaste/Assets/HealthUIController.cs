using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour
{
    HealthManager healthManager;
    public int currentHP;

    [SerializeField] private Image[] hearts;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            healthManager = player.GetComponent<HealthManager>();
        }
        else
        {
            Debug.LogError("Player GameObject not found.");
        }
    }

    public void UpdateHealth()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHP)
            {
                hearts[i].color = Color.white;
            }
            else
            {
                hearts[i].color = Color.black;
            }
        }
    }

    void Update()
    {
        if (healthManager != null)
        {
            currentHP = healthManager.currentHealth;
            UpdateHealth();
        }
    }
}