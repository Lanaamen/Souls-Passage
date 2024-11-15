using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrimGuideButton : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip buttonSound1;
    public AudioClip buttonSound2;
    public AudioClip buttonSound3;

    public XRGrabInteractable button1;
    public XRGrabInteractable button2;
    public XRGrabInteractable button3;

    private void Start()
    {
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

    private void PlaySound(AudioClip sound)
    {
        if (audioSource != null && sound != null)
        {
            audioSource.clip = sound;
            audioSource.Play();
        }
    }
}
