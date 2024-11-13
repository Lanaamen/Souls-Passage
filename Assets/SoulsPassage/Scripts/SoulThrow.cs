using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using TMPro;

public class SoulThrow : MonoBehaviour
{
    public XRGrabInteractable grabInteractable; // Reference to the XRGrabInteractable
    public Rigidbody soulRigidbody; // The Rigidbody attached to the soul (the parent object)
    public TMP_Text scoreText; // Text to display the score
    public TMP_Text timerText; // Text to display the timer
    public int score = 0; // The player's score
    public float timer = 60f; // 60 seconds countdown timer
    private Vector3 initialPosition = new Vector3(-0.3337124f, 1.596273f, -6.39f); // Store initial position of the soul
    private bool isBeingHeld = false;
    private bool isGoodSoul; // To track if the soul is good or bad

    // Reference to the SoulManager
    public SoulManager soulManager;

    private void Start()
    {
        // Set initial score and timer text
        UpdateScoreText();
        UpdateTimerText();
        // Initialize position and physics settings for the soul
        ResetSoulPosition();
        soulRigidbody.isKinematic = true; // Prevent gravity when being held
    }

    private void Update()
    {
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
    }

    // Method to handle the timer countdown
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
        }
    }

    // Updates the score text display
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
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

    // Handle interaction with doors to check and update the score
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HellDoor") || other.CompareTag("HeavenDoor"))
        {
            bool correctDoor = (other.CompareTag("HellDoor") && !isGoodSoul) || 
                               (other.CompareTag("HeavenDoor") && isGoodSoul);

            if (correctDoor)
            {
                Debug.Log("Soul correctly sent to " + (isGoodSoul ? "Heaven." : "Hell."));
                score++;
                UpdateScoreText();
            }
            else
            {
                Debug.Log("Soul sent to the wrong place!");
            }

            // Hide the soul when it passes through the door
            gameObject.SetActive(false);
        }
    }

    // Reset the soul and display a new riddle when the button is pressed
    public void OnButtonPress()
    {
        ResetSoulPosition(); // Reset the soul's position to its initial state
        soulManager.ShowRiddleAndSoul(); // Call the SoulManager to display the new riddle and soul
    }
}
