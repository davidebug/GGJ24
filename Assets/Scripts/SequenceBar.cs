using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SequenceBar : MonoBehaviour
{
    public GameObject SequenceGameObject;
    public GameObject CorrectTryAsset;
    public GameObject EmptyTryAsset;

    private int SequenceLength;
    private int CurrentCorrectNumber;

    public float TrySpacing = 20f; // Adjust this value as needed for spacing

    // Start is called before the first frame update
    void Start()
    {
        UpdateSequence();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSequence();
    }
    
    void UpdateSequence()
    {
        SequenceLength = GameManager.Get().TotalSequenceLength;
        CurrentCorrectNumber = GameManager.Get().CurrentCorrectNumber;

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
