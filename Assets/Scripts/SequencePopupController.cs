
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SequencePopupController : MonoBehaviour
{
    public GameObject[] sequencePiecePrefabs; // Prefab of the sequence pieces
    public GameObject arrowPrefab; // Prefab of the arrow separator

    public float animationDuration = 0.5f; // Duration of the animation in seconds
    public float pieceSpacing = 10f; // Spacing between pieces
    public int maxPieces = 10;

    private void Start()
    {
        StartCoroutine(AnimateSequencePieces());
    }

    private IEnumerator AnimateSequencePieces()
    {
        // Calculate the total width of all pieces and separators
        float totalWidth = sequencePiecePrefabs.Length * (sequencePiecePrefabs[0].transform.localScale.x + pieceSpacing);

        // Calculate the initial x position for the first piece
        float initialX = -totalWidth / 1.8f;

        bool arrow = false;
        int pieceIndex = 0;

        float currentX = 0;
        float yPos = sequencePiecePrefabs.Length > maxPieces /2 ? sequencePiecePrefabs[pieceIndex].transform.localScale.y + pieceSpacing : 0;

        // Iterate over the sequence pieces
        for (int i = 0; i < (sequencePiecePrefabs.Length * 2) - 1; i++)
        {
            // Calculate the position for the current piece
            float xPos = initialX + (sequencePiecePrefabs[pieceIndex].transform.localScale.x + pieceSpacing) * i;

            // Check if xPos exceeds totalWidth, indicating a new row
            if (pieceIndex > ((maxPieces/2)-1))
            {
                // Start a new row
                currentX = initialX;
                // Move the y position down for the new row
                yPos = sequencePiecePrefabs[pieceIndex].transform.localScale.y - 20 - pieceSpacing;
                // Update xPos for the new row
                xPos = initialX + (sequencePiecePrefabs[pieceIndex].transform.localScale.x + pieceSpacing) * (i- (maxPieces-1));
            }

            // Instantiate the sequence piece
            GameObject piece = Instantiate(arrow ? arrowPrefab : sequencePiecePrefabs[pieceIndex], transform);

            // Set its local position relative to the popup
            piece.transform.localPosition = new Vector3(xPos, yPos, 0f);

            // Animate the piece
            StartCoroutine(AnimatePiece(piece.transform, xPos + pieceSpacing));

            // Wait for the animation to finish before instantiating the next piece
            yield return new WaitForSeconds(animationDuration / sequencePiecePrefabs.Length);

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
