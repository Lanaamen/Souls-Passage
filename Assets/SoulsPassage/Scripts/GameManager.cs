using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;   // Text display for the timer
    public TextMeshProUGUI scoreText;   // Text display for the score
    public int score = 0;               // Player's score
    private float timeRemaining = 60f;  // Countdown timer starting at 60 seconds
    private bool isGameActive = false;  // Game state - initially inactive
    private bool isTimerRunning = false; // Timer status

    void Start()
    {
        UpdateScoreText();
        UpdateTimerText();
    }

    void Update()
    {
        if (isGameActive && isTimerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerText();
            }
            else
            {
                EndGame();
            }
        }
    }

    // Method to start the timer when the button is pressed
    public void StartTimer()
    {
        isGameActive = true;  // Set the game as active
        isTimerRunning = true; // Start the timer
        timeRemaining = 60f;   // Reset the timer to 60 seconds
        UpdateTimerText();     // Update the timer display immediately
    }

    // Method to add a point to the score
    public void AddPoint()
    {
        if (isGameActive)
        {
            score++;
            UpdateScoreText();
        }
    }

    // Update the score display
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    // Update the timer display
    private void UpdateTimerText()
    {
        timerText.text = "Time: " + Mathf.Ceil(timeRemaining).ToString() + "s";
    }

    // End the game when the timer reaches zero
    private void EndGame()
    {
        isTimerRunning = false;  // Stop the timer
        isGameActive = false;    // End the game
        timerText.text = "Time's Up!";
        Debug.Log("Game Over! Final Score: " + score);
    }

        public void QuitGame()
    {
        #if UNITY_EDITOR
        // If running in the Unity Editor, stop playing
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // If running as a standalone build, close the application
        Application.Quit();
        #endif
    }

    // This method will restart the game by reloading the current scene
    public void RestartGame()
    {
        // Get the current active scene and reload it
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
