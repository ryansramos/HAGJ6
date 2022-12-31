using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Input Reader")]
public class InputReader : ScriptableObject, Controls.IGameplayActions
{
    private Controls _controls;
    private Controls.GameplayActions _gameplay;
    public event UnityAction OnPrimaryEvent;
    public event UnityAction OnSecondaryEvent;
    public event UnityAction OnInteractEvent;

    void OnEnable()
    {
        if( _controls == null)
        {
            _controls = new Controls();
            _gameplay = _controls.Gameplay;
            _gameplay.SetCallbacks(this);
        }
    }

    public void OnPrimary(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                OnPrimaryEvent?.Invoke();
                return;
        }
    }
    public void OnSecondary(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                OnSecondaryEvent?.Invoke();
                return;
        }
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                OnInteractEvent?.Invoke();
                return;
        }
    }

    // Enable/disable
    public void EnableGameplay(bool status)
    {
        if (status)
        {
            _gameplay.Enable();
        }
        else
        {
            _gameplay.Disable();
        }
    }
}
