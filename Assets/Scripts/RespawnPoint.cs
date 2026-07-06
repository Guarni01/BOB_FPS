using UnityEngine;
using UnityEngine.Serialization;

public class RespawnPoint : MonoBehaviour
{
    [FormerlySerializedAs("spawnOffset")] public Vector3 SpawnOffset;

    public Vector3 GetSpawnPosition()
    {
        return transform.position + SpawnOffset;
    }

    public Quaternion GetSpawnRotation()
    {
        return transform.rotation;
    }
}
