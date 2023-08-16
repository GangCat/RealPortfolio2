using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    /// <summary>
    /// 다음 스테이지로 향할 수 있는 문의 트리거를 활성화
    /// </summary>
    public void ActivateDoorTrigger(int _curStageNum)
    {
        stages[_curStageNum].ActivateDoorTrigger();
    }

    public GameObject GetMinSpawnPoint(int _curStageNum)
    {
        return stages[_curStageNum].GetMinSpawnPoint();
    }

    public GameObject GetMaxSpawnPoint(int _curStageNum)
    {
        return stages[_curStageNum].GetMaxSpawnPoint();
    }

    public void Init(int _ttlStageCnt, OnPlayerMoveToNextStageDelegate _callback)
    {
        stages = new Stage[_ttlStageCnt];
        for (int i = 0; i < _ttlStageCnt; ++i)
        {
            Vector3 spawnPos = Vector3.zero;
            spawnPos.z += i * 60;
            GameObject stageGo = Instantiate(stagePrefabs[Random.Range(0, stagePrefabs.Length)], spawnPos, Quaternion.identity, transform);
            stageGo.GetComponent<Stage>().Init(_callback);
            stages[i] = stageGo.GetComponent<Stage>();
        }
    }

    [SerializeField]
    private GameObject[] stagePrefabs = null;

    private Stage[] stages = null;
}
