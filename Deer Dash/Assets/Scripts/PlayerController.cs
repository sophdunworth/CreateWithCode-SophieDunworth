using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed; // Player's movement speed
    private float verticalInput;
    public bool gameOver = false;
    public GameObject presentPrefab; // Prefab to drop from the player
    private float dropOffset = -1.0f; // Offset to spawn the present slightly below the player
    private float dropCooldown = 0.5f; // Cooldown time for dropping presents
    private float lastDropTime = 0f; // Time of the last present drop
    public GameObject collisionEffectPrefab; // Particle effect prefab for collisions

    void Update()
    {
        // Get the vertical input from the user
        verticalInput = Input.GetAxis("Vertical");

        // Move the object up or down based on input and speed
        transform.Translate(Vector3.up * verticalInput * speed * Time.deltaTime);

        // If spacebar is pressed and drop the present prefab
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver && Time.time > lastDropTime + dropCooldown)
        {
            DropPresent();
            lastDropTime = Time.time; 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Prevent multiple triggers when the game is over
        if (gameOver) return;

        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Player collided with an Obstacle!");
            // Call the GameOver method
            GameOver(); 

            // Trigger collision effect
            if (collisionEffectPrefab != null)
            {
                Instantiate(collisionEffectPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Collision effect prefab is not assigned!");
            }

            // GameManager of game over
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.GameOver();
            }
            else
            {
                Debug.LogError("GameManager not found!");
            }
        }
    }

    //method to drop present when the space bar is pressed
    private void DropPresent()
    {
        if (presentPrefab == null)
        {
            Debug.LogError("Present Prefab is not assigned in the Inspector!");
            return; 
        }

        Vector3 dropPosition = new Vector3(transform.position.x, transform.position.y + dropOffset, transform.position.z);

        
        Debug.Log("Dropping present at position: " + dropPosition);

        GameObject present = Instantiate(presentPrefab, dropPosition, presentPrefab.transform.rotation);

        
        if (present.GetComponent<Rigidbody>() == null)
        {
            Debug.Log("Adding Rigidbody to the present.");
            present.AddComponent<Rigidbody>(); 
        }

        Collider presentCollider = present.GetComponent<Collider>();
        if (presentCollider == null)
        {
            Debug.LogWarning("The present prefab does not have a collider!");
        }
        else
        {
            presentCollider.isTrigger = true; 
        }

        // Add the PresentCollisionHandler script
        present.AddComponent<PresentCollisionHandler>();
    }

    // Method to handle the game over state
    public void GameOver()
    {
        gameOver = true;
        Debug.Log("Game Over!");
    }
}


