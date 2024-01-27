using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/LevelDataSO", fileName = "LevelDataSO.asset")]
[System.Serializable]
public class LevelDataAssetScriptableObject : ScriptableObject
{
    public Texture[] textures;
    public int[] sequenceOrder;
    public int MaxTime;
    public Vector2[] CoordinatesXY;


}
