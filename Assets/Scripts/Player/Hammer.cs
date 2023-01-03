using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    [Header("Broadcasting on: ")]
    [SerializeField]
    private VoidEventChannelSO _perfectCooldownEvent;

    [SerializeField]
    private VoidEventChannelSO _cooldownFailedEvent;

    private Animator _animator;

    public HitSender sender;
    public CooldownBar cooldownBar;

    [SerializeField]
    private HammerSettingsSO _settings;

    [SerializeField]
    private VoidEventChannelSO 
        _onRageStartEvent,
        _onRageStopEvent;


    private bool _isOnCooldown = false;
    private bool _resetInput = false;
    private bool _isRaging = false;
    private IEnumerator _coroutine;

    void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    void OnEnable()
    {
        _onRageStartEvent.OnEventRaised += OnRageStart;
        _onRageStopEvent.OnEventRaised += OnRageStop;
    }

    void OnDisable()
    {
        _onRageStartEvent.OnEventRaised += OnRageStart;
        _onRageStopEvent.OnEventRaised -= OnRageStop;
    }

    void Start()
    {
        _isOnCooldown = false;
        _isRaging = false;
    }

    public void Swing()
    {
        if (!_isOnCooldown)
        {
            sender.SendHit();
            _animator.SetTrigger("OnSwing");
            _coroutine = CooldownStart();
            StartCoroutine(_coroutine);
        }
    }

    public void TryResetCooldown()
    {
        if (_isOnCooldown)
        {
            _resetInput = true;
        }
    }

    void OnRageStart()
    {
        StopAllCoroutines();
        OnCooldownFinished();
        _isRaging = true;
        cooldownBar.OnRageStart();
    }

    void OnRageStop()
    {
        StopAllCoroutines();
        OnCooldownFinished();
        _isRaging = false;
        cooldownBar.OnRageStop();
    }

    IEnumerator CooldownStart()
    {
        if (_settings.CooldownTime == 0f)
        {
            yield break;
        }
        _isOnCooldown = true;
        cooldownBar.StartCooldown();
        float timer = 0f;
        while (timer < _settings.CooldownTime * _settings.CooldownPerfectStart)
        {
            if (_resetInput)
            {
                StartCoroutine(CooldownPunish(timer));
                yield break;
            }
            UpdateCooldownSlider(timer);
            timer += Time.deltaTime;
            yield return null;
        }
        if (_isRaging)
        {
            OnCooldownFinished();
            yield break;
        }
        StartCoroutine(CooldownPerfect(timer));
    }

    IEnumerator CooldownPerfect(float startTime)
    {
        float timer = startTime;
        while (timer < _settings.CooldownTime * _settings.CooldownPerfectEnd)
        {
            if (_resetInput)
            {
                OnPerfectCooldown();
                yield break;
            }
            UpdateCooldownSlider(timer);
            timer += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(CooldownEnd(timer));
    }

    IEnumerator CooldownEnd(float startTime)
    {
        float timer = startTime;
        while (timer < _settings.CooldownTime)
        {
            if (_resetInput)
            {
                StartCoroutine(CooldownPunish(timer));
                yield break;
            }
            UpdateCooldownSlider(timer);
            timer += Time.deltaTime;
            yield return null;
        }
        cooldownBar.CooldownFinished();
        _isOnCooldown = false;
    }

    IEnumerator CooldownPunish(float startTime)
    {
        _cooldownFailedEvent.RaiseEvent();
        float timer = startTime;
        while (timer < _settings.CooldownTime * _settings.CooldownPunishMod)
        {
            float percentComplete = timer / (_settings.CooldownTime * _settings.CooldownPunishMod);
            cooldownBar.UpdateSlider(percentComplete);
            timer += Time.deltaTime;
            yield return null;
        }
        OnCooldownFinished();
    }

    void UpdateCooldownSlider(float currentTime)
    {
        float percentComplete = currentTime / _settings.CooldownTime;
        if (_isRaging)
        {
            percentComplete /= _settings.CooldownPerfectStart;
        }
        cooldownBar.UpdateSlider(percentComplete);
    }

    void OnPerfectCooldown()
    {
        _perfectCooldownEvent.RaiseEvent();
        OnCooldownFinished();
    }

    void OnCooldownFinished()
    {
        cooldownBar.CooldownFinished();
        _resetInput = false;
        _isOnCooldown = false;
    }
}
