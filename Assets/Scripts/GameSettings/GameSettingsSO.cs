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
}
