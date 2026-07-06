using UnityEngine;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour
{
    [FormerlySerializedAs("level1Loop1Objects")] public GameObject[] Level1Loop1Objects;
    [FormerlySerializedAs("level1Loop2Objects")] public GameObject[] Level1Loop2Objects;
    [FormerlySerializedAs("level1Loop3Objects")] public GameObject[] Level1Loop3Objects;
    [FormerlySerializedAs("level2Objects")] public GameObject[] Level2Objects;
    [FormerlySerializedAs("finalLevelObjects")] public GameObject[] FinalLevelObjects;
    [FormerlySerializedAs("movingObjects")] public LoopObjectState[] MovingObjects;

    public void ApplyPhase(GamePhase phase)
    {
        SetObjectsActive(Level1Loop1Objects, phase == GamePhase.Level1Loop1);
        SetObjectsActive(Level1Loop2Objects, phase == GamePhase.Level1Loop2);
        SetObjectsActive(Level1Loop3Objects, phase == GamePhase.Level1Loop3);
        SetObjectsActive(Level2Objects, phase == GamePhase.Level2);
        SetObjectsActive(FinalLevelObjects, phase == GamePhase.FinalLevel);

        foreach (LoopObjectState movingObject in MovingObjects)
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
