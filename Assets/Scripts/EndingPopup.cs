using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingPopup : MonoBehaviour
{
    public GameObject LoseText;
    public GameObject Buttons;
    public GameObject NextChar;
    public GameObject MainMenu2;

    public Sprite[] levelProgressSprites;
    public Image winImage;
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

    public void ShowPopup(bool isVictory)
    {

        if (isVictory)
        {

            this.gameObject.SetActive(true);
            // Hide the popup after victoryPopupDuration seconds
            winImage.sprite = levelProgressSprites[GameManager.Get().gameStateMachine.LevelIndex];
            if (!(GameManager.Get().gameStateMachine.LevelIndex == 4))
            {
                StartCoroutine(HideAfterDelay(victoryPopupDuration));
                MainMenu2.SetActive(false);
                LoseText.SetActive(false);
                Buttons.SetActive(false);
                NextChar.SetActive(true);
            }
            else
            {
                MainMenu2.SetActive(true);
            }

        }
        else
        {
            MainMenu2.SetActive(false);
            this.gameObject.SetActive(true);
            LoseText.SetActive(true);
            Buttons.SetActive(true);
            NextChar.SetActive(false);
        }
      
    }

    public void HidePopup()
    {
        MainMenu2.SetActive(false);
        // Hide the popup immediately if it's not victory or game over
        gameObject.SetActive(false);
    }

    IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // Hide the popup with animation after the delay
        StartCoroutine(AnimatePopup(false));



    }

    public void RestartGame()
    {
        GameManager.Get().StartNewGame();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Get().OnGameStateChanged += ShowPopup;
    }

}
