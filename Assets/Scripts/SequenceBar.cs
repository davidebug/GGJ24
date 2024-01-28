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
            GameManager.Get().OnWrongSequence += UpdateSequence;
            GameManager.Get().OnCorrectSequence += UpdateSequence;
            GameManager.Get().OnGameStateChanged += InitSequence;
            enabledBar = true;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        if(!enabledBar)
        {
            GameManager.Get().OnWrongSequence += UpdateSequence;
            GameManager.Get().OnCorrectSequence += UpdateSequence;
            GameManager.Get().OnGameStateChanged += InitSequence;
        }

    }

    void InitSequence(GameState state) 
    { 
        if(state == GameState.SOLUTION)
        {
            UpdateSequence();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
    void UpdateSequence()
    {
        foreach (GameObject obj in currentTries)
        {
            Destroy(obj);
        }
            

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
            currentTries.Add(instantiatedTry);
            instantiatedTry.transform.localPosition = new Vector3(currentX, 0f, 0f);
            currentX += TrySpacing;
            
        }

    }
}
