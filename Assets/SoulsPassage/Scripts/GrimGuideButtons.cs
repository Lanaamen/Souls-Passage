using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;


public class GrimGuideButtons : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip buttonSound1; // Sound for the first button
    public AudioClip buttonSound2; // Sound for the second button
    public AudioClip buttonSound3; // Sound for the third button

    // Reference to the XRButton Interactable components
    public XRGrabInteractable button1;
    public XRGrabInteractable button2;
    public XRGrabInteractable button3;

    private void Start()
    {
        // Add listeners for button presses
        button1.onSelectEntered.AddListener(OnButton1Pressed);
        button2.onSelectEntered.AddListener(OnButton2Pressed);
        button3.onSelectEntered.AddListener(OnButton3Pressed);
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
}
