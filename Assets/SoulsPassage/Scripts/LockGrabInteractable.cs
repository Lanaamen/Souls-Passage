using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LockGrabInteractable : XRGrabInteractable
{
    private bool isBeingHeld = false;

    // Flagga för att hålla reda på om objektet är i handen
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        isBeingHeld = true;  // När objektet tas, sätt flaggan till true
    }

    // Denna metod förhindrar att objektet släpps
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        if (isBeingHeld)
        {
            // Om objektet är i handen, återställ objektet till handen utan att släppa det
            if (args.interactorObject != null)
            {
                interactionManager.SelectEnter(args.interactorObject, this);
            }
        }
        else
        {
            base.OnSelectExited(args);  // Standardbeteende om objektet inte är i handen
        }
    }

    // För att tillåta handbyte, men förhindra att objektet släpps
    protected override void OnSelectUpdated(SelectUpdateEventArgs args)
    {
        base.OnSelectUpdated(args);

        // Om objektet är hålls, se till att det hålls av rätt interaktor (handen)
        if (isBeingHeld)
        {
            if (args.interactorObject != interactableObject)
            {
                interactionManager.SelectEnter(args.interactorObject, this);
            }
        }
    }
}
