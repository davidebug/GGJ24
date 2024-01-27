using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SequenceBar : MonoBehaviour
{
    public GameObject SequenceGameObject;
    public GameObject CorrectTryAsset;
    public GameObject EmptyTryAsset;

    public int SequenceLength;
    public int CurrentCorrectNumber;

    public float TrySpacing = 20f; // Adjust this value as needed for spacing

    // Start is called before the first frame update
    void Start()
    {
        //SequenceLength = GameManager.Get().TotalSequenceLength;
        //CurrentCorrectNumber = GameManager.Get().CurrentCorrectNumber;

        UpdateSequence();
    }

    // Update is called once per frame
    void Update()
    {
        // You can leave Update() empty if nothing needs to be updated continuously.
    }
    
    void UpdateSequence()
    {
        float currentX = 0f;
        // Instantiate and attach the correct or empty try assets with spacing
        for (int i = 0; i < SequenceLength; i++)
        {
            GameObject tryAsset;

            if (i < CurrentCorrectNumber)
            {
                tryAsset = CorrectTryAsset;
            }
            else
            {
                tryAsset = EmptyTryAsset;
            }

            GameObject instantiatedTry = Instantiate(tryAsset, SequenceGameObject.transform);
            instantiatedTry.transform.localPosition = new Vector3(currentX, 0f, 0f);
            currentX += TrySpacing;
        }
    }
}