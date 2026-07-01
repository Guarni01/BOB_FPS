using UnityEngine;

public class Key : MonoBehaviour
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
            Debug.LogWarning("Key needs a GameManager in the scene.");
            return;
        }

        GameManager.Instance.GiveKey();
        Destroy(gameObject);
    }
}
