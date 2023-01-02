using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    [SerializeField]
    private Image _blackScreen;

    [SerializeField]
    private float _fadeDuration;

    [NonSerialized]
    public bool IsComplete;

    public void FadeIn()
    {
        StartCoroutine(FadeScreen(0f));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeScreen(1f));
    }

    IEnumerator FadeScreen(float alphaTarget)
    {
        IsComplete = false;
        Vector3 c = new Vector3(_blackScreen.color.r, _blackScreen.color.g, _blackScreen.color.b);
        float startAlpha = _blackScreen.color.a;
        float deltaAlpha = alphaTarget - startAlpha;
        if (_fadeDuration <= 0f)
        {
            UpdateAlpha(c, alphaTarget);
            IsComplete = true;
            yield break;
        }

        float timer = 0f;
        while (timer < _fadeDuration)
        {
            float newAlpha = deltaAlpha * (timer / _fadeDuration) + startAlpha;
            UpdateAlpha(c,newAlpha);
            timer += Time.deltaTime;
            yield return null;
        }
        IsComplete = true;
    }

    void UpdateAlpha(Vector3 c, float alpha)
    {
        alpha = Mathf.Clamp(alpha, 0f, 1f);
        Color newColor = new Color(c.x, c.y, c.z, alpha);
        _blackScreen.color = newColor;
    }
}
