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

    void OnEnable()
    {
        _input.OnPrimaryEvent += OnPrimary;
        _input.OnSecondaryEvent += OnSecondary;
        _input.OnInteractEvent += OnInteract;
    }
    void OnDisable()
    {
        _input.OnPrimaryEvent -= OnPrimary;
        _input.OnSecondaryEvent -= OnSecondary;
        _input.OnInteractEvent -= OnInteract;
    }

    void OnPrimary()
    {
        LHammer.Swing();
    }

    void OnSecondary()
    {
        RHammer.Swing();
    }

    void OnInteract()
    {
    }
}