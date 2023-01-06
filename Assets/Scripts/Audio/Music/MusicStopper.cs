using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStopper : MonoBehaviour
{
    [SerializeField]
    private AudioCueEventChannelSO _musicRequestChannel;

    public void Stop()
    {
        _musicRequestChannel.RaiseEvent(null);
    }
}
