using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SpriteLight : MonoBehaviour
{
    private Light2D _light;
    [SerializeField]
    float _maxIntensity;

    [SerializeField]
    float _glowCycleTime;

    [SerializeField]
    private VoidEventChannelSO _onRageEvent;

    [SerializeField]
    private VoidEventChannelSO _onRageStoppedEvent;

    [SerializeField]
    private float _rageRate;

    [SerializeField]
    private Color _rageStartColor;

    [SerializeField]
    private Color _normalColor;

    private bool _isFading;
    private bool _isRaging;

    IEnumerator _rageCoroutine;

    void Awake()
    {
        _light = gameObject.GetComponent<Light2D>();
    }

    void OnEnable()
    {
        _isFading = true;
        _light.intensity = 1f;
        _light.color = _normalColor;
        StartCoroutine(FadeLight());
        
        _onRageEvent.OnEventRaised += OnRage;
        _onRageStoppedEvent.OnEventRaised += OnRageStopped;
    }

    void OnDisable()
    {
        _isFading = false;
        _light.intensity = 1f;
        StopAllCoroutines();

        _onRageEvent.OnEventRaised -= OnRage;
        _onRageStoppedEvent.OnEventRaised -= OnRageStopped;
    }

    void OnRage()
    {
        _light.color = _rageStartColor;
        _rageCoroutine = RageColor();
        _isRaging = true;
        StartCoroutine(_rageCoroutine);
    }

    void OnRageStopped()
    {
        if (_rageCoroutine != null)
        {
            StopCoroutine(_rageCoroutine);
        }
        _light.color = _normalColor;
        _isRaging = false;
    }

    IEnumerator FadeLight()
    {
        float timer = 0f;
        while (_isFading)
        {
            float duration = _glowCycleTime / 2f;
            timer = 0f;
            while (timer < duration)
            {
                float newIntensity = (_maxIntensity - 1f) * timer / duration + 1f;
                _light.intensity = newIntensity;
                timer += Time.deltaTime;
                yield return null;
            }

            timer = 0f;
            while (timer < duration)
            {
                float newIntensity = (1f - _maxIntensity) * timer / duration + _maxIntensity;
                _light.intensity = newIntensity;
                timer += Time.deltaTime;
                yield return null;
            }
        }
    }

    IEnumerator RageColor()
    {
        if (_rageRate == 0)
        {
            yield break;
        }
        while (_isRaging)
        {
            Color.RGBToHSV(_light.color, out float h, out float s, out float v);
            h += Time.deltaTime * _rageRate;
            // Getting the decimal portion of h
            h = h - (int)h;
            h = Mathf.Clamp(h, 0f, 1f);
            Color newColor = Color.HSVToRGB(h, s, v);
            _light.color = newColor;
            yield return null;
        }
    }
}
