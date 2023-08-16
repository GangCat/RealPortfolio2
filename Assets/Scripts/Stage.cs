using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public void ActivateDoorTrigger()
    {
        console.ActivateTrigger();
    }

    public GameObject GetMinSpawnPoint()
    {
        return minSpawnPoint.gameObject;
    }

    public GameObject GetMaxSpawnPoint()
    {
        return maxSpawnPoint.gameObject;
    }

    public void Init(OnPlayerMoveToNextStageDelegate _callback)
    {
        trigger = GetComponentInChildren<TriggerToNextStage>();
        console = GetComponentInChildren<ConsoleToNextStage>();

        trigger.OnPlayerMoveToNextStageCallback = _callback;
    }

    private TriggerToNextStage trigger = null;
    private ConsoleToNextStage console = null;

    [SerializeField]
    private MinSpawnPoint minSpawnPoint = null;
    [SerializeField]
    private MaxSpawnPoint maxSpawnPoint = null;
}
