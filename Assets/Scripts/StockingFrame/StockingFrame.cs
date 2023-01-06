using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockingFrame : MonoBehaviour
{
    [Header("Broadcasting on")]
    [SerializeField]
    private VoidEventChannelSO _onFrameDestroyedEvent;

    [Range(0, 10)]
    [SerializeField]
    private int _spawnHealth;

    [SerializeField]
    private Sprite[] _frameSprites;
    private SpriteRenderer _renderer;

    [SerializeField]
    private float _destroyTime;
    private int _currentHealth;

    public HealthIndicator _indicator;
    public AudioSender _frameBreakAudio;

    void Awake()
    {
        _renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void OnGameStart()
    {
        _currentHealth = _spawnHealth;
        _renderer.sprite = _frameSprites[0];
        _indicator.OnGameStart();
    }

    public void OnGameOver()
    {
        StopAllCoroutines();
    }

    public void AddDamage(int damage)
    {
        _currentHealth -= damage;
        _indicator.UpdateHealth(Mathf.Clamp(_currentHealth, 0, _spawnHealth));
        if (_currentHealth <= 0f)
        {
            StartCoroutine(OnFrameDestroyed());
            return;
        }
        UpdateSprite();
    }

    IEnumerator OnFrameDestroyed()
    {
        _frameBreakAudio.Play();
        _renderer.sprite = _frameSprites[3];
        _onFrameDestroyedEvent.RaiseEvent();
        yield return new WaitForSeconds(_destroyTime);
        _indicator.NewFrame();
        _currentHealth = _spawnHealth;
        UpdateSprite();
    }

    void UpdateSprite()
    {
        float healthFraction = (float)_currentHealth / (float)_spawnHealth;
        Sprite newSprite = _frameSprites[0];
        if (healthFraction < .5f)
        {
            newSprite = _frameSprites[2];
        }
        else if (healthFraction < 0.8f)
        {
            newSprite = _frameSprites[1];
        }
        _renderer.sprite = newSprite;
    }
}
