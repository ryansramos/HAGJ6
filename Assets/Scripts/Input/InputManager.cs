using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private InputReader _input;

    public void StartGameplay()
    {
        _input.EnableGameplay(true);
    }

    public void StopGameplay()
    {
        _input.EnableGameplay(false);
    }
}
