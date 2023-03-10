using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Scorekeeper : MonoBehaviour
{
    [SerializeField]
    private GameSettingsSO _settings;

    [SerializeField]
    private VoidEventChannelSO _onFrameDestroyedEvent;

    public FramesUI _UI;

    public UnityEvent onScoreCompleteEvent;
    public MeterMask _meterMask;
    private int _score;

    void OnEnable()
    {
        _onFrameDestroyedEvent.OnEventRaised += OnFrameDestroyed;
    }

    void OnDisable()
    {
        _onFrameDestroyedEvent.OnEventRaised -= OnFrameDestroyed;
    }

    public void StartGameplay()
    {
        _score = 0;
        _UI.UpdateUI(_score, _settings.TotalScore);
        _meterMask.ResetMeter();
    }

    void OnFrameDestroyed()
    {
        _score++;
        _UI.UpdateUI(_score, _settings.TotalScore);
        float percentComplete = (float)_score / (float)_settings.TotalScore;
        _meterMask.UpdateMeter(percentComplete);
        if (_score >= _settings.TotalScore)
        {
            OnScoreComplete();
        }
    }

    void OnScoreComplete()
    {
        onScoreCompleteEvent?.Invoke();
    }

    public int GetScore()
    {
        return _score;
    }
}
