using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrimGuideButtons : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip buttonSound1;
    public AudioClip buttonSound2;
    public AudioClip buttonSound3;


    public XRGrabInteractable button1;
    public XRGrabInteractable button2;
    public XRGrabInteractable button3;
    public XRGrabInteractable stopButton;  // New button to stop all sounds

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
        PlaySound(buttonSound1);
    }

    public void OnButton2Pressed(XRBaseInteractor interactor)
    {
        PlaySound(buttonSound2);
    }

    public void OnButton3Pressed(XRBaseInteractor interactor)
    {
        PlaySound(buttonSound3);
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

    // Stop all sounds when the stop button is pressed
    public void OnStopButtonPressed(XRBaseInteractor interactor)
    {
        StopAllSounds();
    }

    // Stop the audio from playing
    private void StopAllSounds()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}
