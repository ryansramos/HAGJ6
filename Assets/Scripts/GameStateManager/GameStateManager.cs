using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameTimer timer;
    public InputManager input;
    public Scorekeeper score;
    public UIManager ui;
    public FrameManager frame;
    public RageManager rage;
    public Player player;
    public MusicRequester gameMusic;

    [Header("Listening on:")]
    [SerializeField]
    private VoidEventChannelSO _onResetEvent;

    void OnEnable()
    {
        timer.gameTimeElapsedEvent.AddListener(OnGameTimerElapsed);
        score.onScoreCompleteEvent.AddListener(OnScoreComplete);
        _onResetEvent.OnEventRaised += OnReset;
    }

    void OnDisable()
    {
        timer.gameTimeElapsedEvent.RemoveListener(OnGameTimerElapsed);
        score.onScoreCompleteEvent.RemoveListener(OnScoreComplete);
        _onResetEvent.OnEventRaised -= OnReset;
    }

    void Start()
    {
        StartGameplay();
    }

    void OnReset()
    {
        StartGameplay();
    }

    void StartGameplay()
    {
        ui.OnGameStart();
        timer.StartTimer();
        input.StartGameplay();
        score.StartGameplay();
        frame.OnGameStart();
        rage.OnGameStart();
        player.OnGameStart();
        gameMusic.Play();
    }

    void StopGame()
    {
        input.StopGameplay();
        int result = score.GetScore();
        ui.OnGameOver(result);
        timer.OnGameOver();
        frame.OnGameOver();
        player.OnGameOver();
        rage.OnGameStop();
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
