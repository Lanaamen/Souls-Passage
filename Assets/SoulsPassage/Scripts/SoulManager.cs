using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoulManager : MonoBehaviour
{
    public TextMeshProUGUI riddleText;  // Reference to the TextMeshPro component for displaying the riddle
    public GameObject soul;  // Reference to the Soul particle system
    [SerializeField] private Riddle[] riddles;  // Array of Riddle ScriptableObject references
    private int currentRiddleIndex = 0;  // Keeps track of which riddle to show

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
            // Show the soul and display the current riddle text
            soul.SetActive(true);
            riddleText.text = riddles[currentRiddleIndex].riddleText;  // Set riddle text

            // You can check the `isGoodSoul` to determine if the soul should go to heaven or hell
            bool isGood = riddles[currentRiddleIndex].isGoodSoul;
            Debug.Log(isGood ? "This soul is good!" : "This soul is bad!");

            // Move to the next riddle for the next button press
            currentRiddleIndex++;
        }
        else
        {
            riddleText.text = "No more riddles!";
        }
    }

    // Reset or hide the riddle and soul if needed
    public void ResetSoulAndRiddle()
    {
        currentRiddleIndex = 0;
        soul.SetActive(false);
        riddleText.text = "";
    }
}
