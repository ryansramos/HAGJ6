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
        StartCoroutine(DepleteMeter(_settings.RageDuration));
    }

    IEnumerator DepleteMeter(float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            float percentage = 1 - timer / duration;
            UpdateMeter(percentage);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public void OnRageStop()
    {
        _cooldownCount = 0;
        StopAllCoroutines();
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
        float percentage = (float)_cooldownCount / (float)_settings.RageThreshold;
        UpdateMeter(percentage);
    }

    void UpdateMeter(float percentage)
    {
        float xScale = percentage * 3.6f + 1f;
        Vector3 newScale = new Vector3(xScale, 1f, 1f);
        _meter.localScale = newScale;
    }

    void OnCooldownFailed()
    {
        _cooldownCount = 0;
        OnRageStop();
    }
}
