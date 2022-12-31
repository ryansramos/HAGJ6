using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Vector3")]
public class Vector3EventChannel : ScriptableObject
{
    public event UnityAction<Vector3> OnEventRaised;

    public void RaiseEvent(Vector3 input)
    {
        OnEventRaised?.Invoke(input);
    }
}
