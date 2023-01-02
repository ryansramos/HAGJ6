using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFader
{
    public bool IsComplete;
    private MonoBehaviour _caller;
    private TextMeshProUGUI _textToFade;

    public TextFader(TextMeshProUGUI text, MonoBehaviour caller)
    {
        _textToFade = text;
        _caller = caller;
        IsComplete = false;
    }

    public static TextFader FadeOut(TextMeshProUGUI text, float duration, MonoBehaviour caller)
    {
        TextFader fader = new TextFader(text, caller);
        fader.ToInvisible(duration);
        return fader;
    }

    public static TextFader FadeIn(TextMeshProUGUI text, float duration, MonoBehaviour caller)
    {
        TextFader fader = new TextFader(text, caller);
        fader.ToOpaque(duration);
        return fader;
    }

    void ToInvisible(float duration)
    {
        _caller.StartCoroutine(_ToInvisible(duration));
    }

    IEnumerator _ToInvisible(float duration)
    {
        float startAlpha = _textToFade.color.a;
        float deltaAlpha = 0f - startAlpha;
        Vector3 c = new Vector3(_textToFade.color.r, _textToFade.color.g, _textToFade.color.b);
        if (duration < 0)
        {
            UpdateAlpha(c, 0f);
            IsComplete = true;
            yield break;
        }
        float timer = 0f;

        while (timer < duration)
        {
            float newAlpha = deltaAlpha * (timer / duration);
            newAlpha += startAlpha;
            UpdateAlpha(c, newAlpha);
            timer += Time.deltaTime;
            yield return null;
        }

        UpdateAlpha(c, 0f);
        IsComplete = true;
    }

    void ToOpaque(float duration)
    {
        _caller.StartCoroutine(_ToOpaque(duration));
    }

    IEnumerator _ToOpaque(float duration)
    {
        float startAlpha = _textToFade.color.a;
        float deltaAlpha = startAlpha + 1;
        Vector3 c = new Vector3(_textToFade.color.r, _textToFade.color.g, _textToFade.color.b);
        if (duration < 0)
        {
            UpdateAlpha(c, 1f);
            IsComplete = true;
            yield break;
        }
        float timer = 0f;

        while (timer < duration)
        {
            float newAlpha = deltaAlpha * (timer / duration);
            newAlpha += startAlpha;
            UpdateAlpha(c, newAlpha);
            timer += Time.deltaTime;
            yield return null;
        }

        UpdateAlpha(c, 1f);
        IsComplete = true;
    }

    private void UpdateAlpha(Vector3 c, float alpha)
    {
        alpha = Mathf.Clamp(alpha, 0f, 1f);
        Color newColor = new Color(c.x, c.y, c.z, alpha);
        _textToFade.color = newColor;
    }
}
