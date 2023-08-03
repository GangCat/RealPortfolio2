using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMemoryPool : MonoBehaviour
{
    public void SpawnProjectile(Vector3 _pos, Quaternion _quaternion, float _dmg)
    {
        GameObject projectileGo = memoryPool.ActivatePoolItem();
        projectileGo.transform.position = _pos;
        projectileGo.transform.rotation = _quaternion;
        projectileGo.GetComponent<ProjectileController>().Setup(memoryPool, _dmg, impactMemoryPool);
    }

    private void Start()
    {
        impactMemoryPool = GetComponent<ImpactMemoryPool>();
        memoryPool = new MemoryPool(ProjectilePrefab, increaseCnt);
    }

    [SerializeField]
    private GameObject ProjectilePrefab;
    [SerializeField]
    private int increaseCnt = 5;

    private MemoryPool memoryPool;
    private ImpactMemoryPool impactMemoryPool;
}
