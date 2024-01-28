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
    private void Awake()
    {
        IsSelected = false;  
        bodyImage = GetComponent<Image>();
        Assert.IsNotNull(bodyImage);    
        original = bodyImage.color;
    }

    private void Start()
    {
        
        if (gameManager == null)
        {
            gameManager = GameManager.Get();
        }

        gameManager.OnWrongSequence += Unselect;
    }
    private void OnEnable()
    {
        gameManager = GameManager.Get();

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
                IsSelected=true;
            }

        }
        else
        {
            gameManager.UnselectEverything();
            IsSelected = false;
            ShowClickedEffect(false);
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

    private void Unselect()
    {
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
}
