using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIndicator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _indicatorPrefabs;

    [SerializeField]
    private TextileSettingsSO _settings;
    private Textile[] _indicators = new Textile[4];

    private Queue<Textile> _dropQueue = new Queue<Textile>();
    private bool _isDropping;
    
    public void OnGameStart()
    {
        if (_indicators != null)
        {
            foreach (Textile textile in _indicators)
            {
                if (textile != null)
                {
                    Destroy(textile.gameObject);
                }
            }
        }
        NewFrame();
    }

    public void UpdateHealth(int health)
    {
        int numToDrop = 4 - health;
        for (int i = 0; i < numToDrop; i++)
        {
            Textile t = _indicators[i];
            if (t != null)
            {
                if (!t.isDropped)
                {
                    _dropQueue.Enqueue(t);
                }
            }
        }
    }

    IEnumerator DropTextile(Textile textile)
    {
        yield return new WaitForSeconds(_settings.DropLag);
        textile.Drop();
        _isDropping = false;
    }

    public void NewFrame()
    {
        int length = _indicatorPrefabs.Length;
        _indicators = new Textile[length];
        for (int i = 0; i < length; i++)
        {
            GameObject textileObj = Instantiate(_indicatorPrefabs[i], this.gameObject.transform);
            _indicators[i] = textileObj.GetComponent<Textile>();
        }
    }

    void Update()
    {
        if (_dropQueue.Count > 0 && !_isDropping)
        {
            _isDropping = true;
            StartCoroutine(DropTextile(_dropQueue.Dequeue()));
        }
    }
}