using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Audio Cue")]
public class AudioCueEventChannelSO : ScriptableObject
{
    public event UnityAction<AudioCueSO, AudioKey> OnEventRaised;
    public event UnityAction<AudioKey> OnStopRequested;

    public AudioKey RaiseEvent(AudioCueSO cue)
    {
        AudioKey key = new AudioKey();
        OnEventRaised?.Invoke(cue, key);
        return key;
    }

    public void Stop(AudioKey key)
    {
        OnStopRequested?.Invoke(key);
    }
}
