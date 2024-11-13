using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoulManager : MonoBehaviour
{
    public TextMeshProUGUI riddleText;  // Reference to the TextMeshPro component for displaying the riddle
    public GameObject soul;  // Reference to the Soul particle system
    [SerializeField] private Riddle[] riddles;  // Array of Riddle ScriptableObject references
    private int currentRiddleIndex = 0;  // Keeps track of which riddle to show
    private SoulThrow soulThrow; // Reference to the SoulThrow script

    void Start()
    {
        soulThrow = soul.GetComponent<SoulThrow>();

        // Start with both the soul and riddle text hidden
        soul.SetActive(false);
        riddleText.text = "";
    }

    public void ShowRiddleAndSoul()
    {
        if (currentRiddleIndex < riddles.Length)
        {
            soulThrow.ResetSoulPosition(); // Reset and show the soul
            soul.SetActive(true);

            riddleText.text = riddles[currentRiddleIndex].riddleText; // Display the riddle text

            soulThrow.SetSoulStatus(riddles[currentRiddleIndex].isGoodSoul); // Set the soul's status
            currentRiddleIndex++; // Move to the next riddle
        }
        else
        {
            riddleText.text = "No more riddles!";
        }
    }

    public void ResetSoulAndRiddle()
    {
        currentRiddleIndex = 0;
        soul.SetActive(false);
        riddleText.text = "";
    }
}
