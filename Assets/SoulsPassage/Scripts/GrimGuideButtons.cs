using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;  // Import TMP for handling text UI

public class GrimGuideButtons : MonoBehaviour
{
    // Individual audio sources for each button
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public AudioSource audioSource3;

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
        PlaySound(audioSource1, buttonSound1);
        DisplayTextFromFile("Text1");
    }

    public void OnButton2Pressed(XRBaseInteractor interactor)
    {
        // Play sound and display the corresponding text
        PlaySound(audioSource2, buttonSound2);
        DisplayTextFromFile("Text2");
    }

    public void OnButton3Pressed(XRBaseInteractor interactor)
    {
        // Play sound and display the corresponding text
        PlaySound(audioSource3, buttonSound3);
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

    // Play the appropriate sound using a specific audio source
    private void PlaySound(AudioSource source, AudioClip sound)
    {
        if (source != null && sound != null)
        {
            source.clip = sound;
            source.Play();
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
        if (audioSource1 != null)
        {
            audioSource1.Stop();
        }

        if (audioSource2 != null)
        {
            audioSource2.Stop();
        }

        if (audioSource3 != null)
        {
            audioSource3.Stop();
        }

        if (introAudioSource != null)
        {
            introAudioSource.Stop();  // Stop the intro sound as well
        }
    }
}
