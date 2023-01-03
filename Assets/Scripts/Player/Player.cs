using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField]
    private InputReader _input;

    [SerializeField]
    public Hammer 
        LHammer,
        RHammer;
    
    [SerializeField]
    public RageManager rageManager;

    void OnEnable()
    {
        _input.OnPrimaryEvent += OnPrimary;
        _input.OnSecondaryEvent += OnSecondary;
        _input.OnResetPrimaryEvent += OnResetPrimary;
        _input.OnResetSecondaryEvent += OnResetSecondary;
        _input.OnInteractEvent += OnInteract;
        _input.OnRageEvent += OnRage;
    }
    void OnDisable()
    {
        _input.OnPrimaryEvent -= OnPrimary;
        _input.OnSecondaryEvent -= OnSecondary;
        _input.OnResetPrimaryEvent -= OnResetPrimary;
        _input.OnResetSecondaryEvent -= OnResetSecondary;
        _input.OnInteractEvent -= OnInteract;
        _input.OnRageEvent -= OnRage;
    }

    void OnPrimary()
    {
        LHammer.Swing();
    }

    void OnSecondary()
    {
        RHammer.Swing();
    }

    void OnResetPrimary()
    {
        LHammer.TryResetCooldown();
    }

    void OnResetSecondary()
    {
        RHammer.TryResetCooldown();
    }

    void OnRage()
    {
        rageManager.RageInput();
    }

    void OnInteract()
    {
    }
}
