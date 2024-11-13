using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SoulThrow : MonoBehaviour
{
    public XRGrabInteractable grabInteractable; // Reference to the XRGrabInteractable
    public Rigidbody soulRigidbody; // The Rigidbody attached to the soul (the parent object)
    public float throwForce = 10f; // The force with which the soul will be thrown
    private Vector3 initialPosition; // Store initial position of the soul
    private bool isBeingHeld = false;

    private void Start()
    {
        // Store the initial position of the soul object
        initialPosition = transform.position;

        // Set isKinematic to true initially to prevent gravity when being held
        soulRigidbody.isKinematic = true;
    }

    private void Update()
    {
        if (grabInteractable.isSelected)
        {
            // When grabbed, make the object follow the hand, but disable physics interaction
            isBeingHeld = true;
            soulRigidbody.isKinematic = true; // Keep it kinematic while being held

            // Optionally, you can set the position of the soul to match the hand's position smoothly
            // If you want it to follow the hand but without jitter, you can use a smoother method like Lerp
            transform.position = grabInteractable.transform.position;

            // Make sure the soul follows the hand smoothly
            transform.rotation = grabInteractable.transform.rotation;
        }
        else if (isBeingHeld)
        {
            // After release, apply the force to "throw" the soul
            soulRigidbody.isKinematic = false; // Let physics control it again
            Vector3 throwDirection = grabInteractable.transform.forward; // Direction the soul is thrown
            soulRigidbody.AddForce(throwDirection * throwForce, ForceMode.Impulse); // Apply throw force
            isBeingHeld = false;
        }
    }

    // Optionally, add a method to reset the position if needed
    public void ResetSoulPosition()
    {
        transform.position = initialPosition; // Reset to the initial position
    }
}
