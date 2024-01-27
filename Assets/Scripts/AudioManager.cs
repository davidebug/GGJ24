using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class AudioManager : Manager<AudioManager>
{
    public AudioAssetScriptableObject AudioAssetSO;
    public AudioSource audioSource;
    // Start is called before the first frame update

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
        switch(state)
        {
            case GameState.START_MENU:
                if (AudioAssetSO.mainMenuSongTheme)
                {
                    audioSource.PlayOneShot(AudioAssetSO.mainMenuSongTheme);
                }
                break;
            default: break; 

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
