using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScytheGrab : XRGrabInteractable
{
    private bool isHeld = false; // Håller koll på om objektet hålls

    // Override för att hantera interaktionen vid objektets selektion
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        isHeld = true; // När objektet tas upp sätts isHeld till true
    }

    // Override för att hantera interaktionen vid objektets selektion
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        isHeld = false; // När objektet släpps sätts isHeld till false
    }

    // Denna funktion kommer att anropas när spelaren byter hand
    // eller om objektet ska släppas
    public void TryRelease()
    {
        if (isHeld)
        {
            base.OnSelectExited(new SelectExitEventArgs()); // Släpp objektet om det hålls
        }
    }
}
