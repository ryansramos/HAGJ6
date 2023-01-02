using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/String")]
public class StringEventChannelSO : ScriptableObject
{
    public event UnityAction<string> OnEventRaised;

    public void RaiseEvent(string input)
    {
        OnEventRaised?.Invoke(input);
    }
}
