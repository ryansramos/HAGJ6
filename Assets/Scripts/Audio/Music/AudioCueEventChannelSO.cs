using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Audio Cue")]
public class AudioCueEventChannelSO : ScriptableObject
{
    public event UnityAction<AudioCueSO> OnEventRaised;

    public void RaiseEvent(AudioCueSO cue)
    {
        OnEventRaised?.Invoke(cue);
    }
}
