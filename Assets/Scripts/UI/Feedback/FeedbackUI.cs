using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackUI : MonoBehaviour
{
    [SerializeField]
    private GameObject 
        _perfectTextPrefab,
        _hitTextPrefab;

    public RectTransform _canvas;

    public void OnHit(Vector3 position)
    {
        GameObject textObj = Instantiate(_hitTextPrefab, _canvas.transform);
        textObj.transform.position = position;
    }

    public void OnPerfect(Vector3 position)
    {
        GameObject textObj = Instantiate(_perfectTextPrefab, _canvas.transform);
        textObj.transform.position = position;
    }
}
