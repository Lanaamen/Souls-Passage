using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;  // Import TMP for handling text UI

public class GrimGuideButtons : MonoBehaviour
{
    public AudioSource audioSource; // General audio source for button sounds
    public AudioClip buttonSound1;
    public AudioClip buttonSound2;
    public AudioClip buttonSound3;

    public XRGrabInteractable button1;
    public XRGrabInteractable button2;
    public XRGrabInteractable button3;
    public XRGrabInteractable stopButton;  // New button to stop all sounds

    public AudioSource introAudioSource;  // AudioSource for the intro sound

    public TextAsset textFile; // Reference to the text file
    public TextMeshProUGUI displayText; // Text UI component to display text

    private void Start()
    {
        // Add listeners for button presses
        button1.onSelectEntered.AddListener(OnButton1Pressed);
        button2.onSelectEntered.AddListener(OnButton2Pressed);
        button3.onSelectEntered.AddListener(OnButton3Pressed);
        stopButton.onSelectEntered.AddListener(OnStopButtonPressed);  // Listen for stop button press
    }

    public void OnButton1Pressed(XRBaseInteractor interactor)
    {
        // Play sound and display the corresponding text
        PlaySound(buttonSound1);
        DisplayTextFromFile("Text1");
    }

    public void OnButton2Pressed(XRBaseInteractor interactor)
    {
        // Play sound and display the corresponding text
        PlaySound(buttonSound2);
        DisplayTextFromFile("Text2");
    }

    public void OnButton3Pressed(XRBaseInteractor interactor)
    {
        // Play sound and display the corresponding text
        PlaySound(buttonSound3);
        DisplayTextFromFile("Text3");
    }

    private void DisplayTextFromFile(string key)
    {
        if (textFile != null && displayText != null)
        {
            // Find the corresponding text for the key in the text file
            string[] lines = textFile.text.Split('\n');
            foreach (string line in lines)
            {
                if (line.StartsWith(key))
                {
                    displayText.text = line.Substring(key.Length + 1); // +1 to remove the key and the space
                    break;
                }
            }
        }
    }

    // Play the appropriate sound when a button is pressed
    private void PlaySound(AudioClip sound)
    {
        if (audioSource != null && sound != null)
        {
            audioSource.clip = sound;
            audioSource.Play();
        }
    }

    // Stop all sounds, including the intro sound, when the stop button is pressed
    public void OnStopButtonPressed(XRBaseInteractor interactor)
    {
        StopAllSounds();
    }

    // Stop the audio from playing, including the intro sound
    private void StopAllSounds()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }

        if (introAudioSource != null)
        {
            introAudioSource.Stop();  // Stop the intro sound as well
        }
    }
}
