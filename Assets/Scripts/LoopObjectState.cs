using UnityEngine;
using UnityEngine.Serialization;

public class LoopObjectState : MonoBehaviour
{
    [FormerlySerializedAs("level1Loop1Transform")] public Transform Level1Loop1Transform;
    [FormerlySerializedAs("level1Loop2Transform")] public Transform Level1Loop2Transform;
    [FormerlySerializedAs("level1Loop3Transform")] public Transform Level1Loop3Transform;
    [FormerlySerializedAs("level2Transform")] public Transform Level2Transform;
    [FormerlySerializedAs("finalLevelTransform")] public Transform FinalLevelTransform;

    public void ApplyPhase(GamePhase phase)
    {
        Transform targetTransform = GetTargetTransform(phase);

        if (targetTransform == null)
        {
            return;
        }

        transform.position = targetTransform.position;
        transform.rotation = targetTransform.rotation;
    }

    private Transform GetTargetTransform(GamePhase phase)
    {
        if (phase == GamePhase.Level1Loop1)
        {
            return Level1Loop1Transform;
        }

        if (phase == GamePhase.Level1Loop2)
        {
            return Level1Loop2Transform;
        }

        if (phase == GamePhase.Level1Loop3)
        {
            return Level1Loop3Transform;
        }

        if (phase == GamePhase.Level2)
        {
            return Level2Transform;
        }

        return FinalLevelTransform;
    }
}
