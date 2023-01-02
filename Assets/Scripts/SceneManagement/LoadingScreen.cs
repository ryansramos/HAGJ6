using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    public TextMeshProUGUI _textMesh;
    private IEnumerator _coroutine;
    private bool _isPlaying;
    public void Play()
    {
        if (!_isPlaying)
        {
            _coroutine = PlayLoadingScreen(_textMesh);
            StartCoroutine(_coroutine);
            _isPlaying = true;
        }
    }

    IEnumerator PlayLoadingScreen(TextMeshProUGUI mesh)
    {
        string baseText = "Loading ";
        string addText = ". ";
        int i = 0;
        while (_isPlaying)
        {
            string displayText = baseText;
            for (int j = 0; j < i; j++)
            {
                displayText += addText;
            }
            mesh.text = displayText;
            yield return null;
            i++;
            i = i % 3;
        }
    }

    public void Stop()
    {
        _isPlaying = false;
    }
}
