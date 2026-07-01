using UnityEngine;

public class LoopDoor : MonoBehaviour
{
    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag))
        {
            return;
        }

        if (GameManager.Instance == null)
        {
            Debug.LogWarning("LoopDoor needs a GameManager in the scene.");
            return;
        }

        GameManager.Instance.EnterLoopDoor();
    }
}
