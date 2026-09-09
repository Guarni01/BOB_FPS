using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PickUp : MonoBehaviour
{
    [FormerlySerializedAs("highlightMaterial")] public Material HighlightMaterial;
    private Material[] originalMaterials;
    private  MeshRenderer[] meshRenderers;

    [FormerlySerializedAs("weaponePrefab")] public GameObject WeaponPrefab;
    [FormerlySerializedAs("lookRange")] public float LookRange = 3f;

    private bool isLookedAt = false;
    private Camera playerCam;
    private PlayerShooting player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [System.Obsolete]
    void Start()
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        originalMaterials = new Material [meshRenderers.Length];
        for(int i = 0; i < meshRenderers.Length; i++)
        {
            originalMaterials[i] = meshRenderers[i].material;
        }
        player = FindObjectOfType<PlayerShooting>();
        playerCam = player.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(playerCam.transform.position, playerCam.transform.forward);
        if(Physics.Raycast(ray, out RaycastHit hit, LookRange))
        {
            if(hit.collider.GetComponentInParent<PickUp>() == this)
            {
                if(!isLookedAt)
                  SetLookedAt(true);

                  return;
            }
        }

        if(isLookedAt)
          SetLookedAt(false);
    }

    void SetLookedAt(bool lookedAt)
    {
        isLookedAt = lookedAt;
        if(lookedAt)
        {
            foreach(MeshRenderer mr in meshRenderers)
            {
                mr.material = HighlightMaterial;
            }
        }
        else
        {
            for(int i = 0; i < meshRenderers.Length; i ++)
            {
                meshRenderers[i].material = originalMaterials[i];
            }
        }
    }

    public void OnPickUp()
    {
        if(!isLookedAt) return;

        if(player.CurrentGun != null)
        {
            player.OnDrop();
        }

        GameObject newWeapon = Instantiate(WeaponPrefab,player.GunHolder);
        newWeapon.transform.localPosition = Vector3.zero;
        newWeapon.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

        player.CurrentGun = newWeapon.GetComponent<Gun>();

        Destroy(gameObject);
    }

}
