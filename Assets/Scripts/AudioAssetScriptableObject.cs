using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/AudioDataSO", fileName = "AudioDataSO.asset")]
[System.Serializable]
public class AudioAssetScriptableObject : ScriptableObject
{
    public AudioClip gamePlayMusic_1;
    public AudioClip gamePlayMusic_2;

    public AudioClip mainMenuSongTheme;
    public AudioClip gameOverSong;

    public AudioClip SFX_partCorrect;
    public AudioClip SFX_partIncorrect;

    public AudioClip SFX_feather;


    public AudioClip GetGameplayAudioClip(int i)
    {
        if(i == 0)
        {
            return gamePlayMusic_1;
        }
        return gamePlayMusic_2;
    }

}
