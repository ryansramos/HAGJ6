using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagePrompt : MonoBehaviour
{
    
    public GameObject _textObj;

    [SerializeField]
    private VoidEventChannelSO _onRageAvailableEvent;

    [SerializeField]
    private VoidEventChannelSO _onRageUnavailableEvent;

    void OnEnable()
    {
        _onRageAvailableEvent.OnEventRaised += OnRageAvailable;
        _onRageUnavailableEvent.OnEventRaised += OnRageUnavailable;
    }

    void OnDisable()
    {
        _onRageAvailableEvent.OnEventRaised -= OnRageAvailable;
        _onRageUnavailableEvent.OnEventRaised -= OnRageUnavailable;
    }

    void OnRageAvailable()
    {
        _textObj.SetActive(true);
    }

    void OnRageUnavailable()
    {
        _textObj.SetActive(false);
    }
}
