using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private InputReader _input;

    public void StartGameplay()
    {
        StopMenu();
        _input.EnableGameplay(true);
    }

    public void StopGameplay()
    {
        _input.EnableGameplay(false);
    }

    public void StartMenu()
    {
        StopGameplay();
        _input.EnableMenus(true);
    }

    public void StopMenu()
    {
        _input.EnableMenus(true);
    }
}
