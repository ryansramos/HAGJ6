using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    private Animator _animator;

    public HitSender _sender;

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
            _sender.SendHit();
            _animator.SetTrigger("OnSwing");
            _coroutine = Cooldown(_cooldown);
            StartCoroutine(_coroutine);
        }
    }

    IEnumerator Cooldown(float duration)
    {
        _isOnCooldown = true;
        yield return new WaitForSeconds(duration);
        _isOnCooldown = false;
    }
}
