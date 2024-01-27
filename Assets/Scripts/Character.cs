using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public BodyPart[] bodyParts;
    public int[] sequenceOrder;
    public int MaxTime;

    public void Awake()
    {
        if (bodyParts == null)
        {
            bodyParts = GetComponentsInChildren<BodyPart>();
        }
    }
    void Start()
    {

    }


    void Update()
    {

    }

}
