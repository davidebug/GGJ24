using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/AudioDataSO", fileName = "AudioDataSO.asset")]
[System.Serializable]
public class AudioAssetScriptableObject : ScriptableObject
{
    public AudioClip mainSongTheme;
    public AudioClip mainMenuSongTheme;
    public AudioClip gameOverSong;

}
