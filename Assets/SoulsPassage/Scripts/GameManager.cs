using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;   // Text display for the timer
    public TextMeshProUGUI scoreText;   // Text display for the score
    public int score = 0;               // Player's score
    private float timeRemaining = 60f;  // Countdown timer starting at 60 seconds
    private bool isGameActive = true;   // Game state

    void Start()
    {
        UpdateScoreText();
        UpdateTimerText();
    }

    void Update()
    {
        if (isGameActive)
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

    public void AddPoint()
    {
        if (isGameActive)
        {
            score++;
            UpdateScoreText();
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    private void UpdateTimerText()
    {
        timerText.text = "Time: " + Mathf.Ceil(timeRemaining).ToString() + "s";
    }

    private void EndGame()
    {
        isGameActive = false;
        timerText.text = "Time's Up!";
        Debug.Log("Game Over! Final Score: " + score);
    }
}
