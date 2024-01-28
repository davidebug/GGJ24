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
        GameManager.Get().OnGameStateChanged += SwitchSongBasedOnGameState;
    }

    private void SwitchSongBasedOnGameState(GameState state)
    {
        switch (state)
        {
            case GameState.SOLUTION:
                if (AudioAssetSO.mainMenuSongTheme)
                {
                    int nAudioClip = nChanges % 2;
                    audioSource.Stop();
                    audioSource.PlayOneShot(AudioAssetSO.GetGameplayAudioClip(nAudioClip));
                    nChanges++;
                }
                break;
            default: break;

        }
    }

    //IEnumerator  PlayGameplayAudioSwitchCoroutine()
    //{

    //    yield return;
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
