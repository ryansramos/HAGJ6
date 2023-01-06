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

    [SerializeField]
    private StringEventChannelSO _sceneLoadRequest;

    [SerializeField]
    private string _titleScene;

    [SerializeField]
    private string[] _rankResults;
    [SerializeField]
    private TextBlock[] _flavorResults;
    public GameObject panel;
    public Button resetButton;
    public Button quitButton;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI rankFlavorText;

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
            " frames before dawn.";
        
        int poundCost = score * 10;
        float dollarCost = (float)poundCost * 93.28f;
        damageText.text =
            "Estimated damages: £" +
            poundCost +
            " , $" +
            dollarCost.ToString("F0") +
            " 2022 USD";
        
        if (score >=63)
        {
            SetRank(0);
        }
        else if (score >= 55)
        {
            SetRank(1);
        }
        else if (score >= 40)
        {
            SetRank(2);
        }
        else if (score >= 20)
        {
            SetRank(3);
        }
        else
        {
            SetRank(4);
        }
    }

    void SetRank(int index)
    {
        rankText.text = _rankResults[index];
        rankFlavorText.text = _flavorResults[index].Text;
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
        _sceneLoadRequest.RaiseEvent(_titleScene);
    }
}
