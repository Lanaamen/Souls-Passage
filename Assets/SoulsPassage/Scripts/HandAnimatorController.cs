using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimatorController : MonoBehaviour
{
    public InputActionProperty gripAnimationAction;
    public Animator handAnimator;

    void Update()
    {
        float gripValue = gripAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);
    }
}