
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SequencePopupController : MonoBehaviour
{
    public GameObject sequencePiecePrefab; // Prefab of the sequence pieces
    public GameObject arrowPrefab; // Prefab of the arrow separator

    public float animationDuration = 0.5f; // Duration of the animation in seconds
    public float fadeDuration = 0.3f; // Duration of the fade animation in seconds
    public float pieceSpacing = 10f; // Spacing between pieces
    public int maxPieces = 10;

    private Character currentCharacter;

    void OnEnable()
    {
    }

    private void Start()
    {
        GameManager.Get().OnGameStateChanged += ShowAnimateSequence;
        
    }

    public void ShowAnimateSequence(GameState currentState)
    {
        if(currentState == GameState.SOLUTION)
        {
            currentCharacter = GameManager.Get().currentCharacter;
            StartCoroutine(AnimateSequencePopup());
        }

    }

    private IEnumerator AnimateSequencePopup()
    {
        // Fade in animation
        yield return FadeIn();

        // Animate sequence pieces
        yield return AnimateSequencePieces();

        // Stay visible for a duration
        yield return new WaitForSeconds(currentCharacter.bodyParts.Length);

        // Fade out animation
        yield return FadeOut();

        // Start game
        GameManager.Get().StartTimer();
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
        // Calculate the total width of all pieces and separators
        float totalWidth = currentCharacter.bodyParts.Length * (sequencePiecePrefab.transform.localScale.x + pieceSpacing);

        // Calculate the initial x position for the first piece
        float divider = (currentCharacter.bodyParts.Length > (maxPieces / 2)) ? 1.8f : 1f;
        float initialX = -totalWidth / divider;

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

            // Set its texture to currentTexture if it's not an arrow piece
            if (!arrow)
            {
                int currentSequenceIndex = currentCharacter.sequenceOrder[pieceIndex];
                Image currentImage = currentCharacter.bodyParts[currentSequenceIndex].bodyImage;
                // Access the Image component of the piece
                piece.GetComponent<Image>().sprite = currentImage.sprite;
            }
            // Check if xPos exceeds totalWidth, indicating a new row
            if (pieceIndex > ((maxPieces / 2) - 1))
            {
                // Start a new row
                currentX = initialX;
                // Move the y position down for the new row
                yPos = sequencePiecePrefab.transform.localScale.y - 20 - pieceSpacing;
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
