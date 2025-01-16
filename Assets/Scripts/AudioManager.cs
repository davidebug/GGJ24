using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class AudioManager : Manager<AudioManager>
{
    public AudioAssetScriptableObject AudioAssetSO;
    public AudioSource audioSource;
    public AudioSource audioSource2;
    public AudioSource exitingSource;
    [SerializeField, Range(0, 10)]
    [Tooltip("Time lerping between two background music tracks")]
    private float timeBetweenSongsInGameplay = 5.0f;
    [SerializeField]
    private float timeGameOverSongEase = 2.0f;
    private const float GAMEPLAY_TARGET_VOLUME = 0.3f;
    private float songSwitchingTimeEase = 5.0f;

    // Start is called before the first frame update
    int nChanges = 0;
    public void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    void Start()
    {
        Assert.IsNotNull(AudioAssetSO);
        GameManager.Get().OnGameStateChanged += SwitchSongBasedOnGameState;
        audioSource.loop = true;
        audioSource.clip = AudioAssetSO.gamePlayMusic_2;
        audioSource.Play();
    }

 
    private void SwitchSongBasedOnGameState(GameState state)
    {
        if (state != GameState.GAME_OVER && state != GameState.SOLUTION)
        {
            return;
        }

        AudioClip exitingSong = audioSource.clip;
        float exitingSomeExecutionTime = audioSource.time;
        float playingSongVolume = audioSource.volume;

        switch (state)
        {
            case GameState.SOLUTION:
                audioSource.clip = audioSource.clip == AudioAssetSO.gamePlayMusic_1 ? AudioAssetSO.gamePlayMusic_2 : AudioAssetSO.gamePlayMusic_1;
                songSwitchingTimeEase = timeBetweenSongsInGameplay;
                break;
            case GameState.GAME_OVER:
                audioSource.clip = AudioAssetSO.gameOverSong;
                songSwitchingTimeEase = timeGameOverSongEase;
                break;
        }

        exitingSource.clip = exitingSong;
        exitingSource.time = exitingSomeExecutionTime;
        exitingSource.volume = playingSongVolume;
        audioSource.time = 0.0f;
        audioSource.Play();
        exitingSource.Play();
        StartCoroutine(PlayGameplayAudioSwitchCoroutine());
    }



        // Increase slowly audiosource volume while decreasing exitingSourceVolume
    IEnumerator PlayGameplayAudioSwitchCoroutine()
    {
      
        float elapsedTime = 0f;
        audioSource.volume = 0f;       
        while (elapsedTime < 5.0f)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0, GAMEPLAY_TARGET_VOLUME, elapsedTime / songSwitchingTimeEase);
            exitingSource.volume = Mathf.Lerp(GAMEPLAY_TARGET_VOLUME, 0, elapsedTime / songSwitchingTimeEase);
            yield return null;
        }
    }

    public void PlayingWithSecondAudioSource(AudioClip clip)
    {
        if (audioSource2.isPlaying)
        {
            audioSource2.Stop();
        }

        audioSource.PlayOneShot(clip);
    }

    public void PlayLaugh(int levelIndex)
    {
        PlayingWithSecondAudioSource(AudioAssetSO.GetRandomAudioLaugh(levelIndex));
    }

    public void PlayWoorp()
    {
        PlayingWithSecondAudioSource(AudioAssetSO.GetRandomWoorp());
    }

    public void PlayWhoosh()
    {
        PlayingWithSecondAudioSource(AudioAssetSO.GetRandomWhoosh());
    }
}
