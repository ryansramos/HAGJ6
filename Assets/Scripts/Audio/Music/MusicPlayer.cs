using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    AudioCueEventChannelSO _playMusicEvent;

    [SerializeField]
    float _fadeDuration;

    public AudioSource _source;

    void OnEnable()
    {
        _playMusicEvent.OnEventRaised += PlayMusic;
    }

    void OnDisable()
    {
        _playMusicEvent.OnEventRaised -= PlayMusic;
    }

    void PlayMusic(AudioCueSO cue, AudioKey key)
    {
        if (cue == null)
        {
            StartCoroutine(FadeOut(_fadeDuration));
            return;
        }
        if (_source.clip == cue.clip && _source.isPlaying)
        {
            return;
        }
        StartCoroutine(ChangeMusic(cue.clip));
    }

    IEnumerator ChangeMusic(AudioClip clip)
    {
        if (!_source.isPlaying)
        {
            _source.volume = 1f;
            _source.clip = clip;
            _source.Play();
            yield break;
        }

        float timer = 0f;
        float startVolume = _source.volume;
        while (timer < _fadeDuration)
        {
            float newVolume = startVolume - (timer / _fadeDuration) * startVolume;
            Mathf.Clamp(newVolume, 0f, 1f);
            _source.volume = newVolume;
            timer += Time.deltaTime;
            yield return null;
        }
        _source.volume = 1f;
        _source.clip = clip;
        _source.Play();
    }

    IEnumerator FadeOut(float duration)
    {
        float timer = 0f;
        float startVolume = _source.volume;
        while (timer < _fadeDuration)
        {
            float newVolume = startVolume - (timer / _fadeDuration) * startVolume;
            Mathf.Clamp(newVolume, 0f, 1f);
            _source.volume = newVolume;
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
