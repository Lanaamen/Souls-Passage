using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class GrimGuideButtons : MonoBehaviour
{
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public AudioSource audioSource3;

    public AudioClip buttonSound1;
    public AudioClip buttonSound2;
    public AudioClip buttonSound3;

    public XRGrabInteractable button1;
    public XRGrabInteractable button2;
    public XRGrabInteractable button3;
    public XRGrabInteractable stopButton;

    public AudioSource introAudioSource;

    public AudioSource backgroundAudioSource;
    public AudioClip backgroundMusic;

    public TextAsset textFile;
    public TextMeshProUGUI displayText;

    public SoulThrow soulThrowScript;

    private SoulManager soulManager; // Reference to SoulManager to access its audio sources

    private void Start()
    {
        // Get the SoulManager component
        soulManager = FindObjectOfType<SoulManager>(); // This assumes you have only one instance of SoulManager in the scene

        PlayBackgroundMusic();

        button1.onSelectEntered.AddListener(OnButton1Pressed);
        button2.onSelectEntered.AddListener(OnButton2Pressed);
        button3.onSelectEntered.AddListener(OnButton3Pressed);
        stopButton.onSelectEntered.AddListener(OnStopButtonPressed);
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
            StopAllSoundsExcept(source);
            source.clip = sound;
            source.Play();
        }
    }

    private void StopAllSoundsExcept(AudioSource exceptionSource)
    {
        // Stop all sounds except the one specified and exceptions
        foreach (AudioSource source in FindObjectsOfType<AudioSource>())
        {
            // Skip background, bookpage, and soul wosh sounds
            if (source != exceptionSource && source != backgroundAudioSource && 
                source != soulManager.soulWooshAudioSource && source != soulManager.bookPageAudioSource)
            {
                if (source.isPlaying)
                {
                    source.Stop();
                }
            }
        }

        // Ensure the background music is never stopped by this function
        if (backgroundAudioSource != null && backgroundAudioSource.isPlaying && backgroundAudioSource != exceptionSource)
        {
            backgroundAudioSource.Play();
        }
    }

    public void OnStopButtonPressed(XRBaseInteractor interactor)
    {
        StopAllSounds();
        soulThrowScript.StopAllSounds();  // Stop all audio in SoulThrow
    }

 private void StopAllSounds()
{
    foreach (AudioSource source in FindObjectsOfType<AudioSource>())
    {
        // Skip stopping the background audio source
        if (source != backgroundAudioSource && source.isPlaying)
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
            backgroundAudioSource.loop = true;
            backgroundAudioSource.Play();
        }
    }
}
