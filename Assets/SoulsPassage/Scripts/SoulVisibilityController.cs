using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulVisibilityController : MonoBehaviour
{
    public GameObject soul; // The soul object you want to make visible/invisible

    // This function will be called when the button is pressed
    public void ToggleSoulVisibility()
    {
        // Toggle the active state of the soul
        soul.SetActive(!soul.activeSelf);
    }
}
