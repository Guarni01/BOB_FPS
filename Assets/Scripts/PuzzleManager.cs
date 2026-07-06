using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public int RequiredPieces = 3;
    public GameObject CompletedKey;

    [SerializeField] private int collectedPieces;
    private bool isPuzzleCompleted;

    void Start()
    {
        if (CompletedKey != null)
        {
            CompletedKey.SetActive(false);
        }
    }

    public void CollectPiece()
    {
        if (isPuzzleCompleted)
        {
            return;
        }

        collectedPieces++;
        Debug.Log("Puzzle piece collected: " + collectedPieces + "/" + RequiredPieces);

        if (collectedPieces >= RequiredPieces)
        {
            CompletePuzzle();
        }
    }

    private void CompletePuzzle()
    {
        isPuzzleCompleted = true;

        if (CompletedKey != null)
        {
            CompletedKey.SetActive(true);
        }

        Debug.Log("Puzzle completed. The key is now available.");
    }
}
