using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using TMPro;

public class SoulThrow : MonoBehaviour
{
    public XRGrabInteractable grabInteractable; // Reference to the XRGrabInteractable
    public Rigidbody soulRigidbody; // The Rigidbody attached to the soul (the parent object)
    public TMP_Text scoreText; // Text to display the total score
    public TMP_Text correctText; // Text to display the correct answers
    public TMP_Text wrongText; // Text to display the wrong answers
    public TMP_Text timerText; // Text to display the timer
    private int score = 0; // The player's total score (correct - wrong)
    private int correctAnswers = 0; // Correct answers count
    private int wrongAnswers = 0; // Wrong answers count
    public float timer = 60f; // 60 seconds countdown timer
    private Vector3 initialPosition = new Vector3(-0.3337124f, 1.596273f, -6.39f); // Store initial position of the soul
    private bool isBeingHeld = false;
    private bool isGoodSoul; // To track if the soul is good or bad
    private bool hasMissed = false; // Flag to ensure only one deduction per missed throw

    // Reference to the SoulManager
    public SoulManager soulManager;

    // Audio sources for correct and incorrect placements
    public AudioSource correctPlacementAudio;
    public AudioSource badSoulInHeavenAudio;
    public AudioSource goodSoulInHellAudio;
    public AudioSource missedThrowAudio; // New audio source for missed throws

    // Game Over UI Elements
    public GameObject gameOverPanel;  // The panel that will show the game over screen
    public TMP_Text gameOverText;     // The text to display game over message

    // Win UI Elements
    public GameObject winPanel;  // The panel that will show the win screen
    public TMP_Text winText;     // The text to display win message
    
    private bool gameOver = false;    // Flag to check if game is over
    private bool gameWon = false;     // Flag to check if the player has won

    private void Start()
    {
        // Set initial score and timer text
        UpdateScoreText();
        UpdateCorrectText();
        UpdateWrongText();
        UpdateTimerText();
        // Initialize position and physics settings for the soul
        ResetSoulPosition();
        soulRigidbody.isKinematic = true; // Prevent gravity when being held

        // Hide the Game Over and Win screens initially
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
    }

    private void Update()
    {
        if (gameOver || gameWon) return; // If game is over or won, skip the update

        HandleTimer();

        if (grabInteractable.isSelected)
        {
            // When grabbed, make the object follow the hand, but disable physics interaction
            isBeingHeld = true;
            soulRigidbody.isKinematic = true; // Keep it kinematic while being held
            transform.position = grabInteractable.transform.position;
            transform.rotation = grabInteractable.transform.rotation;
        }
        else if (isBeingHeld)
        {
            // After release, apply force to throw the soul
            soulRigidbody.isKinematic = false; // Let physics control it again
            Vector3 throwDirection = grabInteractable.transform.forward; // Direction the soul is thrown
            soulRigidbody.AddForce(throwDirection * 10f, ForceMode.Impulse); // Apply throw force
            isBeingHeld = false;
        }

        // Check if the game is over or won
        CheckGameOver();
        CheckGameWon();
    }

    private void HandleTimer()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateTimerText();
        }

        if (timer <= 0)
        {
            timer = 0;
            Debug.Log("Time's up!");
            CheckGameOver(); // Check if the game is over when the time runs out
        }
    }

    private void CheckGameOver()
    {
        if (timer <= 0 && (correctAnswers < 15 || wrongAnswers > 2))
        {
            TriggerGameOver("You failed to guide enough souls to their rightful place... \nBetter luck next time!");
        }
    }

    private void CheckGameWon()
    {
        if (correctAnswers >= 15 && wrongAnswers <= 2 && timer > 0 && !gameWon)
        {
            TriggerWin("Congratulations! You have successfully guided enough souls back to their rightful place!");
        }
    }

    // Trigger the Game Over screen
    private void TriggerGameOver(string message)
    {
        gameOver = true;  // Set the gameOver flag to true
        gameOverText.text = message;  // Set the game over text
        gameOverPanel.SetActive(true);  // Show the game over panel
    }

    // Trigger the Win screen
    private void TriggerWin(string message)
    {
        gameWon = true;  // Set the gameWon flag to true
        winText.text = message;  // Set the win message
        winPanel.SetActive(true);  // Show the win panel
    }

    // Updates the total score text display
    private void UpdateScoreText()
    {
        score = correctAnswers - wrongAnswers; // Total score is correct answers - wrong answers
        scoreText.text = "Score: " + score;
    }

    // Updates the correct answers text display
    private void UpdateCorrectText()
    {
        correctText.text = "Correct: " + correctAnswers;
    }

    // Updates the wrong answers text display
    private void UpdateWrongText()
    {
        wrongText.text = "Wrong: " + wrongAnswers;
    }

    // Updates the timer text display
    private void UpdateTimerText()
    {
        timerText.text = "Time: " + Mathf.CeilToInt(timer).ToString();
    }

    // Reset the soul to its initial position
    public void ResetSoulPosition()
    {
        transform.position = initialPosition; // Reset to the initial position
        soulRigidbody.isKinematic = true;
        soulRigidbody.velocity = Vector3.zero; // Stop any motion
        soulRigidbody.angularVelocity = Vector3.zero; // Stop any rotation
        gameObject.SetActive(true); // Ensure the soul is visible
    }

    // Set the soul's good/bad status from the SoulManager
    public void SetSoulStatus(bool isGood)
    {
        isGoodSoul = isGood;
    }

    // Attach a second collider (trigger) to detect missing the doors
    private void OnTriggerEnter(Collider other)
    {
        // Check if it hits the "missed" collider
        if (other.CompareTag("MissedCollider") && !hasMissed)
        {
            hasMissed = true; // Set the flag to prevent multiple deductions
            timer -= 6f; // Deduct 6 seconds
            if (timer < 0) timer = 0; // Ensure the timer doesn't go negative
            UpdateTimerText(); // Update the timer display
            missedThrowAudio.Play(); // Play the missed throw sound
            Debug.Log("The soul has left the premises, it's gone... -10 seconds deducted");

            // Reset the flag after a short delay to allow new misses
            StartCoroutine(ResetMissedFlag());
        }

        // Check if it hits the Hell or Heaven door
        if (other.CompareTag("HellDoor") || other.CompareTag("HeavenDoor"))
        {
            bool correctDoor = (other.CompareTag("HellDoor") && !isGoodSoul) || 
                               (other.CompareTag("HeavenDoor") && isGoodSoul);

            if (correctDoor)
            {
                Debug.Log("Soul correctly sent to " + (isGoodSoul ? "Heaven." : "Hell."));
                correctAnswers++;  // Increment correct answers
                UpdateCorrectText(); // Update correct answers text
                score++;  // Add point for correct placement
                UpdateScoreText(); // Update total score
                correctPlacementAudio.Play(); // Play correct placement sound
            }
            else
            {
                // Increment wrong answers and show the wrong answer text
                Debug.Log("Soul sent to the wrong place!");
                wrongAnswers++;  // Increment wrong answers
                UpdateWrongText(); // Update wrong answers text
                score--;  // Deduct point for wrong placement
                UpdateScoreText(); // Update total score

                // Play specific sound for bad placement
                if (isGoodSoul && other.CompareTag("HellDoor"))
                {
                    goodSoulInHellAudio.Play(); // Play sound for good soul in hell
                }
                else if (!isGoodSoul && other.CompareTag("HeavenDoor"))
                {
                    badSoulInHeavenAudio.Play(); // Play sound for bad soul in heaven
                }
            }

            ResetSoulPosition(); // Reset the soul's position
        }
    }

    private IEnumerator ResetMissedFlag()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second
        hasMissed = false;  // Reset the missed flag
    }
}
