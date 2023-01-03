using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GlowFade : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField]
    private float _fadeStart;

    [Range(0f, 1f)]
    [SerializeField]
    private float _fadeDuration;
    private TextMeshProUGUI _textMesh;
    private int _glowOffsetID;

    void Awake()
    {
        _textMesh = gameObject.GetComponent<TextMeshProUGUI>();
        _glowOffsetID = ShaderUtilities.ID_GlowOffset;
    }

    void OnEnable()
    {
        StartCoroutine(_GlowFade(_fadeStart, _fadeDuration));
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator _GlowFade(float start, float duration)
    {
        if (duration == 0f)
        {
            UpdateGlow(0f);
            yield break;
        }
        UpdateGlow(start);
        float timer = 0f;
        while (timer < duration)
        {
            float newValue = start - (timer / duration) * start;
            UpdateGlow(newValue);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    void UpdateGlow(float value)
    {
        _textMesh.fontMaterial.SetFloat(_glowOffsetID, value);
    }
}
