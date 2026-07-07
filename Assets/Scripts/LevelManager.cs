using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour
{
    [FormerlySerializedAs("level1Loop1Objects")] public GameObject[] Level1Loop1Objects;
    [FormerlySerializedAs("level1Loop2Objects")] public GameObject[] Level1Loop2Objects;
    [FormerlySerializedAs("level1Loop3Objects")] public GameObject[] Level1Loop3Objects;
    [FormerlySerializedAs("level2Objects")] public GameObject[] Level2Objects;
    public GameObject[] Level1UnlockedObjects;
    [FormerlySerializedAs("finalLevelObjects")] public GameObject[] FinalLevelObjects;
    [FormerlySerializedAs("movingObjects")] public LoopObjectState[] MovingObjects;

    public void ApplyPhase(GamePhase phase)
    {
        HashSet<GameObject> managedObjects = new HashSet<GameObject>();
        HashSet<GameObject> activeObjects = new HashSet<GameObject>();

        RegisterObjects(Level1Loop1Objects, phase == GamePhase.Level1Loop1, managedObjects, activeObjects);
        RegisterObjects(Level1Loop2Objects, phase == GamePhase.Level1Loop2, managedObjects, activeObjects);
        RegisterObjects(Level1Loop3Objects, phase == GamePhase.Level1Loop3, managedObjects, activeObjects);
        RegisterObjects(Level2Objects, phase == GamePhase.Level2, managedObjects, activeObjects);
        RegisterObjects(Level1UnlockedObjects, phase == GamePhase.Level1Unlocked, managedObjects, activeObjects);
        RegisterObjects(FinalLevelObjects, phase == GamePhase.FinalLevel, managedObjects, activeObjects);

        foreach (GameObject sceneObject in managedObjects)
        {
            sceneObject.SetActive(activeObjects.Contains(sceneObject));
        }

        foreach (LoopObjectState movingObject in MovingObjects)
        {
            if (movingObject != null)
            {
                bool shouldBeActive = phase != GamePhase.FinalLevel;
                movingObject.gameObject.SetActive(shouldBeActive);

                if (shouldBeActive)
                {
                    movingObject.ApplyPhase(phase);
                }
            }
        }
    }

    private void RegisterObjects(GameObject[] objects, bool active, HashSet<GameObject> managedObjects, HashSet<GameObject> activeObjects)
    {
        foreach (GameObject sceneObject in objects)
        {
            if (sceneObject == null)
            {
                continue;
            }

            managedObjects.Add(sceneObject);

            if (active)
            {
                activeObjects.Add(sceneObject);
            }
        }
    }
}
