using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    [SerializeField]
    private string _sceneToLoad;

    [SerializeField]
    private string _returnScene;
    
    [Header("Broadcasting  on: ")]
    [SerializeField]
    private StringEventChannelSO _sceneLoadRequestEvent;

    public Button playButton;
    // public Button quitButton;
    // public QuitPrompt quitPrompt;
    public Button returnButton;

    public MusicRequester _player;

    void OnEnable()
    {
        playButton.onClick.AddListener(OnPlay);
        returnButton.onClick.AddListener(OnReturn);
        // quitButton.onClick.AddListener(OnQuit);
        // quitPrompt.OnReturn.AddListener(OnReturnFromQuit);
    }

    void Start()
    {
        SetTitleButtons(true);
        _player.Play();
        // quitPrompt.gameObject.SetActive(false);
    }

    void OnDisable()
    {
        playButton.onClick.RemoveListener(OnPlay);
        returnButton.onClick.RemoveListener(OnReturn);
        // quitButton.onClick.RemoveListener(OnQuit);
        // quitPrompt.OnReturn.RemoveListener(OnReturnFromQuit);
    }

    void OnPlay()
    {
        _sceneLoadRequestEvent.RaiseEvent(_sceneToLoad);
    }

    void OnReturn()
    {
        _sceneLoadRequestEvent.RaiseEvent(_returnScene);
    }

    void OnQuit()
    {
        SetTitleButtons(false);
        // quitPrompt.gameObject.SetActive(true);
    }

    void OnReturnFromQuit()
    {
        // quitPrompt.gameObject.SetActive(false);
        SetTitleButtons(true);
    }

    void SetTitleButtons(bool status)
    {
        playButton.interactable = status;
        returnButton.interactable = status;
        // quitButton.interactable = status;
    }
}
