using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public void OpenDoor()
    {
        foreach(DoorToNextStage door in doors)
            door.OpenDoor();
    }

    public GameObject GetMinSpawnPoint()
    {
        return minSpawnPoint.gameObject;
    }

    public GameObject GetMaxSpawnPoint()
    {
        return maxSpawnPoint.gameObject;
    }

    public void Init(VoidVoidDelegate _moveToNextStageCallback, VoidVectorDelegate _teleportPlayerCallback)
    {
        triggers = GetComponentsInChildren<StageMoveTrigger>();
        doors = GetComponentsInChildren<DoorToNextStage>();

        foreach (StageMoveTrigger trigger in triggers)
        {
            trigger.Init(_moveToNextStageCallback, _teleportPlayerCallback);
            connections.Add(trigger.NextStagePos);
        }



        if (isStartStage)
            OpenDoor();
    }


    [SerializeField]
    private MinSpawnPoint minSpawnPoint = null;
    [SerializeField]
    private MaxSpawnPoint maxSpawnPoint = null;
    [SerializeField]
    private bool isStartStage = false;

    private List<ENextStagePos> connections = null;

    private StageMoveTrigger[] triggers = null;
    private DoorToNextStage[] doors = null;
}
