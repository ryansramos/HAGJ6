using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSender : MonoBehaviour
{
    [SerializeField]
    private AudioCueEventChannelSO _sfxChannel;

    [SerializeField]
    private AudioCueSO _audioCue;

    private Queue<AudioKey> _keyQueue = new Queue<AudioKey>();

    public void Play()
    {
        _keyQueue.Enqueue(_sfxChannel.RaiseEvent(_audioCue));
    }

    public void Stop()
    {
        if (_keyQueue.Count < 1)
        {
            return;
        }
        _sfxChannel.Stop(_keyQueue.Dequeue());
    }

    public void StopAll()
    {
        foreach (AudioKey key in _keyQueue)
        {
            _sfxChannel.Stop(key);
        }
    }
}
