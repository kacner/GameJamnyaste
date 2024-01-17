using System.Collections;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public float detectionRadius = 10f;
    public float chargingDistance = 5f;
    public float dashSpeed = 10f;
    public float dashDuration = 1f;
    public float dashCooldown = 3f;

    private Transform player;
    private bool isCharging = false;

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(BossAI());
    }

    void Update()
    {
        // Additional update logic goes here
    }

    IEnumerator BossAI()
    {
        while (true)
        {
            // Check if the player is within detection radius
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRadius)
            {
                // Lock onto the player
                Vector2 directionToPlayer = (player.position - transform.position).normalized;
                transform.up = directionToPlayer;

                // If within charging distance, start charging
                if (distanceToPlayer <= chargingDistance && !isCharging)
                {
                    isCharging = true;
                    yield return StartCoroutine(ChargeDash());
                    isCharging = false;
                }
            }

            yield return null;
        }
    }

    IEnumerator ChargeDash()
    {
        // Charge up the dash
        yield return new WaitForSeconds(dashCooldown);

        // Perform the dash
        StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        // Move towards the player at dash speed
        float startTime = Time.time;
        while (Time.time - startTime < dashDuration)
        {
            transform.Translate(Vector2.up * dashSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // Check if the boss is defeated
        if (currentHealth <= 0)
        {
            DefeatBoss();
        }
    }

    void DefeatBoss()
    {
        // Additional logic for boss defeat goes here
        Destroy(gameObject);
    }
}
