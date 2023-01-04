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

    [SerializeField]
    private float _targetDistanceBuffer;

    [SerializeField]
    private float _targetSpawnLag;

    private float _leftBound;
    private float _rightBound;
    private float _upperBound;
    private float _lowerBound;

    private bool _isSpawning = false;
    private bool _isSpawnActive;

    [SerializeField]
    private HammerSettingsSO _settings;

    void Awake()
    {
        _renderer = gameObject.GetComponent<SpriteRenderer>();
    }
    void OnEnable()
    {
        _targetDestroyedEvent.OnEventRaised += OnTargetDestroyed;
    }

    void Start()
    {
        CalculateBounds();
    }

    void OnDisable()
    {
        _targetDestroyedEvent.OnEventRaised -= OnTargetDestroyed;
    }

    public void OnGameStart()
    {
        _currentTargets = 0;
        _isSpawning = false;
        _isSpawnActive = true;
    }

    public void OnGameOver()
    {
        _isSpawnActive = false;
    }

    void Update()
    {   
        if (_isSpawnActive && _currentTargets < _maxTargets && !_isSpawning)
        {
            _isSpawning = true;
            StartCoroutine(SpawnTarget());
        }
    }

    IEnumerator SpawnTarget()
    {
        yield return new WaitForSeconds(_targetSpawnLag);
        bool isSpawnValid = false;
        Vector3 position = new Vector3();
        while (!isSpawnValid)
        {
            position = GetPosition();
            isSpawnValid = _detector.GetDistanceToNearestTarget(position) > _targetDistanceBuffer;
            yield return null;
        }

        GameObject target = Instantiate(_targetPrefab, position, Quaternion.identity);
        target.transform.parent = this.gameObject.transform;
        _detector.AddTarget(target);
        _currentTargets++;
        _isSpawning = false;
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
        float xPosition = gameObject.transform.position.x;
        float yPosition = gameObject.transform.position.y;
        _leftBound = xPosition - bounds.extents.x;
        _rightBound = xPosition + bounds.extents.x;
        _upperBound = yPosition + bounds.extents.y;
        _lowerBound = yPosition - bounds.extents.y;
    }

    void OnTargetDestroyed()
    {
        _currentTargets--;
        _currentTargets = Mathf.Max(_currentTargets, 0);
    }

    IEnumerator UpdateTargets()
    {
        yield return null;
    }
}
