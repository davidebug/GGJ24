using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBackground : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float secondsToChangeColor = 2f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        InvokeRepeating("ChangeBackgroundColor", 0f, secondsToChangeColor);
    }

    void ChangeBackgroundColor()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value, 1f);
        spriteRenderer.color = randomColor;
    }
}