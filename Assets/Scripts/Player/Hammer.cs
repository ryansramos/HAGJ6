using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public FadeOutText _swingInstruction;
    public FadeOutText _cooldownInstruction;

    [SerializeField]
    private VoidEventChannelSO 
        _onRageStartEvent,
        _onRageStopEvent;


    private bool _isOnCooldown = false;
    private bool _resetInput = false;
    private bool _isRaging = false;
    private IEnumerator _coroutine;

    public UnityEvent<Hammer, float> MoveToTargetEvent;
    public UnityEvent<Hammer, float> ReturnToPlayerEvent;

    public AudioSender _cooldownAudio;
    public AudioSender _cooldownPunishAudio;

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
        _onRageStartEvent.OnEventRaised -= OnRageStart;
        _onRageStopEvent.OnEventRaised -= OnRageStop;
    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void OnGameStart()
    {
        OnCooldownFinished();
        _isOnCooldown = false;
        _isRaging = false;
    }

    public void OnGameOver()
    {
        OnRageStop();
        StopAllCoroutines();
    }

    public void Swing()
    {
        if (!_isOnCooldown)
        {
            _swingInstruction.FadeOut();
            sender.SendHit();
            _animator.SetTrigger("OnSwing");
            _coroutine = CooldownStart();
            StartCoroutine(_coroutine);
            MoveToTargetEvent?.Invoke(this, _settings.CooldownTime * _settings.CooldownPerfectStart);
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
        this.StopAllCoroutines();
        OnCooldownFinished();
        _isRaging = true;
        cooldownBar.OnRageStart();
    }

    void OnRageStop()
    {
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
        ReturnToPlayerEvent?.Invoke(this, _settings.CooldownTime - startTime);
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
        _cooldownPunishAudio.Play();
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
        _cooldownAudio.Play();
        _cooldownInstruction.FadeOut();
        _animator.SetTrigger("OnPerfectCooldown");
        _perfectCooldownEvent.RaiseEvent();
        OnCooldownFinished();
    }

    void OnCooldownFinished()
    {
        if (_isRaging)
        {
            _animator.SetTrigger("OnPerfectCooldown");
        }
        ReturnToPlayerEvent?.Invoke(this, 0f);
        cooldownBar.CooldownFinished();
        _resetInput = false;
        _isOnCooldown = false;
    }
}
