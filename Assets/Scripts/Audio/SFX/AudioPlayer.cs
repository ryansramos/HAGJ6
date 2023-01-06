using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioCueEventChannelSO _SFXrequestChannel;

    [SerializeField]
    private PoolSO _emitterPool;
    private Dictionary<AudioKey, Emitter> _register = new Dictionary<AudioKey, Emitter>();

    void OnEnable()
    {
        _emitterPool.Initialize(this.gameObject);
        _SFXrequestChannel.OnEventRaised += OnSFXRequest;
        _SFXrequestChannel.OnStopRequested += OnStopRequest;
    }

    void OnDisable()
    {
        _SFXrequestChannel.OnEventRaised -= OnSFXRequest;
        _SFXrequestChannel.OnStopRequested -= OnStopRequest;
    }

    void OnSFXRequest(AudioCueSO cue, AudioKey key)
    {
        GameObject obj = _emitterPool.GetObject();
        if (!obj.TryGetComponent<Emitter>(out Emitter e))
        {
            return;
        }
        _register.Add(key, e);
        e.source.clip = cue.clip;
        e.source.panStereo = cue.pan;
        e.source.loop = cue.loop;
        e.source.Play();
        StartCoroutine(WaitToReturn(e, key));
    }

    void OnStopRequest(AudioKey key)
    {
        if (_register.ContainsKey(key))
        {
            _register[key].source.Stop();
            _register.Remove(key);
        }
    }

    IEnumerator WaitToReturn(Emitter e, AudioKey key)
    {
        while (e.source.isPlaying)
        {
            yield return null;
        }
        e.source.clip = null;
        if (_register.ContainsKey(key))
        {
            _register.Remove(key);
        }
        _emitterPool.ReturnObject(e.gameObject);
    }
}
