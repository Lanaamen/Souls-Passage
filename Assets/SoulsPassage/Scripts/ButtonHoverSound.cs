using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonHoverSound : MonoBehaviour
{
    public AudioSource hoverAudioSource;  // Reference to the AudioSource for the hover sound
    public AudioClip hoverSound;  // Reference to the sound to play when hovering

    private XRGrabInteractable interactableButton;

    private void Start()
    {
        // Get the XRGrabInteractable component attached to this GameObject
        interactableButton = GetComponent<XRGrabInteractable>();

        // Add listener for hover events (when the button is hovered over)
        interactableButton.onHoverEntered.AddListener(OnHoverEntered);
    }

    private void OnHoverEntered(XRBaseInteractor interactor)
    {
        PlayHoverSound();
    }

    private void PlayHoverSound()
    {
        if (hoverAudioSource != null && hoverSound != null)
        {
            hoverAudioSource.PlayOneShot(hoverSound);  // Play the hover sound once
        }
    }
}
