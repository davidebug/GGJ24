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
        int maxCharacters = characters.Length;
        index = Mathf.Clamp(index, 0, maxCharacters);

        return index < 5 ?  characters[index] : null;   
    }

}
