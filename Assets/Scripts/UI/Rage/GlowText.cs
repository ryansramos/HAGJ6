using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GlowText : MonoBehaviour
{
    private TextMeshProUGUI _textMesh;

    [SerializeField]
    private float _glowSpeed;

    private int _glowOffsetID;
    private IEnumerator _coroutine;

    void Awake()
    {
        _textMesh = gameObject.GetComponent<TextMeshProUGUI>();
        _glowOffsetID = ShaderUtilities.ID_GlowOffset;
    }
    public void StartGlow()
    {
        _textMesh.fontMaterial.SetFloat(_glowOffsetID, 0f);
        _coroutine = Glow(_glowSpeed);
        StartCoroutine(_coroutine);
    }

    IEnumerator Glow(float rate)
    {
        float timer = 0f;
        while (true)
        {
            float glowOffset = 0.5f * Mathf.Abs(Mathf.Sin(timer * rate));
            glowOffset = Mathf.Clamp(glowOffset, 0f, 0.5f);
            _textMesh.fontMaterial.SetFloat(_glowOffsetID, glowOffset);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public void StopGlow()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }
}
