using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockingFrame : MonoBehaviour
{
    [Range(0.01f, 3f)]
    [SerializeField]
    private float _spawnHealth;

    [SerializeField]
    private Sprite[] _frameSprites;
    private SpriteRenderer _renderer;

    [SerializeField]
    private float _destroyTime;
    private float _currentHealth;

    void Awake()
    {
        _renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        _currentHealth = _spawnHealth;
        _renderer.sprite = _frameSprites[0];
    }

    public void AddDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0f)
        {
            StartCoroutine(OnFrameDestroyed());
            return;
        }
        UpdateSprite();
    }

    IEnumerator OnFrameDestroyed()
    {
        _renderer.sprite = _frameSprites[3];
        yield return new WaitForSeconds(_destroyTime);
        _currentHealth = _spawnHealth;
        UpdateSprite();
    }

    void UpdateSprite()
    {
        float healthFraction = _currentHealth / _spawnHealth;
        Sprite newSprite = _frameSprites[0];
        if (healthFraction < .34f)
        {
            newSprite = _frameSprites[2];
        }
        else if (healthFraction < 0.67f)
        {
            newSprite = _frameSprites[1];
        }
        _renderer.sprite = newSprite;
    }
}
