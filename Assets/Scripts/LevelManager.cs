using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] level1Loop1Objects;
    public GameObject[] level1Loop2Objects;
    public GameObject[] level1Loop3Objects;
    public GameObject[] level2Objects;
    public GameObject[] finalLevelObjects;
    public LoopObjectState[] movingObjects;

    public void ApplyPhase(GamePhase phase)
    {
        SetObjectsActive(level1Loop1Objects, phase == GamePhase.Level1Loop1);
        SetObjectsActive(level1Loop2Objects, phase == GamePhase.Level1Loop2);
        SetObjectsActive(level1Loop3Objects, phase == GamePhase.Level1Loop3);
        SetObjectsActive(level2Objects, phase == GamePhase.Level2);
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
