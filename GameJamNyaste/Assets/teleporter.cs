using System.Collections;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject slimePrefab;  // Reference to the slime prefab
    public int numberOfSlimes = 12;  // Number of slimes to spawn
    private bool isActivated = false;
    public float spawnInterval = 2f; // Time interval between slime spawns in seconds

    private void Update()
    {
        // Check if the "E" key is pressed and the teleporter is not activated yet
        if (Input.GetKeyDown(KeyCode.E) && !isActivated)
        {
            isActivated = true;
            StartCoroutine(SpawnSlimes());
        }
    }

    IEnumerator SpawnSlimes()
    {
        // Calculate the time delay between spawns
        float timeDelay = 30f / numberOfSlimes;

        // Spawn slimes over the specified duration
        for (int i = 0; i < numberOfSlimes; i++)
        {
            // Check if the teleporter is still activated
            if (!isActivated)
                yield break;  // Exit the coroutine if deactivated

            // Randomly determine the x position between -12 and 13
            float randomX = Random.Range(-12f, 13f);
            Vector3 spawnPosition = new Vector3(randomX, transform.position.y, transform.position.z);

            // Instantiate the slime at the random position
            Instantiate(slimePrefab, spawnPosition, Quaternion.identity);

            // Wait for the specified interval before the next spawn
            yield return new WaitForSeconds(timeDelay);
        }

        // Teleporter activation complete
        isActivated = false;
    }
}