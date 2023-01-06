using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeRemainingUI : MonoBehaviour
{
    public TextMeshProUGUI _textMesh;

    public void UpdateUI(float number)
    {
        _textMesh.text = number.ToString("F0");
    }
}
