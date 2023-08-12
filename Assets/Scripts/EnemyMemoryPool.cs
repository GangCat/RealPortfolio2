using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyMemoryPool : MonoBehaviour
{
    public enum EEnemyType { Melee, Range, None }

    public GameObject SpawnInit(EEnemyType _enemyType, Vector3 _pos, Transform _parentTr)
    {
        GameObject enemyGo = memoryPools[(int)_enemyType].ActivatePoolItem();
        enemyGo.transform.parent = _parentTr;
        enemyGo.transform.position = _pos;
        return enemyGo;
    }




    private void Awake()
    {
        memoryPools = new MemoryPool[enemyPrefabs.Length];

        for(int i = 0; i< enemyPrefabs.Length; ++i)
        {
            memoryPools[i] = new MemoryPool(enemyPrefabs[i], 5);
        }
    }




    [SerializeField]
    private GameObject[] enemyPrefabs;

    private MemoryPool[] memoryPools;
}
