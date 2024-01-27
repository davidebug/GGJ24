using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    void Start()
    {
        Debug.Log("In Pointer script setting Cursoer.visible = false,  gameObject.name = " + gameObject.name);

        Cursor.visible = false;
    }
    void Update()
    {
        
    }
}
