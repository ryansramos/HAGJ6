using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio Cue")]
public class AudioCueSO : ScriptableObject
{
    [SerializeField]
    public AudioClip clip;

    [SerializeField]
    public bool loop;

    [Range(-1f, 1f)]
    [SerializeField]
    public float pan;
}
