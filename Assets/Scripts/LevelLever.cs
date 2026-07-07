using UnityEngine;

public class LevelLever : MonoBehaviour
{
    public string PlayerTag = "Player";
    public GameObject[] ObjectsToHide;
    public GameObject[] ObjectsToShow;

    private bool isActivated;

    private void Start()
    {
        SetObjectsActive(ObjectsToShow, false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActivated || !other.CompareTag(PlayerTag))
        {
            return;
        }

        ActivateLever();
    }

    public void ActivateLever()
    {
        isActivated = true;

        SetObjectsActive(ObjectsToHide, false);
        SetObjectsActive(ObjectsToShow, true);
        Debug.Log("Lever activated.");
    }

    private void SetObjectsActive(GameObject[] objects, bool active)
    {
        foreach (GameObject sceneObject in objects)
        {
            if (sceneObject != null)
            {
                sceneObject.SetActive(active);
            }
        }
    }
}
