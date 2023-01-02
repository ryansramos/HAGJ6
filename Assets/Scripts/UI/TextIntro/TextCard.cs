using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextCard : MonoBehaviour
{
    [SerializeField]
    private float _fadeDuration;
    public TextMeshProUGUI _textMesh;
    private IEnumerator _coroutine;
    private Queue<string> _textQueue = new Queue<string>();
    private bool _isPlaying;

    public void Initialize()
    {
        _isPlaying = false;
        _textQueue.Clear();
    }

    public void PlayNext(string text)
    {
        if (_isPlaying)
        {
            _textQueue.Enqueue(text);
            return;
        }
        _coroutine = SwapText(text);
        StartCoroutine(_coroutine);
    }

    void OnUpdate()
    {
        if (_textQueue.Count > 0)
        {
            PlayNext(_textQueue.Dequeue());
        }
    }

    IEnumerator SwapText(string text)
    {
        _isPlaying = true;
        TextFader fader = TextFader.FadeOut(_textMesh, _fadeDuration, this);
        while (!fader.IsComplete)
        {
            yield return null;
        }
        _textMesh.text = text;

        fader = TextFader.FadeIn(_textMesh, _fadeDuration, this);
        while (!fader.IsComplete)
        {
            yield return null;
        }
        _isPlaying = false;
    }
}
