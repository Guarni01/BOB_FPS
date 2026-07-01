using UnityEngine;

public class LoopObjectState : MonoBehaviour
{
    public Transform firstLoopTransform;
    public Transform secondLoopTransform;
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
        if (phase == GamePhase.FirstLoop)
        {
            return firstLoopTransform;
        }

        if (phase == GamePhase.SecondLoop)
        {
            return secondLoopTransform;
        }

        return finalLevelTransform;
    }
}
