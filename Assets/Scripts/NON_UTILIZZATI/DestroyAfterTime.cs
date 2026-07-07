using UnityEngine;
using UnityEngine.Serialization;

public class DestroyAfterTime : MonoBehaviour
{
    [FormerlySerializedAs("time")] public float Lifetime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject,Lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
