using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveLeft : MonoBehaviour
{
    private float obstacleSpeed = 5f;                       // Speed of the obstacle moving left
    private float obstacleDestroyX = -35f;                 // Position where the obstacle should be destroyed
    private bool isObstacle = false;                      // Flag to check if the object is an obstacle
    private PlayerController playerControllerScript;     // Reference to PlayerController script

    void Start()
    {
        // Find PlayerController to check if the game is over
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            playerControllerScript = player.GetComponent<PlayerController>();
        }

        // Check if this object is an obstacle
        isObstacle = gameObject.CompareTag("Obstacle");

        if (isObstacle)
        {
            // Ensure no external forces affect the obstacle
            Rigidbody obstacleRigidbody = GetComponent<Rigidbody>();
            if (obstacleRigidbody != null)
            {
                // Disable gravity
                obstacleRigidbody.useGravity = false;
                // Avoid physics-based movement
                obstacleRigidbody.isKinematic = true; 
            }
        }
    }
    void Update()
    {
        if (playerControllerScript == null || playerControllerScript.gameOver)
        {
            // Stop movement if the player script is not found or the game is over
            return;
        }

        // Move the obstacle
        MoveObstacle();

        // Destroy the obstacle if it moves past the left boundary
        if (transform.position.x <= obstacleDestroyX)
        {
            Destroy(gameObject);
        }
    }

    // Function to move the obstacle strictly left
    private void MoveObstacle()
    {
        transform.Translate(Vector3.left * obstacleSpeed * Time.deltaTime, Space.World);
    }
}

