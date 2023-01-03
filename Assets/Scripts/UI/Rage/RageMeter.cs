using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageMeter : MonoBehaviour
{
    [SerializeField]
    private VoidEventChannelSO _onPerfectCooldown;

    [SerializeField]
    private VoidEventChannelSO _onCooldownFailed;

    [SerializeField]
    private GameSettingsSO _settings;

    private int _cooldownCount;

    private RectTransform _meter;

    void Awake()
    {
        _meter = gameObject.GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        _onPerfectCooldown.OnEventRaised += OnPerfectCooldown;
        _onCooldownFailed.OnEventRaised += OnCooldownFailed;
    }
    void OnDisable()
    {
        _onPerfectCooldown.OnEventRaised -= OnPerfectCooldown;
        _onCooldownFailed.OnEventRaised -= OnCooldownFailed;
    }

    public void OnRageStart()
    {

    }
    public void OnRageStop()
    {
        _cooldownCount = 0;
        _meter.localScale = Vector3.one;
    }

    void OnPerfectCooldown()
    {
        if (_settings.RageThreshold <= 0)
        {
            return;
        }
        _cooldownCount++;
        _cooldownCount = Mathf.Min(_cooldownCount, _settings.RageThreshold);
        float xScale = (float)_cooldownCount / (float)_settings.RageThreshold;
        xScale = xScale * 3.6f + 1f;
        Vector3 newScale = new Vector3(xScale, 1f, 1f);
        _meter.localScale = newScale;
    }

    void OnCooldownFailed()
    {
        _cooldownCount = 0;
        OnRageStop();
    }
}
