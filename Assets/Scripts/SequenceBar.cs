using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SequenceBar : MonoBehaviour
{
    public GameObject SequenceGameObject;
    public GameObject CorrectTryAsset;
    public GameObject EmptyTryAsset;

    public List<GameObject> currentTries = new List<GameObject>();
    private int SequenceLength;
    private int CurrentCorrectNumber;

    public float TrySpacing = 100f; // Adjust this value as needed for spacing
    public bool enabledBar = false;

    void OnEnable()
    {
        if(GameManager.Get())
        {
            GameManager.Get().gameStateMachine.OnSequenceProgress += UpdateSequence;
            GameManager.Get().gameStateMachine.OnStageBegin += InitSequence;
            enabledBar = true;
        }

    }


    private void OnDisable()
    {
        if (GameManager.Get())
        {
            GameManager.Get().gameStateMachine.OnSequenceProgress -= UpdateSequence;
            GameManager.Get().gameStateMachine.OnStageBegin -= InitSequence;
            enabledBar = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(!enabledBar)
        {
            GameManager.Get().gameStateMachine.OnSequenceProgress += UpdateSequence;
    
            GameManager.Get().gameStateMachine.OnStageBegin += InitSequence;
        }

    }

    void InitSequence() 
    {
        CurrentCorrectNumber = 0;
        SequenceLength = GameManager.Get().gameStateMachine.CurrentSequenceLength;
        
    }
 
 
    public void UpdateSequence(bool hasPlayerMadeProgress)
    {
        foreach (GameObject obj in currentTries)
        {
            Destroy(obj);
        }

        if (hasPlayerMadeProgress)
        {
            CurrentCorrectNumber += 1;
        }
      
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
            currentTries.Add(instantiatedTry);
            instantiatedTry.transform.localPosition = new Vector3(currentX, 0f, 0f);
            currentX += TrySpacing;

        }
    }
}
