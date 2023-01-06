using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicRequester : MonoBehaviour
{
    [SerializeField]
    AudioCueEventChannelSO _musicRequestEvent;

    [SerializeField]
    AudioCueSO _musicCue;

    public void Play()
    {
        _musicRequestEvent.RaiseEvent(_musicCue);
    }
}
