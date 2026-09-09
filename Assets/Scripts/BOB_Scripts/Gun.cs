using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

public class Gun : MonoBehaviour
{
    [FormerlySerializedAs("reloadTime")] public float ReloadTime = 1f;
    [FormerlySerializedAs("fireRate")] public float FireRate = 0.15f;
    [FormerlySerializedAs("magSize")] public int MagSize = 20;

    [FormerlySerializedAs("bullet")] public GameObject BulletPrefab;
    [FormerlySerializedAs("bulletSpawnPoint")] public Transform BulletSpawnPoint;
    [FormerlySerializedAs("weaponFlash")] public GameObject WeaponFlash;
    [FormerlySerializedAs("droppedWeapon")] public GameObject DroppedWeapon;

    [FormerlySerializedAs("recoilDistance")] public float RecoilDistance = 0.1f;
    [FormerlySerializedAs("recoilSpeed")] public float RecoilSpeed = 15f;

    private int currentAmmo;
    private bool isReloading = false;
    private float nextTimeToFire = 0f;
    

    private Quaternion initialRotation;
    private Vector3 initialPosition;
    private Vector3 reloadRotationOffset = new Vector3(66,50,50);



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentAmmo = MagSize;
        initialRotation = transform.localRotation;
        initialPosition = transform.localPosition;
    }

    public void Shoot()
    {
        if(isReloading) return;
        if(Time.time < nextTimeToFire) return;

        if(currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        nextTimeToFire = Time.time + FireRate;
        currentAmmo --;

        Instantiate(BulletPrefab, BulletSpawnPoint.position, BulletSpawnPoint.rotation);
        Instantiate(WeaponFlash, BulletSpawnPoint.position, BulletSpawnPoint.rotation);

        StopCoroutine(nameof(Recoil));
        StartCoroutine(nameof(Recoil));

    }

    IEnumerator Reload()
    {
        isReloading = true;
        Quaternion targetRotation = Quaternion.Euler(initialRotation.eulerAngles + reloadRotationOffset);
        float halfReload = ReloadTime/2f;
        float t = 0f;
        
        while(t < halfReload)
        {
            t += Time.deltaTime;
            transform.localRotation = Quaternion.Slerp(initialRotation,targetRotation, t/ halfReload);
            yield return null;
        }
        
        t = 0f;

         while(t < halfReload)
        {
            t += Time.deltaTime;
            transform.localRotation = Quaternion.Slerp(targetRotation,initialRotation, t/ halfReload);
            yield return null;
        }

        currentAmmo = MagSize;
        isReloading = false;
    }

    public void TryReload()
    {
        if(isReloading) return;
        if(currentAmmo == MagSize) return;
        StartCoroutine(Reload());   
    }

    private IEnumerator Recoil()
    {
        Vector3 recoilTarget = initialPosition + new Vector3 (0, 0, RecoilDistance);
        float t = 0f;

        while(t < 1f)
        {
            t += Time.deltaTime * RecoilSpeed;
            transform.localPosition = Vector3.Lerp(initialPosition, recoilTarget, t);
            yield return null;
        }

        t = 0f;

        while(t < 1f)
        {
            t += Time.deltaTime * RecoilSpeed;
            transform.localPosition = Vector3.Lerp( recoilTarget,initialPosition, t);
            yield return null;
        }

        transform.localPosition = initialPosition;
    }

    public void Drop()
    {
        Instantiate(DroppedWeapon, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
