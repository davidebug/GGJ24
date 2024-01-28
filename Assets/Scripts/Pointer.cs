using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Pointer : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Texture2D cursorTexture2;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public bool rotateCursor = false;

    //public AudioClip fetherSound;
    public AudioSource audioSource;

    public AudioAssetScriptableObject AudioAssetSO;


    void Start()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        audioSource = GetComponent<AudioSource>();
        //        if (audioSource == null)
        //        {
        //            audioSource = GetComponent<AudioSource>();
        //        }
        //        Debug.Assert(fetherSound != null, "fetherSound sound is not set on " + gameObject.name);
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // Set flag to enable rotation when mouse button is pressed
            rotateCursor = true;
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
        else
        {
            // Reset rotation when mouse button is released
            rotateCursor = false;
            Cursor.SetCursor(cursorTexture2, hotSpot, cursorMode);
//            audioSource.clip = fetherSound;
            audioSource.Play();

        }
    }
}
