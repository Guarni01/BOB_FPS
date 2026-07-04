using UnityEngine;

public class LoopObjectState : MonoBehaviour
{
    public Transform level1Loop1Transform;
    public Transform level1Loop2Transform;
    public Transform level1Loop3Transform;
    public Transform level2Transform;
    public Transform finalLevelTransform;

    public void ApplyPhase(GamePhase phase)
    {
        Transform targetTransform = GetTargetTransform(phase);

        if (targetTransform == null)
        {
            return;
        }

        transform.position = targetTransform.position;
        transform.rotation = targetTransform.rotation;
        transform.localScale = targetTransform.localScale;
    }

    private Transform GetTargetTransform(GamePhase phase)
    {
        if (phase == GamePhase.Level1Loop1)
        {
            return level1Loop1Transform;
        }

        if (phase == GamePhase.Level1Loop2)
        {
            return level1Loop2Transform;
        }

        if (phase == GamePhase.Level1Loop3)
        {
            return level1Loop3Transform;
        }

        if (phase == GamePhase.Level2)
        {
            return level2Transform;
        }

        return finalLevelTransform;
    }
}
