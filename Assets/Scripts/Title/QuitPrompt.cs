using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class QuitPrompt : MonoBehaviour
{
    public UnityEvent OnReturn;
    public Button confirmButton;
    public Button backButton;

    void OnEnable()
    {
        confirmButton.onClick.AddListener(OnConfirm);
        backButton.onClick.AddListener(OnBack);
    }

    void OnDisable()
    {
        confirmButton.onClick.RemoveListener(OnConfirm);
        backButton.onClick.RemoveListener(OnBack);
    }

    void OnConfirm()
    {
        Application.Quit();
    }

    void OnBack()
    {
        OnReturn?.Invoke();
    }
}
