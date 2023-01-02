using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameManager : MonoBehaviour
{
    public StockingFrame frame;
    public HitDetector detector;
    public TargetGenerator generator;

    public void OnGameStart()
    {
        frame.OnGameStart();
        detector.OnGameStart();
        generator.OnGameStart();
    }

    public void OnGameOver()
    {
        frame.OnGameOver();
        detector.OnGameOver();
        generator.OnGameOver();
    }
}
