using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerShooting : MonoBehaviour
{
    [FormerlySerializedAs("gun")] public Gun CurrentGun;
    [FormerlySerializedAs("gunHolder")] public Transform GunHolder;
    private bool isHoldingShoot = false;

    void OnShoot()
    {
        isHoldingShoot = true;
    }

    void  OnShootRelease()
    {
        isHoldingShoot = false;
    }

    void OnReload()
    {
        if(CurrentGun != null)
        {
            CurrentGun.TryReload();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isHoldingShoot && CurrentGun != null)
        {
            CurrentGun.Shoot();
        }
    }
    public void OnDrop()
    {
        if(CurrentGun != null)
        {
            CurrentGun.Drop();
            CurrentGun = null;
        }
    }
}
