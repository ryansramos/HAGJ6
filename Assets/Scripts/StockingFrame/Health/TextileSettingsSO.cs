using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Textile")]
public class TextileSettingsSO : ScriptableObject
{
    [SerializeField]
    public float DropSpeed;

    [SerializeField]
    public float DropLag;

    [SerializeField]
    public float FadeDuration;
}
