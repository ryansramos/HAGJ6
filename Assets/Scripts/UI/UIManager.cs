using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameOverUI _gameOver;
    public RageUI _rageUI;
    public CooldownBar[] _cooldownBars;

    [SerializeField]
    private VoidEventChannelSO 
        _onRageStartEvent,
        _onRageStopEvent;


    void OnEnable()
    {
        _onRageStartEvent.OnEventRaised += OnRageStart;
        _onRageStopEvent.OnEventRaised += OnRageStop;
    }
    void OnDisable()
    {
        _onRageStartEvent.OnEventRaised -= OnRageStart;
        _onRageStopEvent.OnEventRaised -= OnRageStop;
    }

    public void OnGameStart()
    {
        _gameOver.OnGameStart();
        OnRageStop();
        foreach (CooldownBar bar in _cooldownBars)
        {
            bar.OnGameStart();
        }
    }
    public void OnGameOver(int score)
    {
        _gameOver.OnGameOver(score);
        OnRageStop();
        foreach (CooldownBar bar in _cooldownBars)
        {
            bar.OnGameOver();
        }
    }

    void OnRageStart()
    {
        _rageUI.OnRageStart();
    }

    void OnRageStop()
    {
        _rageUI.OnRageStop();
    }
}
