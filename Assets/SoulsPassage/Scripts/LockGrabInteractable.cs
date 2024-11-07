using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LockGrabInteractable : XRGrabInteractable
{
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        // Immediately re-select the object, so it stays in the player's hand
        if (args.interactorObject != null)
        {
            interactionManager.SelectEnter(args.interactorObject, this);
        }
    }
}
