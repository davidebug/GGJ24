using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameBarDecrease : MonoBehaviour
{
    private float maxTime = 0f; // Maximum time in seconds
    public Transform greenBar; // Reference to the green bar transform
    private float decreaseDuration = 60f; // Duration over which the bar decreases

    private float startTime;
    private GameState currentState;
    private AudioSource audioSource;

    void Start()
    {
        GameManager.Get().OnGameStateChanged += InitTime;
        audioSource = gameObject.AddComponent<AudioSource>();   
    }

    void InitTime(GameState currentState)
    {
        this.currentState = currentState;
        if (currentState == GameState.PLAYING)
        {
            maxTime = GameManager.Get().MaxTime;
            decreaseDuration = maxTime;
            startTime = Time.time;
        }
    }

    void Update()
    {
        if(currentState == GameState.PLAYING)
        {
            // Calculate the elapsed time since the start
            float elapsedTime = GameManager.Get().CurrentTime;

            // Calculate the progress (how much of the time has passed)
            float progress = elapsedTime / decreaseDuration;

            // Ensure progress stays within 0 to 1 range
            progress = Mathf.Clamp01(progress);

            // Calculate the scale factor based on progress
            float scaleFactor = 1f - progress;

            // Apply the scale to the green bar only
            greenBar.localScale = new Vector3(scaleFactor, 1f, 1f);

            if(scaleFactor < 0.3f && !audioSource.isPlaying)
            {
                audioSource.PlayOneShot(AudioManager.Get().AudioAssetSO.SFX_TimeRunning);
                audioSource.volume = 0.6f;
            }

            // If the time has exceeded the maximum time, you can perform some action here
            if (elapsedTime >= maxTime)
            {
                // Do something when time is up
                audioSource.Stop();
            }
        }
        
    }
}
