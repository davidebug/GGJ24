
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SequencePopupController : MonoBehaviour
{
    public GameObject[] sequencePiecePrefabs; // Prefab of the sequence piece
    public GameObject arrowPrefab; // Prefab of the sequence piece

    public int numberOfPieces = 5; // Number of sequence pieces
    public float animationDuration = 0.5f; // Duration of the animation in seconds
    public float pieceSpacing = 10f; // Spacing between pieces

    private void Start()
    {
        // Calculate the total width of all pieces and separators
        float totalWidth = sequencePiecePrefabs[0].transform.localScale.x * numberOfPieces + pieceSpacing * (numberOfPieces - 1);

        // Calculate the initial x position for the first piece
        float initialX = -totalWidth;

        // Calculate the position for the arrow
        float arrowXPos = initialX;
        bool arrow = false;

        for (int i = 0; i < (sequencePiecePrefabs.Length * 2) - 1; i++)
        {
            // Calculate the position for the current piece
            float xPos = initialX + (sequencePiecePrefabs[i].transform.localScale.x + pieceSpacing) * i;

            // Instantiate the sequence piece
            GameObject piecePrefab;
            if (arrow)
            {
                piecePrefab = arrowPrefab;
            }
            else
            {
                int sequencePieceIndex = i / 2; // Divide by 2 since there's an arrow between each sequence piece
                piecePrefab = sequencePiecePrefabs[sequencePieceIndex];
            }

            GameObject piece = Instantiate(piecePrefab, transform);

            // Set its local position relative to the popup
            piece.transform.localPosition = new Vector3(xPos, 0f, 0f);

            // Animate the piece
            StartCoroutine(AnimatePiece(piece.transform, xPos + pieceSpacing));

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
