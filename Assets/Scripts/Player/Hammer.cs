using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    private Animator _animator;

    public HitSender sender;
    public CooldownBar cooldownBar;

    [SerializeField]
    private HammerSettingsSO _settings;

    private bool _isOnCooldown = false;
    private bool _resetInput = false;
    private IEnumerator _coroutine;

    void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    void Start()
    {
        _isOnCooldown = false;
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
        StartCoroutine(CooldownPerfect(timer));
    }

    IEnumerator CooldownPerfect(float startTime)
    {
        float timer = startTime;
        while (timer < _settings.CooldownTime * _settings.CooldownPerfectEnd)
        {
            if (_resetInput)
            {
                OnCooldownFinished();
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
        cooldownBar.UpdateSlider(percentComplete);
    }

    void OnCooldownFinished()
    {
        cooldownBar.CooldownFinished();
        _resetInput = false;
        _isOnCooldown = false;
    }
}
