using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    [FormerlySerializedAs("speed")] public float Speed = 15f;
    [FormerlySerializedAs("lifeTime")] public float LifeTime = 3f;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = -transform.right * Speed;
        Destroy(gameObject,LifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
