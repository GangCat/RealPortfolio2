using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public void Setup(MemoryPool _memoryPool, float _dmg, ImpactMemoryPool _impactPool)
    {
        impactPool = _impactPool;
        memoryPool = _memoryPool;
        dmg = _dmg;
    }

    private void OnEnable()
    {
        StartCoroutine("AutoDisable");
    }

    private IEnumerator AutoDisable()
    {
        yield return new WaitForSeconds(autoDisableTime);

        Disable();
    }

    private void Disable()
    {
        memoryPool.DeactivatePoolItem(gameObject);
    }

    private void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision != null)
        {
            if (_collision.transform.CompareTag("Enemy"))
            {
                SpawnImpact(_collision, -transform.forward);
                _collision.transform.GetComponent<EnemyController>().TakeDmg(dmg);
            }
            else if (_collision.transform.CompareTag("Interactive"))
            {
                SpawnImpact(_collision, -transform.forward);
                _collision.transform.GetComponent<InteractiveObject>().TakeDmg(dmg);
            }
            else if (_collision.transform.CompareTag("Player"))
            {
                _collision.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
                _collision.transform.GetComponent<PlayerCollider>().TakeDmg(dmg);
            }
            else if (_collision.transform.CompareTag("Wall"))
                SpawnImpact(_collision, -transform.forward);
        }

        Disable();
    }

    private void SpawnImpact(Collision _collision, Vector3 _dir)
    {
        Debug.Log(_collision.contacts[0].point);

        if (gameObject != null)
            impactPool.SpawnInit(_collision.GetContact(0).point, _dir);
    }


    [SerializeField]
    private float moveSpeed = 5.0f;
    [SerializeField]
    private float autoDisableTime = 0.5f;

    private float dmg = 0;
    private MemoryPool memoryPool = null;
    private ImpactMemoryPool impactPool = null;
}
