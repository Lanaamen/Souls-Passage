using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTest : MonoBehaviour
{
    private Button button;
    private Color originalColor;

    void Start()
    {
        // Get the Button component
        button = GetComponent<Button>();

        // Save the original color of the button
        originalColor = button.image.color;

        // Assign the OnButtonClick function to the button's OnClick event
        button.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        // Change the button's color to green as feedback
        button.image.color = Color.green;

        // Print a message in the console
        Debug.Log("Button clicked!");

        // Optionally, reset color after a short delay
        Invoke("ResetColor", 0.5f);
    }

    void ResetColor()
    {
        // Reset the button's color back to original
        button.image.color = originalColor;
    }
}
