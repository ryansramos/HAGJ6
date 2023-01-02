using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Input Reader")]
public class InputReader : ScriptableObject, Controls.IGameplayActions, Controls.IMenusActions
{
    private Controls _controls;
    private Controls.GameplayActions _gameplay;
    public event UnityAction OnPrimaryEvent;
    public event UnityAction OnSecondaryEvent;
    public event UnityAction OnResetPrimaryEvent;
    public event UnityAction OnResetSecondaryEvent;
    public event UnityAction OnInteractEvent;
    public event UnityAction<Vector2> OnAimEvent;

    // Menus
    private Controls.MenusActions _menus;
    public event UnityAction OnProceedEvent;

    void OnEnable()
    {
        if( _controls == null)
        {
            _controls = new Controls();
            _gameplay = _controls.Gameplay;
            _gameplay.SetCallbacks(this);
            _menus = _controls.Menus;
            _menus.SetCallbacks(this);
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

    public void OnResetPrimary(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                OnResetPrimaryEvent?.Invoke();
                return;
        }
    }

    public void OnResetSecondary(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                OnResetSecondaryEvent?.Invoke();
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

    public void OnAim(InputAction.CallbackContext context)
    {
        Vector2 aimPosition = context.ReadValue<Vector2>();
        OnAimEvent?.Invoke(aimPosition);
    }

    public void OnProceed(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                OnProceedEvent?.Invoke();
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

    public void EnableMenus(bool status)
    {
        if (status)
        {
            _menus.Enable();
        }
        else
        {
            _menus.Disable();
        }
    }
}
