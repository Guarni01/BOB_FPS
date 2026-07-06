using UnityEngine;

public class FinalReturnObject : MonoBehaviour
{
    public string PlayerTag = "Player";

    private bool isUsed;

    private void OnTriggerEnter(Collider other)
    {
        if (isUsed || !other.CompareTag(PlayerTag))
        {
            return;
        }

        if (GameManager.Instance == null)
        {
            Debug.LogWarning("FinalReturnObject needs a GameManager in the scene.");
            return;
        }

        isUsed = true;
        GameManager.Instance.ReturnToUnlockedLevel1();
    }
}
