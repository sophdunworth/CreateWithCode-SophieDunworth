using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        // Initialization code if needed
    }

    // Update is called once per frame
    void Update()
    {
        // Get the vertical input from the user (Up/Down arrow keys)
        verticalInput = Input.GetAxis("Vertical");

        // Move the object up or down based on input and speed
        transform.Translate(Vector3.up * verticalInput * speed * Time.deltaTime);

        // Check if spacebar is pressed and drop the present prefab
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!gameOver && Time.time > lastDropTime + dropCooldown)
            {
                DropPresent();
                lastDropTime = Time.time; // Update the last drop time
            }
            else
            {
                // Optional feedback: Log message or play a sound
                Debug.Log("Present drop on cooldown!");
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
        }
    }

    // Method to drop the present prefab below the player
    private void DropPresent()
    {
        // Calculate the position slightly below the player
        Vector3 dropPosition = new Vector3(transform.position.x, transform.position.y + dropOffset, transform.position.z);

        // Instantiate the present prefab at the calculated position
        GameObject present = Instantiate(presentPrefab, dropPosition, presentPrefab.transform.rotation);

        // Ensure the present is on the correct layer
        present.layer = LayerMask.NameToLayer("Present");

        // Add a Rigidbody to the present prefab if it doesn't have one to ensure it falls
        if (present.GetComponent<Rigidbody>() == null)
        {
            present.AddComponent<Rigidbody>();
        }

        // Add a custom collision handler for the present object
        present.AddComponent<PresentCollisionHandler>();
    }
}

public class PresentCollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Child") || collision.gameObject.CompareTag("Ground"))
        {
            // Destroy the present when it collides with a Child or Ground
            Destroy(gameObject);
        }
    }
}
