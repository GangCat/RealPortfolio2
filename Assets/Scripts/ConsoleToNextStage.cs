using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleToNextStage : InteractiveBase
{
    public void Init()
    {
        GetComponent<Collider>().enabled = false;

    }

    public void ActivateTrigger()
    {
        GetComponent<Collider>().enabled = true;
    }

    public override void Use()
    {
        door.OpenDoor();
    }

    private void Awake()
    {
        door = GetComponentInChildren<DoorToNextStage>();
    }

    private DoorToNextStage door = null;
}
