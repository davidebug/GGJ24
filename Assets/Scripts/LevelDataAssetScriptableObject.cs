using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/LevelDataSO", fileName = "LevelDataSO.asset")]
[System.Serializable]
public class LevelDataAssetScriptableObject : ScriptableObject
{
    public Character[] characters;
    
  


    public Character GetCharacter(int index)
    {
        return characters[index];   
    }

}
