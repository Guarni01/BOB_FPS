using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] firstLoopObjects;
    public GameObject[] secondLoopObjects;
    public GameObject[] finalLevelObjects;
    public LoopObjectState[] movingObjects;

    public void ApplyPhase(GamePhase phase)
    {
        SetObjectsActive(firstLoopObjects, phase == GamePhase.FirstLoop);
        SetObjectsActive(secondLoopObjects, phase == GamePhase.SecondLoop);
        SetObjectsActive(finalLevelObjects, phase == GamePhase.FinalLevel);

        foreach (LoopObjectState movingObject in movingObjects)
        {
            if (movingObject != null)
            {
                movingObject.ApplyPhase(phase);
            }
        }
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
