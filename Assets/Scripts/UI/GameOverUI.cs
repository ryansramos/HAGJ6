using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    private GameSettingsSO _settings;

    [Header("Broadcasting on")]
    [SerializeField]
    private VoidEventChannelSO _gameResetEvent;

    public GameObject panel;
    public Button resetButton;
    public Button quitButton;
    public TextMeshProUGUI scoreText;

    void OnEnable()
    {
        resetButton.onClick.AddListener(OnReset);
        quitButton.onClick.AddListener(OnQuit);
    }

    void OnDisable()
    {
        resetButton.onClick.RemoveListener(OnReset);
        quitButton.onClick.RemoveListener(OnQuit);
    }

    public void OnGameStart()
    {
        SetUIActive(false);
    }

    public void OnGameOver(int score)
    {
        SetUIActive(true);
        scoreText.text = 
            "You managed to break " +
            score + 
            " of " +
            _settings.TotalScore +
            " frames before dawn.";
    }

    void SetUIActive(bool status)
    {
        panel.SetActive(status);
    }
    
    void OnReset()
    {
        _gameResetEvent.RaiseEvent();
    }

    void OnQuit()
    {
        Application.Quit();
    }
}
