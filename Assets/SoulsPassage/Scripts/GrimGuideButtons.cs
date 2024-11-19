using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;  // Import TMP för att hantera text UI

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

    // Bakgrundsljud
    public AudioSource backgroundAudioSource; // Ny AudioSource för bakgrundsljud
    public AudioClip backgroundMusic; // Bakgrundsljudets ljudklipp

    public TextAsset textFile; // Reference to the text file
    public TextMeshProUGUI displayText; // Text UI component to display text

    private void Start()
    {
        // Spela bakgrundsljudet när spelet startar
        PlayBackgroundMusic();

        // Lägg till lyssnare för knapptryckningar
        button1.onSelectEntered.AddListener(OnButton1Pressed);
        button2.onSelectEntered.AddListener(OnButton2Pressed);
        button3.onSelectEntered.AddListener(OnButton3Pressed);
        stopButton.onSelectEntered.AddListener(OnStopButtonPressed);  // Lyssna på stopp-knapptryckning
    }

    public void OnButton1Pressed(XRBaseInteractor interactor)
    {
        // Spela ljud och visa motsvarande text
        PlaySound(audioSource1, buttonSound1);
        DisplayTextFromFile("Text1");
    }

    public void OnButton2Pressed(XRBaseInteractor interactor)
    {
        // Spela ljud och visa motsvarande text
        PlaySound(audioSource2, buttonSound2);
        DisplayTextFromFile("Text2");
    }

    public void OnButton3Pressed(XRBaseInteractor interactor)
    {
        // Spela ljud och visa motsvarande text
        PlaySound(audioSource3, buttonSound3);
        DisplayTextFromFile("Text3");
    }

    private void DisplayTextFromFile(string key)
    {
        if (textFile != null && displayText != null)
        {
            // Hitta motsvarande text för nyckeln i textfilen
            string[] lines = textFile.text.Split('\n');
            foreach (string line in lines)
            {
                if (line.StartsWith(key))
                {
                    displayText.text = line.Substring(key.Length + 1); // +1 för att ta bort nyckeln och mellanslaget
                    break;
                }
            }
        }
    }

    // Spela det valda ljudet när en knapp trycks
    private void PlaySound(AudioSource source, AudioClip sound)
    {
        if (source != null && sound != null)
        {
            // Stoppa introljudet om det spelas
            if (introAudioSource != null && introAudioSource.isPlaying)
            {
                introAudioSource.Stop();
            }

            // Stoppa det eventuella ljudet som redan spelas innan vi spelar ett nytt ljud
            if (source.isPlaying)
            {
                source.Stop();
            }

            source.clip = sound;
            source.Play();
        }
    }

    // Stoppa alla ljud, inklusive intro-ljudet, när stopp-knappen trycks
    public void OnStopButtonPressed(XRBaseInteractor interactor)
    {
        StopAllSounds();
    }

    // Stoppa ljuden, men inte bakgrundsljudet
    private void StopAllSounds()
    {
        if (audioSource1 != null && audioSource1.isPlaying)
        {
            audioSource1.Stop();
        }

        if (audioSource2 != null && audioSource2.isPlaying)
        {
            audioSource2.Stop();
        }

        if (audioSource3 != null && audioSource3.isPlaying)
        {
            audioSource3.Stop();
        }

        if (introAudioSource != null && introAudioSource.isPlaying)
        {
            introAudioSource.Stop();  // Stoppa även intro-ljudet om det spelas
        }
    }

    // Spela bakgrundsljudet och säkerställa att det fortsätter spelas även om andra ljud stoppas
    private void PlayBackgroundMusic()
    {
        if (backgroundAudioSource != null && backgroundMusic != null)
        {
            // Se till att bakgrundsljudet är igång och loopas
            if (!backgroundAudioSource.isPlaying)
            {
                backgroundAudioSource.clip = backgroundMusic;
                backgroundAudioSource.loop = true;  // Se till att bakgrundsljudet loopas
                backgroundAudioSource.Play();
            }
        }
    }
}
