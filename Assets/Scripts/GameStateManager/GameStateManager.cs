using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameTimer timer;
    public InputManager input;

    void OnEnable()
    {
        timer.gameTimeElapsedEvent.AddListener(OnGameTimerElapsed);
    }

    void OnDisable()
    {
        timer.gameTimeElapsedEvent.RemoveListener(OnGameTimerElapsed);
    }

    void Start()
    {
        StartGameplay();
    }

    void StartGameplay()
    {
        timer.StartTimer();
        input.StartGameplay();
    }

    void OnGameTimerElapsed()
    {
        input.StopGameplay();
    }
}
