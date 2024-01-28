using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem confettiParticleSystem = default;
    public ParticleSystem myParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //        if (Input.GetKeyDown("o") || Input.GetKeyDown("O"))
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Hello triggering particle sys: " + myParticleSystem.isPlaying);

            //confettiParticleSystem.Play();
            myParticleSystem.Play();

        }
    }
}
