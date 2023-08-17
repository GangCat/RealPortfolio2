using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ENextStagePos { None = -1, Right, Left, Front, Back }

public class StageMoveTrigger : MonoBehaviour
{
    public void Init(VoidVoidDelegate _playerMoveToNextStageCallback, VoidVectorDelegate _teleportPlayerCallback)
    {
        playerMoveToNextStageCallback = _playerMoveToNextStageCallback;
        teleportPlayerCallback = _teleportPlayerCallback;
    }

    public ENextStagePos NextStagePos => nextStagePos;

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            playerMoveToNextStageCallback?.Invoke();
            teleportPlayerCallback?.Invoke(myDir * 15f);
        }
    }

    private void Start()
    {
        switch (nextStagePos)
        {
            case ENextStagePos.None:
                break;
            case ENextStagePos.Right:
                myDir = Vector3.right;
                break;
            case ENextStagePos.Left:
                myDir = Vector3.left;
                break;
            case ENextStagePos.Front:
                myDir = Vector3.forward;
                break;
            case ENextStagePos.Back:
                myDir = Vector3.back;
                break;
        }
    }
    [SerializeField]
    private ENextStagePos nextStagePos = ENextStagePos.None;

    private Vector3 myDir = Vector3.zero;

    private VoidVoidDelegate playerMoveToNextStageCallback = null;
    private VoidVectorDelegate teleportPlayerCallback = null;
}
