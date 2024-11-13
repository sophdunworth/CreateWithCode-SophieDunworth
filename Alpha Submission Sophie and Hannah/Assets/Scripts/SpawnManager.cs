using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public GameObject childPrefab;
    private float minObstacleSpawnTime = 4f;
    private float maxObstacleSpawnTime = 10f;
    private Vector3 spawnPos = new Vector3(25, 1, 0);
    private float startDelay = 2f;
    private PlayerController PlayerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        PlayerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        // Start spawning obstacles and children with the desired delays
        StartCoroutine(SpawnObstaclesAtRandomIntervals());
        StartCoroutine(SpawnChildrenAtRandomIntervals());
    }

    // Coroutine to spawn child objects at random intervals
    private IEnumerator SpawnChildrenAtRandomIntervals()
    {
        yield return new WaitForSeconds(startDelay); // Initial delay

        while (!PlayerControllerScript.gameOver)
        {
            SpawnChild();
            // Wait for a random time between 1 and 8 seconds before spawning the next child
            float randomInterval = Random.Range(1f, 8f);
            yield return new WaitForSeconds(randomInterval);
        }
    }

    // Method to instantiate child objects
    void SpawnChild()
    {
        if (!PlayerControllerScript.gameOver)
        {
            Instantiate(childPrefab, spawnPos, childPrefab.transform.rotation);
        }
    }

    // Coroutine to spawn obstacles at random intervals
    private IEnumerator SpawnObstaclesAtRandomIntervals()
    {
        yield return new WaitForSeconds(startDelay); // Initial delay

        while (!PlayerControllerScript.gameOver)
        {
            SpawnObstacle();
            // Wait for a random time interval between minObstacleSpawnTime and maxObstacleSpawnTime
            float randomInterval = Random.Range(minObstacleSpawnTime, maxObstacleSpawnTime);
            yield return new WaitForSeconds(randomInterval);
        }
    }

    // Method to instantiate obstacles with random Y positions
    void SpawnObstacle()
    {
        if (PlayerControllerScript.gameOver == false)
        {
            // Generate a random Y position for the obstacle within a specified range
            float randomY = Random.Range(3f, 15f);  // Adjust the range as needed

            // Set the spawn position with the random Y value
            Vector3 obstacleSpawnPos = new Vector3(spawnPos.x, randomY, spawnPos.z);

            // Instantiate the obstacle at the random Y position
            Instantiate(obstaclePrefab, obstacleSpawnPos, obstaclePrefab.transform.rotation);
        }
    }
}

