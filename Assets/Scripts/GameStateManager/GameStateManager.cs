using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameTimer timer;
    public InputManager input;
    public Scorekeeper score;

    void OnEnable()
    {
        timer.gameTimeElapsedEvent.AddListener(OnGameTimerElapsed);
        score.onScoreCompleteEvent.AddListener(OnScoreComplete);
    }

    void OnDisable()
    {
        timer.gameTimeElapsedEvent.RemoveListener(OnGameTimerElapsed);
        score.onScoreCompleteEvent.RemoveListener(OnScoreComplete);
    }

    void Start()
    {
        StartGameplay();
    }

    void StartGameplay()
    {
        timer.StartTimer();
        input.StartGameplay();
        score.StartGameplay();
    }

    void StopGame()
    {
        input.StopGameplay();
    }

    void OnGameTimerElapsed()
    {
        StopGame();
    }

    void OnScoreComplete()
    {
        StopGame();
    }
}
