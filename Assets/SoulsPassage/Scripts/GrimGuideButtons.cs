using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

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

    // Background music
    public AudioSource backgroundAudioSource; // New AudioSource for background sound
    public AudioClip backgroundMusic; // Background sound clip

    public TextAsset textFile; // Reference to the text file
    public TextMeshProUGUI displayText; // Text UI component to display text

    private void Start()
    {
        PlayBackgroundMusic(); // Start background music

        // Attach listeners to button events
        button1.onSelectEntered.AddListener(OnButton1Pressed);
        button2.onSelectEntered.AddListener(OnButton2Pressed);
        button3.onSelectEntered.AddListener(OnButton3Pressed);
        stopButton.onSelectEntered.AddListener(OnStopButtonPressed);  // Listen for stop button press
    }

    public void OnButton1Pressed(XRBaseInteractor interactor)
    {
        PlaySound(audioSource1, buttonSound1);
        DisplayTextFromFile("Text1");
    }

    public void OnButton2Pressed(XRBaseInteractor interactor)
    {
        PlaySound(audioSource2, buttonSound2);
        DisplayTextFromFile("Text2");
    }

    public void OnButton3Pressed(XRBaseInteractor interactor)
    {
        PlaySound(audioSource3, buttonSound3);
        DisplayTextFromFile("Text3");
    }

    private void DisplayTextFromFile(string key)
    {
        if (textFile != null && displayText != null)
        {
            string[] lines = textFile.text.Split('\n');
            foreach (string line in lines)
            {
                if (line.StartsWith(key))
                {
                    displayText.text = line.Substring(key.Length + 1); // Remove key and space
                    break;
                }
            }
        }
    }

    private void PlaySound(AudioSource source, AudioClip sound)
    {
        if (source != null && sound != null)
        {
            StopAllSoundsExcept(source); // Ensure only one sound plays
            source.clip = sound;
            source.Play();
        }
    }

    private void StopAllSoundsExcept(AudioSource exceptionSource)
    {
        // Stop all sounds except the one specified
        foreach (AudioSource source in GetComponents<AudioSource>())
        {
            if (source != exceptionSource && source.isPlaying)
            {
                source.Stop();
            }
        }

        // Also stop introAudioSource if it's not the exception
        if (introAudioSource != exceptionSource && introAudioSource != null && introAudioSource.isPlaying)
        {
            introAudioSource.Stop();
        }
    }

    public void OnStopButtonPressed(XRBaseInteractor interactor)
    {
        StopAllSounds();
    }

    private void StopAllSounds()
    {
        foreach (AudioSource source in GetComponents<AudioSource>())
        {
            if (source.isPlaying)
            {
                source.Stop();
            }
        }
    }

    private void PlayBackgroundMusic()
    {
        if (backgroundAudioSource != null && backgroundMusic != null)
        {
            backgroundAudioSource.clip = backgroundMusic;
            backgroundAudioSource.loop = true;  // Ensure background music loops
            backgroundAudioSource.Play();
        }
    }
}
