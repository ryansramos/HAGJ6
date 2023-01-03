using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RageUI : MonoBehaviour
{
    public GlowText _rageText;
    public RageMeter _rageMeter;

    public void OnRageStart()
    {
        _rageText.StartGlow();
        _rageMeter.OnRageStart();
    }

    public void OnRageStop()
    {
        _rageText.StopGlow();
        _rageMeter.OnRageStop();
    }

    void SetUIElements(bool status)
    {
        _rageText.gameObject.SetActive(status);
    }
}
