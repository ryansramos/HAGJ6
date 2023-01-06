using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameTimer : MonoBehaviour
{
    [SerializeField]
    private GameSettingsSO _settings;

    [SerializeField]
    private MeterMask _meterMask;
    
    public TimeRemainingUI _timeUI;
    public UnityEvent gameTimeElapsedEvent;
    private IEnumerator _coroutine;

    public void StartTimer()
    {
        _meterMask.ResetMeter();
        _timeUI.UpdateUI(_settings.GameDuration);
        _coroutine = Timer(_settings.GameDuration);
        StartCoroutine(_coroutine);
    }

    IEnumerator Timer(float duration)
    {
        if (_settings.GameDuration <= 0)
        {
            Debug.LogWarning("Game duration set to 0");
            OnTimerComplete();
            yield break;
        }

        float timer = 0f;
        while (timer < duration)
        {
            float percentageComplete = timer / duration;
            _meterMask.UpdateMeter(percentageComplete);
            _timeUI.UpdateUI(duration - percentageComplete * duration);
            timer += Time.deltaTime;
            yield return null;
        }
        OnTimerComplete();
    }

    void OnTimerComplete()
    {
        gameTimeElapsedEvent?.Invoke();
        _timeUI.UpdateUI(0f);
    }
}
