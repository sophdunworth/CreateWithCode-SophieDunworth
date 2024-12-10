using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenController : MonoBehaviour
{
    public GameObject openingScreen;        // Reference to the opening screen UI
    public GameObject instructionsScreen;   // Reference to the instructions screen UI
    public GameObject levelScreen;          // Reference to the level selection screen UI
    public GameObject background;           // Reference to the background GameObject
    public GameManager gameManager;         // Reference to the GameManager script
    public RepeatBackground repeatBackground; // Reference to the RepeatBackground script for background movement
    public GameObject obstaclesParent;       // Parent GameObject containing all obstacles (not used here, but likely for obstacle management)

    public void Start()
    {
        // Initialize UI screens: Show the opening screen, hide others
        openingScreen.SetActive(true);
        instructionsScreen.SetActive(false);
        levelScreen.SetActive(false);

        // Get the RepeatBackground component from the background GameObject
        repeatBackground = background.GetComponent<RepeatBackground>();

        // Assign the GameManager reference if not already set in the inspector
        if (gameManager == null)
        {
            gameManager = GameManager.Instance;
        }
    }

    // Method to handle the transition from the opening screen to the instructions screen
    public void OnNextButtonPressed()
    {
        openingScreen.SetActive(false); // Hide the opening screen
        instructionsScreen.SetActive(true); // Show the instructions screen
    }

    // Method to handle the transition from the instructions screen to the level selection screen
    public void OnNextButtonToLevel()
    {
        instructionsScreen.SetActive(false); // Hide the instructions screen
        levelScreen.SetActive(true); // Show the level selection screen
    }

    // Method to select a level and start the game
    public void SelectLevel(string difficulty)
    {
        levelScreen.SetActive(false); // Hide the level screen after a level is selected
        gameManager.StartGame(difficulty); // Pass the selected difficulty to the GameManager

        // Start the background movement using the RepeatBackground script
        repeatBackground.StartBackgroundMovement();
    }

    // Wrapper method to select and start the Easy level
    public void StartEasyLevel()
    {
        Debug.Log("Easy level selected"); 
        SelectLevel("Easy"); 
    }

    // Wrapper method to select and start the Medium level
    public void StartMediumLevel()
    {
        Debug.Log("Medium level selected"); 
        SelectLevel("Medium"); 
    }

    // Wrapper method to select and start the Hard level
    public void StartHardLevel()
    {
        Debug.Log("Hard level selected");
        SelectLevel("Hard"); 
    }
}











