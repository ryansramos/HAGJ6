using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameOverUI _gameOver;
    public CooldownBar[] _cooldownBars;

    public void OnGameStart()
    {
        _gameOver.OnGameStart();
        foreach (CooldownBar bar in _cooldownBars)
        {
            bar.OnGameStart();
        }
    }
    public void OnGameOver(int score)
    {
        _gameOver.OnGameOver(score);
        foreach (CooldownBar bar in _cooldownBars)
        {
            bar.OnGameOver();
        }
    }
}
