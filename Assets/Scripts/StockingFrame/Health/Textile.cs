using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Textile : MonoBehaviour
{
    [SerializeField]
    private TextileSettingsSO _settings;

    private bool _isDropped;
    public bool isDropped => _isDropped;
    private Transform _transform;
    private SpriteRenderer _renderer;

    void Awake()
    {
        _transform = gameObject.GetComponent<Transform>();
        _renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        _isDropped = false;
    }

    public void Drop()
    {
        if (!_isDropped)
        {
            _isDropped = true;
            StartCoroutine(_Drop(_settings.DropSpeed, _settings.FadeDuration));
        }
    }

    IEnumerator _Drop(float speed, float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            MoveTransform(speed);
            UpdateAlpha(timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }
        UpdateAlpha(1f);
        Destroy(this.gameObject, 1f);
    }

    void MoveTransform(float speed)
    {
        float yDelta = speed * Time.deltaTime;
        _transform.Translate(Vector3.up * -speed);
    }

    void UpdateAlpha(float percentage)
    {
        percentage = Mathf.Clamp(1f - percentage, 0f, 1f);
        Color newColor = new Color(1f,1f,1f,percentage);
        _renderer.color = newColor;
    }
}
