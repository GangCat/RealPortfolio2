using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public void Init(int _stageCnt, OnPlayerMoveToNextStageDelegate _callback)
    {
        for (int i = 0; i < _stageCnt; ++i)
        {
            Vector3 spawnPos = Vector3.zero;
            spawnPos.z += i * 60;
            GameObject stageGo = Instantiate(stagePrefabs[Random.Range(0, stagePrefabs.Length)], spawnPos, Quaternion.identity, transform);
            stageGo.GetComponent<Stage>().Init(_callback);
        }
    }

    [SerializeField]
    private GameObject[] stagePrefabs = null;
}
