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

    [Range(0f,2f)]
    [SerializeField]
    private float _hitDistance;

    [SerializeField]
    private float _hitDamage;

    [SerializeField]
    public StockingFrame _frame;

    private List<GameObject> _targetList = new List<GameObject>();

    void OnEnable()
    {
        _targetList.Clear();
    }

    void Start()
    {
        _hitSendEvent.OnEventRaised += OnHitSend;
    }

    void OnDisable()
    {
        _hitSendEvent.OnEventRaised -= OnHitSend;
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
            _frame.AddDamage(_hitDamage);
            _targetList.Remove(nearestTarget);
            _targetDestroyedEvent.RaiseEvent();
            Destroy(nearestTarget);
        }
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

    public void AddTarget(GameObject target)
    {
        _targetList.Add(target);
    }
}
