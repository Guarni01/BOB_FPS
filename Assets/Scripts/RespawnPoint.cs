using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public Vector3 spawnOffset;

    public Vector3 GetSpawnPosition()
    {
        return transform.position + spawnOffset;
    }

    public Quaternion GetSpawnRotation()
    {
        return transform.rotation;
    }
}
