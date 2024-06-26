
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SequencePopupController : MonoBehaviour
{
    public GameObject sequencePiecePrefab; // Prefab of the sequence pieces
    public GameObject arrowPrefab; // Prefab of the arrow separator
    public GameObject startingPosition;

    public float animationDuration = 0.5f; // Duration of the animation in seconds
    public float fadeDuration = 0.3f; // Duration of the fade animation in seconds
    public float pieceSpacing = 10f; // Spacing between pieces
    public int maxPieces = 10;

    private Character currentCharacter;
    private List<GameObject> istantiatedObjs = new List<GameObject>();

    private bool Subscribed = false;
    void OnEnable()
    {
        if (GameManager.Get())
        {
            GameManager.Get().OnGameStateChanged += ShowAnimateSequence;
            Subscribed = true;  
        }
    }

    private void Start()
    {
        if (!Subscribed)
        {
            GameManager.Get().OnGameStateChanged += ShowAnimateSequence;
        }
    }

    public void ShowAnimateSequence(GameState currentState)
    {
        if(currentState == GameState.SOLUTION)
        {
            this.gameObject.SetActive(true);
            currentCharacter = GameManager.Get().currentCharacter;
            StartCoroutine(AnimateSequencePopup());
        }

    }

    private IEnumerator AnimateSequencePopup()
    {
        this.gameObject.SetActive(true);

        // Fade in animation
        yield return FadeIn();

        // Animate sequence pieces
        yield return AnimateSequencePieces();

        // Stay visible for a duration
        yield return new WaitForSeconds(currentCharacter.bodyParts.Length);

        // Fade out animation
        yield return FadeOut();

        this.gameObject.SetActive(false);

        // Start game
        GameManager.Get().StartGame();

        Reset();
    }

    private void Reset()
    {
        currentCharacter = null;
        foreach (GameObject obj in istantiatedObjs)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
    }

    private IEnumerator FadeIn()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f; // Ensure alpha is set to 1 at the end
    }

    private IEnumerator FadeOut()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;
        // Ensure alpha is set to 0 at the end
    }

    private IEnumerator AnimateSequencePieces()
    {

        float initialX = startingPosition.transform.localPosition.x ;

        bool arrow = false;
        int pieceIndex = 0;

        float currentX = 0;
        float yPos = currentCharacter.bodyParts.Length > maxPieces / 2 ? sequencePiecePrefab.transform.localScale.y + pieceSpacing : 0;

        // Iterate over the sequence pieces
        for (int i = 0; i < (currentCharacter.bodyParts.Length * 2) - 1; i++)
        {
            // Calculate the position for the current piece
            float xPos = initialX + (sequencePiecePrefab.transform.localScale.x + pieceSpacing) * i;

            // Instantiate the sequence piece
            GameObject piece = Instantiate(arrow ? arrowPrefab : sequencePiecePrefab, transform);
            istantiatedObjs.Add(piece);

            // Set its texture to currentTexture if it's not an arrow piece
            if (!arrow)
            {
                int correctSequenceIndex = currentCharacter.sequenceOrder[pieceIndex];
                //Debug.Log($"piece index {pieceIndex} taking {correctSequenceIndex} from sequence in image {currentCharacter.bodyParts[correctSequenceIndex].bodyNumberIndex}");
                Image currentImage = currentCharacter.bodyParts[correctSequenceIndex].bodyImage;
                // Access the Image component of the piece
                piece.GetComponent<Image>().sprite = currentImage.sprite;
            }
            // Check if xPos exceeds totalWidth, indicating a new row
            if (pieceIndex > ((maxPieces / 2) - 1))
            {
                // Start a new row
                currentX = initialX;
                // Move the y position down for the new row
                yPos = sequencePiecePrefab.transform.localScale.y - pieceSpacing;
                // Update xPos for the new row
                xPos = initialX + (sequencePiecePrefab.transform.localScale.x + pieceSpacing) * (i - (maxPieces - 1));
            }


            // Set its local position relative to the popup
            piece.transform.localPosition = new Vector3(xPos, yPos, 0f);

            // Animate the piece
            StartCoroutine(AnimatePiece(piece.transform, xPos + pieceSpacing));

            // Wait for the animation to finish before instantiating the next piece
            yield return new WaitForSeconds(animationDuration / currentCharacter.bodyParts.Length);

            if (!arrow)
                pieceIndex++;

            arrow = !arrow;
        }
    }

    private IEnumerator AnimatePiece(Transform piece, float targetX)
    {
        float elapsedTime = 0f;
        float startX = piece.localPosition.x;

        while (elapsedTime < animationDuration)
        {
            float newX = Mathf.Lerp(startX, targetX, elapsedTime / animationDuration);
            piece.localPosition = new Vector3(newX, piece.localPosition.y, piece.localPosition.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        piece.localPosition = new Vector3(targetX, piece.localPosition.y, piece.localPosition.z);
    }
}
