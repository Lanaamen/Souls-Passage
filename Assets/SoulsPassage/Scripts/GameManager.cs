using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;   // Text display for the timer
    public TextMeshProUGUI scoreText;   // Text display for the total score
    public TextMeshProUGUI correctText; // Text display for correct answers
    public TextMeshProUGUI wrongText;   // Text display for wrong answers
    public int correctAnswers = 0;      // Player's correct answers
    public int wrongAnswers = 0;        // Player's wrong answers
    private int totalScore = 0;         // Player's total score
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

    // Method to add a point for a correct answer
    public void AddCorrectAnswer()
    {
        if (isGameActive)
        {
            correctAnswers++;      // Increment correct answers
            UpdateScoreText();     // Update the score display
        }
    }

    // Method to add a point for a wrong answer
    public void AddWrongAnswer()
    {
        if (isGameActive)
        {
            wrongAnswers++;        // Increment wrong answers
            UpdateScoreText();     // Update the score display
        }
    }

    // Update the score display
    private void UpdateScoreText()
    {
        totalScore = correctAnswers - wrongAnswers; // Total score is the correct minus wrong answers
        scoreText.text = "Total Score: " + totalScore;
        correctText.text = "Correct: " + correctAnswers; // Display correct answers
        wrongText.text = "Wrong: " + wrongAnswers;       // Display wrong answers
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
        Debug.Log("Game Over! Final Score: " + totalScore);
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
