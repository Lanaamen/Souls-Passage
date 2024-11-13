using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScytheButtonPress : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has a Button component
        Button button = other.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.Invoke();  // Trigger the button’s onClick event
        }
    }
}
