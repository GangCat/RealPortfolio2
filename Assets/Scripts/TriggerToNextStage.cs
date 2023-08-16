using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToNextStage : MonoBehaviour
{
    public OnPlayerMoveToNextStageDelegate OnPlayerMoveToNextStageCallback
    {
        set => onPlayerMoveToNextStageCallback = value;
    }


    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Player"))
            onPlayerMoveToNextStageCallback?.Invoke();
    }

    private OnPlayerMoveToNextStageDelegate onPlayerMoveToNextStageCallback = null;
}
