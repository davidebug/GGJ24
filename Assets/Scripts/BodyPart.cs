using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BodyPart : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image bodyImage;
    // image identification number
    public int bodyNumberIndex;
    private Color original;
    private bool IsSelected;
    private bool IsShowingHoveringEffect;
    private GameManager gameManager;
    private AudioSource audioSource;
    private void Awake()
    {
        IsSelected = false;
        bodyImage = GetComponent<Image>();
        Assert.IsNotNull(bodyImage);
        original = bodyImage.color;
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Start()
    {

        if (gameManager == null)
        {
            gameManager = GameManager.Get();
        }

        gameManager.gameStateMachine.OnSequenceProgress += CheckAndUnselect;
       
    }
    private void OnEnable()
    {
        gameManager = GameManager.Get();

    }

    private void OnDestroy()
    {
        if (gameManager != null)
        {
            gameManager.gameStateMachine.OnSequenceProgress -= CheckAndUnselect;
        }
    }
    public void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
    {
        SelectBodyPart();
    }

    public void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
    {
        Debug.Log($"Pointer entered on Image {bodyNumberIndex}");
        ShowHoverEffect(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log($"Pointer exited on Image {bodyNumberIndex}");
        ShowHoverEffect(false);
    }

    private void SelectBodyPart()
    {
        if (!IsSelected)
        {
            if (gameManager.TryToSelectBodyPart(bodyNumberIndex))
            {
                ShowClickedEffect(true);
                IsSelected = true;
                PlayAudio(AudioManager.Get().AudioAssetSO.SFX_partCorrect);
            }

        }
        else
        {
            gameManager.UnselectEverything();
            IsSelected = false;
            ShowClickedEffect(false);
            PlayAudio(AudioManager.Get().AudioAssetSO.SFX_partIncorrect);
        }

    }

    private void ShowClickedEffect(bool select)
    {
        if (select)
        {
            bodyImage.color = Color.green;

        }
        else
        {
            bodyImage.color = Color.red;
        }
    }

    private void CheckAndUnselect(bool isPlayerMakingProgress)
    {
        if (isPlayerMakingProgress)
            return;

        IsSelected = false;
        bodyImage.color = original;
    }

    private void ShowHoverEffect(bool isPointerHovering)
    {
        if (isPointerHovering == IsShowingHoveringEffect || IsSelected)
            return;

        if (isPointerHovering)
        {
            Debug.Log("Set Hovering Effect");
            IsShowingHoveringEffect = true;
        }
        else
        {
            Debug.Log("Set Hovering Effect Off");
            IsShowingHoveringEffect = false;
        }
    }

    private void PlayAudio(AudioClip audioClip)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

         audioSource.PlayOneShot(audioClip);
    }

}

