using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactMemoryPool : MonoBehaviour
{
    public void SpawnInit(Vector3 _pos, Vector3 _dir)
    {
        OnSpawnImpact(_pos, _dir);
    }

    public void OnSpawnImpact(Vector3 _point, Vector3 _dir)
    {
        GameObject tempGO = memoryPool.ActivatePoolItem();
        tempGO.transform.position = _point;
        tempGO.transform.forward = _dir;
        tempGO.GetComponent<Impact>().Setup(memoryPool);
    }

    private void Start()
    {
        memoryPool = new MemoryPool(impactPrefab, increaseCnt);
    }


    [SerializeField]
    private int increaseCnt = 3;
    [SerializeField]
    private GameObject impactPrefab;
    private MemoryPool memoryPool;
}