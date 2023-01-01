using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Hammer")]
public class HammerSettingsSO : ScriptableObject
{
    [SerializeField]
    public float CooldownTime;

    [Range(0f, 1f)]
    public float CooldownPerfectStart;

    public float CooldownPerfectEnd => CooldownPerfectStart + .125f;

    [Range(1f, 2f)]
    public float CooldownPunishMod;
}
