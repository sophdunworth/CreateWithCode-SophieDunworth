using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;           

    
    public TextMeshProUGUI scoreText;              // Displays the current score
    public TextMeshProUGUI gameOverText;           // Displays "Game Over" text
    public Button easyButton;                      // Button to select the easy level
    public Button mediumButton;                    // Button to select the medium level
    public Button hardButton;                      // Button to select the hard level
    public GameObject levelSelectPanel;            // Panel for selecting the level
    public GameObject gameUI;                      // Main game UI panel
    public GameObject gameOverPanel;               // Game Over panel
    public GameObject openingScreen;               // Opening screen before the game starts
    public Button restartButton;                   // Button to restart the game
    private int score;                             // Tracks the player's score
    private bool isGameOver;                       // Indicates if the game is over
    public bool isLevelSelected = false;           // Indicates if a level has been selected

    public void Start()
    {
       
        if (Instance == null) Instance = this;

        // Initialize UI states
        openingScreen.SetActive(true);
        levelSelectPanel.SetActive(false);
        gameUI.SetActive(false);
        gameOverPanel.SetActive(false);

        // Add listeners for level selection buttons
        easyButton.onClick.AddListener(() => SelectLevel("Easy"));
        mediumButton.onClick.AddListener(() => SelectLevel("Medium"));
        hardButton.onClick.AddListener(() => SelectLevel("Hard"));

        // Add listener for restart button
        restartButton.onClick.AddListener(RestartGame);

        // Initialize score
        score = 0;
        UpdateScoreText();
    }

    // Handles level selection and starts the game with the specified difficulty
    private void SelectLevel(string difficulty)
    {
        Debug.Log("Level selected: " + difficulty);
        StartGame(difficulty);
        SetLevelSelected(true);
    }

    // Starts the game with the given difficulty
    public void StartGame(string difficulty)
    {
        Debug.Log($"Starting game on {difficulty} difficulty");

        // Update UI visibility
        openingScreen.SetActive(false);
        levelSelectPanel.SetActive(false);
        gameUI.SetActive(true);
        gameOverPanel.SetActive(false);

        // Reset game state
        isGameOver = false;
        score = 0;
        UpdateScoreText();

        // Start background movement
        FindObjectOfType<RepeatBackground>().StartBackgroundMovement();

        // Set difficulty parameters
        SetDifficulty(difficulty);
    }

    // Configures the game based on the selected difficulty
    private void SetDifficulty(string difficulty)
    {
        float speed = 1.0f;             
        float spawnFrequency = 3.0f;   

        switch (difficulty)
        {
            case "Easy":
                speed = 1.0f;
                spawnFrequency = 3.0f;
                break;
            case "Medium":
                speed = 2.0f;
                spawnFrequency = 2.5f;
                break;
            case "Hard":
                speed = 3.0f;
                spawnFrequency = 1.5f;
                break;
        }

        // Apply obstacle settings
        SetObstacleParameters(speed, spawnFrequency);
    }

    // Updates obstacle parameters in the SpawnManager
    private void SetObstacleParameters(float speed, float spawnFrequency)
    {
        float multiplier = 1.0f;

        // Determine multiplier based on speed and spawn frequency
        if (speed == 1.0f && spawnFrequency == 3.0f)
            multiplier = 1.0f;
        else if (speed == 2.0f && spawnFrequency == 2.5f)
            multiplier = 1.5f;
        else if (speed == 3.0f && spawnFrequency == 1.5f)
            multiplier = 2.0f;

        // Pass the multiplier to the SpawnManager
        FindObjectOfType<SpawnManager>().SetDifficultyMultiplier(multiplier);
    }

    // Handles game over state
    public void GameOver()
    {
        isGameOver = true;

        // Update screen visibilty
        openingScreen.SetActive(false);
        levelSelectPanel.SetActive(false);
        gameUI.SetActive(false);
        gameOverPanel.SetActive(true);

        Debug.Log("Game Over! Showing Game Over panel.");
    }

    // Increases the player's score
    public void IncreaseScore(int points)
    {
        if (!isGameOver)
        {
            score += points;
            Debug.Log("Score increased: " + score);
            UpdateScoreText();
        }
    }

    // Updates the score text
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    // Restarts the game by reloading from the start
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Sets the level selection state
    public void SetLevelSelected(bool isSelected)
    {
        isLevelSelected = isSelected;
    }
}



