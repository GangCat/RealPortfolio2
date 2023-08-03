using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss01Skill10AreaCollider : BossAttackAreaCollider
{
    public override void OnAttack()
    {
        base.OnAttack();
        pullCollider.GetComponent<Collider>().enabled = true;
    }

    private void Update()
    {
        lastDamageTime += Time.deltaTime;
    }

    private void OnEnable()
    {
        lastDamageTime = 1.0f;
    }

    private void OnTriggerStay(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            if (lastDamageTime >= damageTickTime)
            {
                lastDamageTime = 0.0f;
                _other.GetComponent<PlayerCollider>().TakeDmg(dmg);
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
        pullCollider = GetComponentInChildren<Boss01Skill10PullAreaCollider>();
    }

    protected override IEnumerator AutoDeActivate()
    {
        yield return new WaitForSeconds(DeactivateTime);

        myCollider.enabled = false;
        pullCollider.GetComponent<Collider>().enabled = false;
        gameObject.SetActive(false);
    }

    [SerializeField]
    private float damageTickTime = 0.5f;

    private float lastDamageTime = 0.0f;
    private Boss01Skill10PullAreaCollider pullCollider = null;
}
