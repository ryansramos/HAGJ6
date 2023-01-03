using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class BlinkingText : MonoBehaviour
{
    [SerializeField]
    private float _fadeRate;
    private TextMeshProUGUI _textMesh;

    void Awake()
    {
        _textMesh = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        StartCoroutine(BlinkFade(_fadeRate));
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator BlinkFade(float duration)
    {
        TextFader fader;
        while (true)
        {
            fader = TextFader.FadeOut(_textMesh, duration, this);
            while (!fader.IsComplete)
            {
                yield return null;
            }
            fader = TextFader.FadeIn(_textMesh, duration, this);
            while (!fader.IsComplete)
            {
                yield return null;
            }
        }
    }
}
