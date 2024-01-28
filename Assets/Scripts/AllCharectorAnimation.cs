using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCharectorAnimation : MonoBehaviour
{
    public float rotationSpeed = 50f;

    void Update()
    {
        // Rotate the object in circles
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}