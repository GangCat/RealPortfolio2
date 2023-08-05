using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    public void Setup(float _dmg)
    {
        dmg = _dmg;
    }

    public void Attack(float _attackTime)
    {
        myCollider.enabled = true;
        Invoke("AttackFinish", _attackTime);
    }

    private void AttackFinish()
    {
        myCollider.enabled = false;
    }

    private void Awake()
    {
        myCollider = GetComponent<Collider>();
    }

    private void Start()
    {
        myCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            _other.GetComponent<PlayerCollider>().TakeDmg(dmg);
            myCollider.enabled = false;
        }
    }

    private float dmg = 0;

    private Collider myCollider = null;
}
