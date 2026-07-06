using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    public string PlayerTag = "Player";
    public PuzzleManager PuzzleManager;

    private bool isCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (isCollected || !other.CompareTag(PlayerTag))
        {
            return;
        }

        if (PuzzleManager == null)
        {
            Debug.LogWarning("PuzzlePiece needs a PuzzleManager.");
            return;
        }

        isCollected = true;
        PuzzleManager.CollectPiece();
        Destroy(gameObject);
    }
}
