using UnityEngine;
using UnityEngine.Serialization;

public class Key : MonoBehaviour
{
    [FormerlySerializedAs("playerTag")] public string PlayerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(PlayerTag))
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
