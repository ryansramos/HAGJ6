using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private InputReader _input;

    void OnEnable()
    {
        _input.EnableGameplay(true);
    }
}
