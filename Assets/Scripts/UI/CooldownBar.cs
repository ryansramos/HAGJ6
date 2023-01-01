using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownBar : MonoBehaviour
{
    public SpriteRenderer BarRenderer;
    public Sprite[] BarSprites;
    public GameObject Slider;
    
    private float _barHeight;
    private float _leftBarBound;
    private float _rightBarBound;

    [SerializeField]
    private bool _flipX;

    void OnEnable()
    {
        BarRenderer.sprite = BarSprites[0];
        Slider.SetActive(false);
        CalculateBarBounds();
    }

    public void StartCooldown()
    {
        Slider.SetActive(true);
        float xPosition = _leftBarBound;
        if (_flipX)
        {
            xPosition = _rightBarBound;
        }
        Slider.transform.position = new Vector3(xPosition, _barHeight, 0f);
        BarRenderer.sprite = BarSprites[1];
    }

    // Expects amount as percentage of cooldown completed
    public void UpdateSlider(float amount)
    {
        amount = Mathf.Clamp(amount, 0f, 1f);
        float xPosition;
        if (_flipX)
        {
            xPosition = amount * (_leftBarBound - _rightBarBound) + _rightBarBound;
        }
        else
        {
            xPosition = amount * (_rightBarBound - _leftBarBound) + _leftBarBound;
        }
        Slider.transform.position = new Vector3(xPosition, _barHeight, 0f);
    }

    public void CooldownFinished()
    {
        Slider.SetActive(false);
        BarRenderer.sprite = BarSprites[0];
    }

    void CalculateBarBounds()
    {
        Bounds bounds = BarRenderer.sprite.bounds;
        _leftBarBound = BarRenderer.transform.position.x - bounds.extents.x;
        _rightBarBound = BarRenderer.transform.position.x + bounds.extents.x;
        _barHeight = BarRenderer.transform.position.y;
    }
}
