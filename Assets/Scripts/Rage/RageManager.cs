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

    private int _cooldownCount;

    [SerializeField]
    private GameSettingsSO _settings;

    [SerializeField]
    private float _rageTimer;

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
    }

    public void OnGameStart()
    {
        _cooldownCount = 0;
        _isRageEnabled = false;
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
            _isRageEnabled = false;
        }
    }

    void EnableRage()
    {
        _isRageEnabled = true;
    }

    public void RageInput()
    {
        if (_isRageEnabled)
        {
            StartCoroutine(Rage(_rageTimer));
            _isRageEnabled = false;
        }
    }

    IEnumerator Rage(float duration)
    {
        _onRageStartedEvent.RaiseEvent();
        yield return new WaitForSeconds(duration);
        _onRageStoppedEvent.RaiseEvent();
    }
}
