using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    public BodyPart[] bodyParts;
    public int[] sequenceOrder;
    public int MaxTime;
    public GameObject smile;
    public float fadeDuration = 1f; // Duration of the fade effect

    private Renderer[] renderers;

    public void Awake()
    {
        if (bodyParts == null)
        {
            bodyParts = GetComponentsInChildren<BodyPart>();
        }

        // Cache the renderers of all body parts
        renderers = new Renderer[bodyParts.Length];
        for (int i = 0; i < bodyParts.Length; i++)
        {
            renderers[i] = bodyParts[i].GetComponent<Renderer>();
        }
    }

    void Start()
    {
        GameManager.Get().OnGameStateChanged += FadeAndDisable;
        smile.SetActive(false);
    }

    //void Update()
    //{

    //}

    public void FadeAndDisable(GameState gameState)
    {
        GameManager.Get().OnGameStateChanged -= FadeAndDisable;
        if (gameState == GameState.VICTORY)
        {
            StartCoroutine(FadeAndDisableCoroutine());
        }

    }

    public void MakeSmile()
    { smile.SetActive(true); }

    IEnumerator FadeAndDisableCoroutine()
    {
        // Fade out each renderer
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = 1 - (elapsedTime / fadeDuration); // Calculate alpha value for fading
            foreach (Renderer renderer in renderers)
            {
                if (renderer != null)
                {
                    foreach (Material material in renderer.materials)
                    {
                        Color color = material.color;
                        color.a = alpha;
                        material.color = color;
                    }
                }
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure transparency is set to 0 at the end of the fade effect
        foreach (Renderer renderer in renderers)
        {
            if (renderer != null)
            {
                foreach (Material material in renderer.materials)
                {
                    Color color = material.color;
                    color.a = 0f;
                    material.color = color;
                }
            }
        }

        // Disable the character
        GameManager.Get().NextStage();
        
        Destroy(gameObject);

    }
}
