using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleToNextStage : InteractiveBase
{
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
