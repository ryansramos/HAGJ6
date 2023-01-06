using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Emitter : MonoBehaviour
{
    private AudioSource _source;
    public AudioSource source => _source;

    void Awake()
    {
        _source = gameObject.GetComponent<AudioSource>();
    }
}
