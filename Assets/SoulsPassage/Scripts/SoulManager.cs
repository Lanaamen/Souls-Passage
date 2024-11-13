using System.Collections.Generic;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoulManager : MonoBehaviour
{
    public TextMeshProUGUI riddleText;  // Reference to the TextMeshPro component for displaying the riddle
    public GameObject soul;  // Reference to the Soul particle system
    [SerializeField] private Riddle[] riddles;  // Array of Riddle ScriptableObject references
    private int currentRiddleIndex = 0;  // Keeps track of which riddle to show

    public Animator soulAnimator;  // Reference to the Animator for soul animations

    void Start()
    {
        // Start with both the soul and riddle text hidden
        soul.SetActive(false);
        riddleText.text = "";
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

            // You can use this info later to trigger the appropriate animation

            // Move to the next riddle for the next button press
            currentRiddleIndex++;
        }
        else
        {
            riddleText.text = "No more riddles!";
        }
    }

    // Call this method when the heaven button is pressed
    public void SendSoulToHeaven()
    {
        // Trigger the animation to send the soul to Heaven
        soulAnimator.SetTrigger("HeavenTrigger");  // Use the trigger set in the Animator
        Debug.Log("Sending soul to Heaven");
    }

    // Call this method when the hell button is pressed
    public void SendSoulToHell()
    {
        // Trigger the animation to send the soul to Hell
        soulAnimator.SetTrigger("HellTrigger");  // Use the trigger set in the Animator
        Debug.Log("Sending soul to Hell");
    }

    // Reset or hide the riddle and soul if needed
    public void ResetSoulAndRiddle()
    {
        currentRiddleIndex = 0;
        soul.SetActive(false);
        riddleText.text = "";
    }
}
