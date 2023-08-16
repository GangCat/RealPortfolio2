using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMemoryPool : MonoBehaviour
{

    public GameObject SpawnInit(EEnemyType _enemyType, Vector3 _pos, Transform _parentTr)
    {
        GameObject enemyGo = memoryPools[(int)_enemyType].ActivatePoolItem(5, _parentTr);
        enemyGo.transform.position = _pos;
        enemyGo.GetComponent<EnemyController>().Init(_enemyType);
        enemyGo.GetComponent<EnemyController>().OnDeactivateCallback = DeactivateEnemy;
        return enemyGo;
    }

    private void DeactivateEnemy(EEnemyType _enemyType, GameObject _enemyGo)
    {
        memoryPools[(int)_enemyType].DeactivatePoolItem(_enemyGo);
    }

    public void SetupEnemyMemoryPool(Transform _parentTr)
    {
        for (int i = 0; i < enemyPrefabs.Length; ++i)
        {
            memoryPools[i] = new MemoryPool(enemyPrefabs[i], 5, _parentTr);
        }
    }


    private void Awake()
    {
        memoryPools = new MemoryPool[enemyPrefabs.Length];
    }




    [SerializeField]
    private GameObject[] enemyPrefabs;

    private MemoryPool[] memoryPools;
}
