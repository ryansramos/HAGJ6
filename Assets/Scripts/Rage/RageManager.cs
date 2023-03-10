using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageManager : MonoBehaviour
{
    [SerializeField]
    private VoidEventChannelSO _onPerfectCooldownEvent;

    [SerializeField]
    private VoidEventChannelSO _onCooldownFailedEvent;

    [SerializeField]
    private VoidEventChannelSO _onRageStartedEvent;

    [SerializeField]
    private VoidEventChannelSO _onRageStoppedEvent;

    [SerializeField]
    private VoidEventChannelSO _onRageAvailableEvent;

    [SerializeField]
    private VoidEventChannelSO _onRageUnavailableEvent;

    public AudioSender
        onRageSender,
        onRageAvailableSender,
        onRageUnavailableSender;

    private int _cooldownCount;

    [SerializeField]
    private GameSettingsSO _settings;

    private bool _isRageEnabled;

    void OnEnable()
    {
        _onPerfectCooldownEvent.OnEventRaised += OnPerfectCooldown;
        _onCooldownFailedEvent.OnEventRaised += OnCooldownFailed;
    }
    void OnDisable()
    {
        _onPerfectCooldownEvent.OnEventRaised -= OnPerfectCooldown;
        _onCooldownFailedEvent.OnEventRaised -= OnCooldownFailed;
        StopAllCoroutines();
        OnRageStop();
    }

    public void OnGameStart()
    {
        _cooldownCount = 0;
        DisableRage();
    }

    public void OnGameStop()
    {
        StopAllCoroutines();
        OnRageStop();
    }

    void OnPerfectCooldown()
    {
        _cooldownCount++;
        if (_cooldownCount >= _settings.RageThreshold && !_isRageEnabled)
        {
            EnableRage();
        }
    }

    void OnCooldownFailed()
    {
        _cooldownCount = 0;
        if (_isRageEnabled)
        {
            onRageUnavailableSender.Play();
            DisableRage();
        }
    }

    void EnableRage()
    {
        onRageAvailableSender.Play();
        _isRageEnabled = true;
        _onRageAvailableEvent.RaiseEvent();
    }

    void DisableRage()
    {
        _isRageEnabled = false;
        _onRageUnavailableEvent.RaiseEvent();
    }

    public void RageInput()
    {
        if (_isRageEnabled)
        {
            StartCoroutine(Rage(_settings.RageDuration));
            _cooldownCount = 0;
            DisableRage();
        }
    }

    IEnumerator Rage(float duration)
    {
        onRageSender.Play();
        _onRageStartedEvent.RaiseEvent();
        yield return new WaitForSeconds(duration);
        OnRageStop();
    }

    void OnRageStop()
    {
        _onRageStoppedEvent.RaiseEvent();
        onRageSender.Stop();
    }
}
