using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterMask : MonoBehaviour
{
    [SerializeField]
    private float 
        _startXPosition,
        _endXPosition;

    [ContextMenu("Set Position/Start")]
    public void SetStartPosition()
    {
        _startXPosition = gameObject.transform.position.x;
    }

    [ContextMenu("Set Position/End")]
    public void SetEndPosition()
    {
        _endXPosition = gameObject.transform.position.x;
    }

    public void ResetMeter()
    {
        gameObject.transform.position = new Vector3(_startXPosition, gameObject.transform.position.y, 0f);
    }

    // Expects amount as a percentage of completion.
    public void UpdateMeter(float amount)
    {
        amount = Mathf.Clamp(amount, 0f, 1f);
        float newXPosition = amount * (_endXPosition - _startXPosition) + _startXPosition;
        gameObject.transform.position = new Vector3(newXPosition, gameObject.transform.position.y, 0f);
    }
}
