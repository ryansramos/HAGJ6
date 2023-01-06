using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio Cue")]
public class AudioCueSO : ScriptableObject
{
    [SerializeField]
    public AudioClip clip;
}
