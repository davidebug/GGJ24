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
    private const float GAMEPLAY_TARGET_VOLUME = 0.3f;
    private AudioClip exitingSong;
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
        switch (state)
        {
            case GameState.SOLUTION:

                StartCoroutine(PlayGameplayAudioSwitchCoroutine());

                break;

            default: break;

        }
    }



    IEnumerator PlayGameplayAudioSwitchCoroutine()
    {
        //Improve and create a general fade In and out
        float exitingSomeExecutionTime = audioSource.time;
        AudioClip exitingSong = audioSource.clip;
        AudioClip playingSong = exitingSong == AudioAssetSO.gamePlayMusic_1 ? AudioAssetSO.gamePlayMusic_2 : AudioAssetSO.gamePlayMusic_1;
        float startVolume = audioSource.volume;
        float elapsedTime = 0f;
        audioSource.volume = 0f;
        exitingSource.clip = exitingSong;
        exitingSource.time = exitingSomeExecutionTime;
        exitingSource.volume = startVolume;
        audioSource.clip = playingSong;
        audioSource.time = 0.0f;
        audioSource.Play();
        exitingSource.Play();
        while (elapsedTime < 5.0f)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0, GAMEPLAY_TARGET_VOLUME, elapsedTime / 5.0f);
            exitingSource.volume = Mathf.Lerp(GAMEPLAY_TARGET_VOLUME, 0, elapsedTime / 5.0f);
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
