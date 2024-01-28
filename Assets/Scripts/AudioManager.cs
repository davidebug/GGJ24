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
        //GameManager.Get().OnGameStateChanged += SwitchSongBasedOnGameState;
        audioSource.PlayOneShot(AudioAssetSO.gamePlayMusic_2);
    }

    //private void SwitchSongBasedOnGameState(GameState state)
    //{
    //    switch (state)
    //    {
    //        case GameState.SOLUTION:
    //            if (AudioAssetSO.mainMenuSongTheme)
    //            {
    //                //int nAudioClip = nChanges % 2;
    //                // original idea was to fade from one song to another
    //                audioSource.Stop();
                   
    //                //nChanges++;
    //            }
    //            break;
    //        default: break;

    //    }
    //}

    //IEnumerator  PlayGameplayAudioSwitchCoroutine()
    //{

    //    yield return;
    //}

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
}
