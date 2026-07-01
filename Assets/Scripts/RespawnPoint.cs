using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public Vector3 GetSpawnPosition()
    {
        return transform.position;
    }

    public Quaternion GetSpawnRotation()
    {
        return transform.rotation;
    }
}
