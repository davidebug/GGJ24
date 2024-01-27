using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameBarDecrease : MonoBehaviour
{
    private float maxTime = 0f; // Maximum time in seconds
    public Transform greenBar; // Reference to the green bar transform
    private float decreaseDuration = 60f; // Duration over which the bar decreases

    private float startTime;

    void Start()
    {
        GameManager.Get().OnStageBegin += InitTime;
    }

    void InitTime()
    {
        maxTime = GameManager.Get().MaxTime;
        decreaseDuration = maxTime;
        startTime = Time.time;
    }

    void Update()
    {
        // Calculate the elapsed time since the start
        float elapsedTime = Time.time - startTime;

        // Calculate the progress (how much of the time has passed)
        float progress = elapsedTime / decreaseDuration;

        // Ensure progress stays within 0 to 1 range
        progress = Mathf.Clamp01(progress);

        // Calculate the scale factor based on progress
        float scaleFactor = 1f - progress;

        // Apply the scale to the green bar only
        greenBar.localScale = new Vector3(scaleFactor, 1f, 1f);

        // If the time has exceeded the maximum time, you can perform some action here
        if (elapsedTime >= maxTime)
        {
            // Do something when time is up
        }
    }
}
