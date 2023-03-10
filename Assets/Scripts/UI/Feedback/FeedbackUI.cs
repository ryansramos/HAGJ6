using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackUI : MonoBehaviour
{
    [SerializeField]
    private GameObject 
        _perfectTextPrefab,
        _hitTextPrefab,
        _rageTextPrefab;

    [SerializeField]
    public AudioSender
        onHitAudio,
        onPerfectAudio,
        onRageAudio;

    public RectTransform _canvas;

    public void OnHit(Vector3 position)
    {
        GameObject textObj = Instantiate(_hitTextPrefab, _canvas.transform);
        onHitAudio.Play();
        textObj.transform.position = position;
    }

    public void OnPerfect(Vector3 position)
    {
        GameObject textObj = Instantiate(_perfectTextPrefab, _canvas.transform);
        onPerfectAudio.Play();
        textObj.transform.position = position;
    }

    public void OnRage(Vector3 position)
    {
        GameObject textObj = Instantiate(_rageTextPrefab, _canvas.transform);
        onRageAudio.Play();
        textObj.transform.position = position;
    }


}
