using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;      // The obstacle prefab to spawn
    public GameObject[] childPrefab;       // Array of child prefabs to spawn
    public GameObject particleEffectPrefab; // Particle effect to attach to obstacles
    private GameObject presentPrefab;      // The present prefab to spawn
    private float minObstacleSpawnTime = 4f;  // Minimum time between obstacle spawns
    private float maxObstacleSpawnTime = 10f; // Maximum time between obstacle spawns
    private Vector3 spawnPos = new Vector3(60, 0, 0); // The spawn position for obstacles and children
    private float startDelay = 2f; // Delay before starting the spawns
    private PlayerController PlayerControllerScript; // Reference to the PlayerController script
    private float difficultyMultiplier = 1f;  // Multiplier to adjust spawn rate and speed based on difficulty

 
    void Start()
    {
        // Get reference to PlayerController script attached to the Player object
        PlayerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        // Start spawning obstacles, children, and presents with the desired delays
        StartCoroutine(SpawnObstaclesAtRandomIntervals());
        StartCoroutine(SpawnChildrenAtRandomIntervals());
        StartCoroutine(SpawnPresentsAtRandomIntervals());
    }

    // Method to set the difficulty multiplier
    public void SetDifficultyMultiplier(float multiplier)
    {
        difficultyMultiplier = multiplier;
        AdjustSpawnTimesAndSpeeds();
    }

    // Adjust obstacle spawn times and speed based on the difficulty multiplier
    private void AdjustSpawnTimesAndSpeeds()
    {
        minObstacleSpawnTime /= difficultyMultiplier;  
        maxObstacleSpawnTime /= difficultyMultiplier;  
    }

    // Coroutine to spawn obstacles at random intervals
    private IEnumerator SpawnObstaclesAtRandomIntervals()
    {
        yield return new WaitForSeconds(startDelay); // Initial delay

        while (!PlayerControllerScript.gameOver) // Continue until the game is over
        {
            SpawnObstacle(); // Spawn an obstacle
            // Wait for a random time interval between minObstacleSpawnTime and maxObstacleSpawnTime
            float randomInterval = Random.Range(minObstacleSpawnTime, maxObstacleSpawnTime);
            yield return new WaitForSeconds(randomInterval);
        }
    }

    // Method to instantiate obstacles 
    void SpawnObstacle()
    {
        if (!PlayerControllerScript.gameOver) // Ensure the game is not over
        {
            // Generate a random position 
            float randomY = Random.Range(4f, 15f);  // Adjust the range as needed

            // Set the spawn position
            Vector3 obstacleSpawnPos = new Vector3(spawnPos.x, randomY, spawnPos.z);

            // Instantiate the obstacle
            GameObject spawnedObstacle = Instantiate(obstaclePrefab, obstacleSpawnPos, obstaclePrefab.transform.rotation);

            // Attach the particle effect to the spawned obstacle
            if (particleEffectPrefab != null)
            {
                // Create a particle effect instance
                GameObject particleEffectInstance = Instantiate(particleEffectPrefab, spawnedObstacle.transform.position, Quaternion.identity);
                particleEffectInstance.transform.SetParent(spawnedObstacle.transform); 
                // Ensure the particle system is set to play constantly
                var particleSystem = particleEffectInstance.GetComponent<ParticleSystem>();
                if (particleSystem != null)
                {
                    var mainModule = particleSystem.main;
                    mainModule.loop = true; 
                    if (!particleSystem.isPlaying)
                    {
                        particleSystem.Play(); 
                    }
                }
            }
        }
    }

    // Coroutine to spawn children at random intervals
    private IEnumerator SpawnChildrenAtRandomIntervals()
    {
        yield return new WaitForSeconds(startDelay); // Initial delay

        while (!PlayerControllerScript.gameOver) // Continue until the game is over
        {
            SpawnChild(); // Spawn a child object
            // Wait for a random time between 1 and 8 seconds before spawning the next child
            float randomInterval = Random.Range(1f, 8f) / difficultyMultiplier;
            yield return new WaitForSeconds(randomInterval);
        }
    }

   
    // Method to instantiate child objects
    void SpawnChild()
    {
        if (!PlayerControllerScript.gameOver) // Ensure the game is not over
        {
            // Select a random prefab from the childPrefab array
            GameObject selectedChild = childPrefab[Random.Range(0, childPrefab.Length)];

            // Instantiate the selected prefab at the spawn position with the correct rotation
            GameObject spawnedChild = Instantiate(selectedChild, spawnPos, selectedChild.transform.rotation);

            // Add collider trigger for the child 
            Collider childCollider = spawnedChild.GetComponent<Collider>();
            if (childCollider != null && !childCollider.isTrigger)
            {
                childCollider.isTrigger = true; // Enable trigger for collision detection
            }

            // Attach the collision detection method to the child
            var childTrigger = spawnedChild.AddComponent<ChildCollisionHandler>();
            childTrigger.spawnManager = this; 
        }
    }

    // Coroutine to spawn presents at random intervals
    private IEnumerator SpawnPresentsAtRandomIntervals()
    {
        yield return new WaitForSeconds(startDelay); // Initial delay

        while (!PlayerControllerScript.gameOver) // Continue until the game is over
        {
            SpawnPresent(); // Spawn a present
            // Wait for a random time interval between 5 and 10 seconds
            float randomInterval = Random.Range(5f, 10f) / difficultyMultiplier; // Adjusted by difficulty
            yield return new WaitForSeconds(randomInterval);
        }
    }

    // Method to instantiate presents
    void SpawnPresent()
    {
        if (!PlayerControllerScript.gameOver) // Ensure the game is not over
        {
            // Generate a random Y position for the present within a specified range
            float randomY = Random.Range(3f, 15f);  // Adjust the range as needed

            // Set the spawn position
            Vector3 presentSpawnPos = new Vector3(spawnPos.x, randomY, spawnPos.z);

            // Instantiate the present 
            Instantiate(presentPrefab, presentSpawnPos, presentPrefab.transform.rotation);
        }
    }

    // Handle OnTriggerEnter for detecting collisions with obstacles
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding is the player 
        if (other.CompareTag("Player"))
        {
            // Handle collision with the player 
            Debug.Log("Player collided with an obstacle!");

            // Call any necessary methods for collision handling
            PlayerControllerScript.GameOver(); 
        }
    }
}

// Create a separate class to handle collision detection for child objects
public class ChildCollisionHandler : MonoBehaviour
{
    public SpawnManager spawnManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Present")) // If the child collides with a present
        {
            // Handle collision with the present
            Debug.Log("Child collided with a present!");

            // Call any necessary functions for interaction with the present
            CollectPresent(other.gameObject);

           // Destroy the child or present after the collision
            Destroy(other.gameObject);
        }
    }

    private void CollectPresent(GameObject present)
    {
        
        Debug.Log("Present collected!");

       
    }
}
