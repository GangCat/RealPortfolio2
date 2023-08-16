using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public void Init(OnPlayerMoveToNextStageDelegate _callback)
    {
        trigger = GetComponentInChildren<TriggerToNextStage>();
        trigger.OnPlayerMoveToNextStageCallback = _callback;
    }

    private TriggerToNextStage trigger = null;
}
