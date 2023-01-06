using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FramesUI : MonoBehaviour
{
    public TextMeshProUGUI _framesSmashedText;

    public void UpdateUI(int score, int total)
    {
        _framesSmashedText.text = score.ToString() + " / " + total.ToString();
    }
}
