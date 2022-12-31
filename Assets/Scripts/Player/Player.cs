using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField]
    private InputReader _input;

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
    }

    void OnSecondary()
    {
    }

    void OnInteract()
    {
    }
}
