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

    private bool IsSelected;
    private void Awake()
    {
        IsSelected = false;  
        bodyImage = GetComponent<Image>();
        Assert.IsNotNull(bodyImage);    
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
    {
        //SetBodyPartState()   
    }

    public void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
    {
        Debug.Log($"Pointer entered on Image {bodyNumberIndex}");

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log($"Pointer exited on Image {bodyNumberIndex}");
    }

    public void SetBodyPartState(bool select)
    {
        if (select && !IsSelected)
        {
            ShowClickedEffect();
        }
        
    }

    private void ShowClickedEffect()
    {
        bodyImage.color = Color.green;
    }

    
}
