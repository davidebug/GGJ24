using System.Collections;
using UnityEngine;

public class EndingPopup : MonoBehaviour
{
    public GameObject LoseText;
    public GameObject Buttons;
    public GameObject NextChar;

    public float animationDuration = 0.5f;
    public float victoryPopupDuration = 4f;

    void OnEnable()
    {
        // Show the popup with animation when it's enabled
        StartCoroutine(AnimatePopup(true));
    }

    IEnumerator AnimatePopup(bool show)
    {
        // Initial scale based on whether we're showing or hiding
        Vector3 startScale = show ? Vector3.zero : Vector3.one;
        Vector3 endScale = show ? Vector3.one : Vector3.zero;

        // Apply initial scale
        transform.localScale = startScale;

        // Animation loop
        float elapsedTime = 0f;
        while (elapsedTime < animationDuration)
        {
            // Interpolate scale
            float t = elapsedTime / animationDuration;
            transform.localScale = Vector3.Lerp(startScale, endScale, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure scale is set correctly at the end of animation
        transform.localScale = endScale;

        // Disable the popup if hiding
        if (!show)
        {
            gameObject.SetActive(false);
        }
    }

    void ShowPopup(GameState currentState)
    {
        if (currentState == GameState.VICTORY)
        {
            this.gameObject.SetActive(true);
            // Hide the popup after victoryPopupDuration seconds
            StartCoroutine(HideAfterDelay(victoryPopupDuration));
            LoseText.SetActive(false);
            Buttons.SetActive(false);
            NextChar.SetActive(true);
        }
        else if (currentState == GameState.GAME_OVER)
        {
            this.gameObject.SetActive(true);
            LoseText.SetActive(true);
            Buttons.SetActive(true);
            NextChar.SetActive(false);
        }
        else
        {
            // Hide the popup immediately if it's not victory or game over
            StartCoroutine(AnimatePopup(false));
        }
    }

    IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // Hide the popup with animation after the delay
        StartCoroutine(AnimatePopup(false));

        GameManager.Get().NextStage();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Get().OnGameStateChanged += ShowPopup;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
