using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBallController : MonoBehaviour
{
    public void Setdmg(float _dmg)
    {
        dmg = _dmg;
    }

    private void Start()
    {
        StartCoroutine("AutoDestroy");
    }

    private IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(autoDestroyTime);

        Destroy(gameObject);
    }

    private void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            _other.GetComponent<PlayerCollider>().TakeDmg(dmg);
        }
    }



    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float autoDestroyTime;

    private float dmg;
}
