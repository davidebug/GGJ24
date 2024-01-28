using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuPanel : MonoBehaviour
{
    public Button StartButton;
    public Button QuitGame;
    public AudioSource audioSource;

    public AudioAssetScriptableObject AudioAssetSO;
  
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        StartButton.onClick.AddListener(LoadGame);
        QuitGame.onClick.AddListener(QuitApplication);   
    }
    
    void LoadGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    void QuitApplication()
    {
        SceneManager.LoadScene("GameScene");
    }

   
}
