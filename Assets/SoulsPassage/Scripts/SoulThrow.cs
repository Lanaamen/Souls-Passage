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
    private bool isGoodSoul; // To track if the soul is good or bad, set from SoulManager

    private GameManager gameManager; // Reference to the GameManager for points

    private void Start()
    {
        // Store the initial position of the soul object
        initialPosition = new Vector3(-0.3337124f, 1.596273f, -6.39f);

        // Set isKinematic to true initially to prevent gravity when being held
        soulRigidbody.isKinematic = true;

        // Find and assign the GameManager reference
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (grabInteractable.isSelected)
        {
            // When grabbed, make the object follow the hand, but disable physics interaction
            isBeingHeld = true;
            soulRigidbody.isKinematic = true; // Keep it kinematic while being held
            transform.position = grabInteractable.transform.position;
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

    // Set the soul's good/bad status from the SoulManager
    public void SetSoulStatus(bool isGood)
    {
        isGoodSoul = isGood;
    }

    // When the soul touches the door, it will disappear
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HellDoor") || other.CompareTag("HeavenDoor"))
        {
            // Check which door the soul is entering and log the results
            if (other.CompareTag("HellDoor"))
            {
                Debug.Log("Soul entered HellDoor.");
                if (isGoodSoul)
                {
                    Debug.Log("But this is a good soul! Shouldn't be here.");
                }
                else
                {
                    Debug.Log("This is a bad soul entering hell.");
                    gameManager.AddPoint(); // Add a point for correct entry
                }
            }
            else if (other.CompareTag("HeavenDoor"))
            {
                Debug.Log("Soul entered HeavenDoor.");
                if (isGoodSoul)
                {
                    Debug.Log("This is a good soul entering heaven.");
                    gameManager.AddPoint(); // Add a point for correct entry
                }
                else
                {
                    Debug.Log("But this is a bad soul! Shouldn't be here.");
                }
            }

            // Hide or deactivate the soul when it passes through the door
            gameObject.SetActive(false);
        }
    }

    // Reset the soul's position to its initial position
    public void ResetSoulPosition()
    {
        transform.position = initialPosition; // Reset to the initial position
        soulRigidbody.isKinematic = true; // Reset physics state
    }
}
