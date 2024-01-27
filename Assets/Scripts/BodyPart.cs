using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BodyPart : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Texture bodyImage;
    // image identification number
    public int bodyNumberIndex;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
    {
        throw new System.NotImplementedException();

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void SetBodyPartState(bool clicked)
    {

    }
}
