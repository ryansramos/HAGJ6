using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetector : MonoBehaviour
{
    [Header("Listening on")]
    [SerializeField]
    private Vector3EventChannel _hitSendEvent;

    [Header("Broadcasting on")]
    [SerializeField]
    private VoidEventChannelSO _targetDestroyedEvent;

    [SerializeField]
    private VoidEventChannelSO _perfectHitEvent;

    [SerializeField]
    private VoidEventChannelSO _rageStartEvent;

    [SerializeField]
    private VoidEventChannelSO _rageStopEvent;
    private bool _isRaging;

    [Range(0f,2f)]
    [SerializeField]
    private float _hitDistance;

    [Range(0f, 1f)]
    [SerializeField]
    private float _perfectDistance;

    [SerializeField]
    private float _hitDamage;

    [Range(1f,5f)]
    [SerializeField]
    private float _perfectHitMultiplier;

    [SerializeField]
    public StockingFrame _frame;

    [SerializeField]
    private HammerSettingsSO _settings;

    public FeedbackUI _feedback;

    private List<GameObject> _targetList = new List<GameObject>();

    void OnEnable()
    {
        _hitSendEvent.OnEventRaised += OnHitSend;
        _rageStartEvent.OnEventRaised += OnRageStart;
        _rageStopEvent.OnEventRaised += OnRageStop;
    }

    void OnDisable()
    {
        _hitSendEvent.OnEventRaised -= OnHitSend;
        _rageStartEvent.OnEventRaised -= OnRageStart;
        _rageStopEvent.OnEventRaised -= OnRageStop;
    }

    public void OnGameStart()
    {
        if (_targetList.Count > 0)
        {
            foreach (GameObject target in _targetList)
            {
                Destroy(target);
            }
        }
        _targetList.Clear();
    }

    public void OnGameOver()
    {

    }

    void OnRageStart()
    {
        _isRaging = true;
    }

    void OnRageStop()
    {
        _isRaging = false;
    }

    void OnHitSend(Vector3 position)
    {
        if (!(_targetList.Count > 0))
        {
            return;
        }
        Vector3 worldPosition = Camera.main.ViewportToWorldPoint(position);
        worldPosition = new Vector3(worldPosition.x, worldPosition.y, 0f);
        GameObject nearestTarget = GetNearestTarget(worldPosition, out float distance);
        if (Mathf.Abs(distance) <= _hitDistance)
        {
            if (Mathf.Abs(distance) < _hitDistance * _perfectDistance)
            {
                if (_isRaging)
                {
                    _feedback.OnRage(worldPosition);
                }
                else
                {
                    _feedback.OnPerfect(worldPosition);
                }
                _perfectHitEvent.RaiseEvent();
                StartCoroutine(AddDamage(2));
            }
            else
            {   
                if (_isRaging)
                {
                    _feedback.OnRage(worldPosition);
                }
                else
                {
                    _feedback.OnHit(worldPosition);
                }
                StartCoroutine(AddDamage(1));
            }
            _targetList.Remove(nearestTarget);
            _targetDestroyedEvent.RaiseEvent();
            Destroy(nearestTarget);
        }
    }

    IEnumerator AddDamage(int amount)
    {
        yield return new WaitForSeconds(_settings.HammerSwingLag);
        if (_isRaging)
        {
            _frame.AddDamage(4);
            yield break;
        }
        _frame.AddDamage(amount);
    }

    GameObject GetNearestTarget(Vector3 position, out float distance)
    {
        GameObject output = default;
        distance = Mathf.Infinity;
        foreach (GameObject target in _targetList)
        {
            float distanceToTarget = Mathf.Abs(Vector3.Distance(position, target.transform.position));
            if (output == null)
            {
                output = target;
                distance = distanceToTarget;
                continue;
            }

            if(distanceToTarget < distance)
            {
                output = target;
                distance = distanceToTarget;
            }
        }
        return output;
    }

    public float GetDistanceToNearestTarget(Vector3 position)
    {
        GameObject nearest = GetNearestTarget(position, out float distance);
        return distance;
    }

    public void AddTarget(GameObject target)
    {
        _targetList.Add(target);
    }
}
