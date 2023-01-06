using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Game")]
public class GameSettingsSO : ScriptableObject
{
    [Range(0f, 300f)]
    [SerializeField]
    public float GameDuration;

    [Range(1,63)]
    [SerializeField]
    public int TotalScore;

    [Range(1,20)]
    [SerializeField]
    public int RageThreshold;

    [Range(0f,10f)]
    public float RageDuration;
}
