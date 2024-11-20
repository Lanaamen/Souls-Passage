using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoulManager : MonoBehaviour
{
    public TextMeshProUGUI riddleText;  // Reference to the text displaying the riddle
    public GameObject soul;  // Reference to the Soul particle system
    [SerializeField] 
    private Riddle[] riddles;  // Array of Riddle ScriptableObject references
    private int currentRiddleIndex = 0;  // Keeps track of which riddle to show

    public SoulThrow soulThrow; // Reference to SoulThrow to set the soul's status

    // Audio sources for sounds
    public AudioSource bookPageAudioSource;  // Audio source for the book page sound
    public AudioSource soulWooshAudioSource; // Audio source for the soul woosh sound
    public AudioClip bookPageSound;          // Audio clip for book page turning
    public AudioClip soulWooshSound;         // Audio clip for soul woosh

    void Start()
    {
        // Shuffle the riddles at the start
        ShuffleRiddles();

        // Start with both the soul and riddle text hidden
        soul.SetActive(false);
        riddleText.text = "";
    }

    // Shuffle method to randomize the order of the riddles
    private void ShuffleRiddles()
    {
        for (int i = riddles.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Riddle temp = riddles[i];
            riddles[i] = riddles[randomIndex];
            riddles[randomIndex] = temp;
        }
    }

    // This method is called to display the current riddle and show the soul
    public void ShowRiddleAndSoul()
    {
        if (currentRiddleIndex < riddles.Length)
        {
            soul.SetActive(true);  // Activate the soul particle system
            riddleText.text = riddles[currentRiddleIndex].riddleText;  // Set riddle text

            // Set the soul status (good or bad) based on the riddle
            bool isGoodSoul = riddles[currentRiddleIndex].isGoodSoul;
            soulThrow.SetSoulStatus(isGoodSoul);  // Set the soul status in the SoulThrow script

            // Play sounds simultaneously
            PlaySounds();

            // Optional debug to track progress
            Debug.Log("Showing riddle: " + riddles[currentRiddleIndex].riddleText);

            // Move to the next riddle for the next button press
            currentRiddleIndex++;
        }
    }

    // Reset or hide the riddle and soul if needed
    public void ResetSoulAndRiddle()
    {
        currentRiddleIndex = 0;
        soul.SetActive(false);  // Hide the soul
        riddleText.text = "";   // Clear the riddle text
    }

    // Play the book page sound and soul woosh sound simultaneously
    private void PlaySounds()
    {
        if (bookPageAudioSource != null && bookPageSound != null)
        {
            bookPageAudioSource.clip = bookPageSound;
            bookPageAudioSource.Play();
        }

        if (soulWooshAudioSource != null && soulWooshSound != null)
        {
            soulWooshAudioSource.clip = soulWooshSound;
            soulWooshAudioSource.Play();
        }
    }
}
