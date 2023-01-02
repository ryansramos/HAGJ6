using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    [SerializeField]
    private string _sceneToLoad;
    [Header("Broadcasting  on: ")]
    [SerializeField]
    private StringEventChannelSO _sceneLoadRequestEvent;

    public Button playButton;
    public Button quitButton;
    public QuitPrompt quitPrompt;

    void OnEnable()
    {
        playButton.onClick.AddListener(OnPlay);
        quitButton.onClick.AddListener(OnQuit);
        quitPrompt.OnReturn.AddListener(OnReturnFromQuit);
    }

    void Start()
    {
        SetTitleButtons(true);
        quitPrompt.gameObject.SetActive(false);
    }

    void OnDisable()
    {
        playButton.onClick.RemoveListener(OnPlay);
        quitButton.onClick.RemoveListener(OnQuit);
        quitPrompt.OnReturn.RemoveListener(OnReturnFromQuit);
    }

    void OnPlay()
    {
        _sceneLoadRequestEvent.RaiseEvent(_sceneToLoad);
    }

    void OnQuit()
    {
        SetTitleButtons(false);
        quitPrompt.gameObject.SetActive(true);
    }

    void OnReturnFromQuit()
    {
        quitPrompt.gameObject.SetActive(false);
        SetTitleButtons(true);
    }

    void SetTitleButtons(bool status)
    {
        playButton.interactable = status;
        quitButton.interactable = status;
    }
}
