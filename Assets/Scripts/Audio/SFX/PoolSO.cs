using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pool")]
public class PoolSO : ScriptableObject
{
    [SerializeField] 
    private GameObject _prefab;
    [SerializeField]
    private float _numberToPrewarm;
    private Queue<GameObject> _objectQueue = new Queue<GameObject>();
    private List<int> _leasedIDList = new List<int>();
    private GameObject _parent;

    public void Initialize(GameObject obj)
    {
        _parent = obj;
        for (int i = 0; i < _numberToPrewarm + 1; i++)
        {
            InstantiatePrefab();
        }
    }

    void InstantiatePrefab()
    {
        GameObject obj = Instantiate(_prefab, _parent.transform);
        _objectQueue.Enqueue(obj);
        obj.SetActive(false);
    }

    public GameObject GetObject()
    {
        if (_objectQueue.Count < 1)
        {
            InstantiatePrefab();
        }
        GameObject obj = _objectQueue.Dequeue();
        _leasedIDList.Add(obj.GetInstanceID());
        obj.SetActive(true);
        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        if (_leasedIDList.Contains(obj.GetInstanceID()))
        {
            obj.SetActive(false);
            _objectQueue.Enqueue(obj);
        }
    }
}
