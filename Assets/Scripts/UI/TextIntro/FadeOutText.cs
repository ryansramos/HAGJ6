using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeOutText : MonoBehaviour
{
    private TextMeshProUGUI _textMesh;
    private bool _hasFaded;

    void Awake()
    {
        _textMesh = gameObject.GetComponent<TextMeshProUGUI>();
    }
    public void FadeOut()
    {
        if (!_hasFaded)
        {
            StartCoroutine(_FadeOut());
            _hasFaded = true;
        }
    }

    IEnumerator _FadeOut()
    {
        TextFader fader = TextFader.FadeOut(_textMesh, 1f, this);
        while (!fader.IsComplete)
        {
            yield return null;
        }
    }
}
