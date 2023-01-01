using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    private Animator _animator;

    public HitSender sender;
    public CooldownBar cooldownBar;

    [SerializeField]
    private float _cooldown;
    private bool _isOnCooldown = false;
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
            _coroutine = Cooldown(_cooldown);
            StartCoroutine(_coroutine);
        }
    }

    IEnumerator Cooldown(float duration)
    {
        if (duration == 0f)
        {
            yield break;
        }
        _isOnCooldown = true;
        cooldownBar.StartCooldown();
        float timer = 0f;
        while (timer < duration)
        {
            float percentComplete = timer / duration;
            cooldownBar.UpdateSlider(percentComplete);
            timer += Time.deltaTime;
            yield return null;
        }
        cooldownBar.CooldownFinished();
        _isOnCooldown = false;
    }
}
