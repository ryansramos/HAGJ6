using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGenerator : MonoBehaviour
{
    [Header("Listening on")]
    [SerializeField]
    private VoidEventChannelSO _targetDestroyedEvent;

    [SerializeField]
    private GameObject _targetPrefab;

    [SerializeField]
    public HitDetector _detector;

    [SerializeField]
    private float _boundsMargin;

    [SerializeField]
    private int _maxTargets;
    private int _currentTargets = 0;

    private SpriteRenderer _renderer;

    private float _leftBound;
    private float _rightBound;
    private float _upperBound;
    private float _lowerBound;

    void Awake()
    {
        _renderer = gameObject.GetComponent<SpriteRenderer>();
    }
    void OnEnable()
    {
        CalculateBounds();
        _currentTargets = 0;
    }

    void Start()
    {
        _targetDestroyedEvent.OnEventRaised += OnTargetDestroyed;
    }
    void OnDisable()
    {
        _targetDestroyedEvent.OnEventRaised -= OnTargetDestroyed;
    }

    void Update()
    {   
        if (_currentTargets < _maxTargets)
        {
            SpawnTarget();
        }
    }

    void SpawnTarget()
    {
        Vector3 position = GetPosition();
        GameObject target = Instantiate(_targetPrefab, position, Quaternion.identity);
        target.transform.parent = this.gameObject.transform;
        _detector.AddTarget(target);
        _currentTargets++;
    }

    Vector3 GetPosition()
    {
        float xPos = Random.Range(_leftBound + _boundsMargin, _rightBound - _boundsMargin);
        float yPos = Random.Range(_lowerBound + _boundsMargin, _upperBound - _boundsMargin);
        Vector3 position = new Vector3(xPos, yPos, 0f);
        return position;
    }

    void CalculateBounds()
    {
        Bounds bounds = _renderer.sprite.bounds;
        _leftBound = bounds.center.x - bounds.extents.x;
        _rightBound = bounds.center.x + bounds.extents.x;
        _upperBound = bounds.center.y + bounds.extents.y;
        _lowerBound = bounds.center.y - bounds.extents.y;
    }

    void OnTargetDestroyed()
    {
        _currentTargets--;
        _currentTargets = Mathf.Max(_currentTargets, 0);
    }
}
