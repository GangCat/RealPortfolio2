using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public void TakeDmg(float _dmg)
    {
        if (statusHp.DecreaseHp(_dmg))
        {
            StartCoroutine("LerpAxisY");
            StopCoroutine("FindPath");
            StopCoroutine("Attack");

            Debug.Log(gameObject.name);

            isDead = true;
            gameObject.layer = 13;
            anim.Play("Die", -1, 0);
            Invoke("SetDeactive", 3f);
        }
    }

    private void SetDeactive()
    {
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        statusHp = GetComponent<StatusHP>();
        statusSpeed = GetComponent<StatusSpeed>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CalcDistanceToTarget();
    }

    private void CalcDistanceToTarget()
    {
        if (targetTr == null) return;

        if (isDead) return;

        if (Vector3.SqrMagnitude(targetTr.position - transform.position) < Mathf.Pow(weaponBase.AttackDistance, 2f) && !isAttack)
        {
            anim.SetTrigger("onEngage");
            StopCoroutine("FindPath");
            StartCoroutine("Attack");
        }
    }

    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision.gameObject.CompareTag("Floor"))
        {
            roomCollider = _collision.gameObject.GetComponent<RoomCollider>();
            SetEvent();
        }
        else if(_collision.gameObject.CompareTag("Player"))
        {
            rigid.isKinematic = true;
        }
    }

    private void OnCollisionExit(Collision _collision)
    {
        if (_collision.gameObject.CompareTag("Player") && rigid.isKinematic)
            rigid.isKinematic = false;
    }

    private void SetEvent()
    {
        roomCollider.onEngageEvent.AddListener(SetTarget);
    }

    private void SetTarget(Transform _targetTr)
    {
        targetTr = _targetTr;
        weaponBase.TargetTr = targetTr;
        StartCoroutine("FindPath");
    }

    private IEnumerator FindPath()
    {
        anim.SetTrigger("onWalk");

        while (true)
        {
            Vector3 moveDir = (targetTr.position - transform.position).normalized;
            transform.Translate(moveDir * statusSpeed.WalkSpeed * Time.deltaTime, Space.World);

            transform.LookAt(targetTr);
            yield return null;
        }
    }

    private IEnumerator Attack()
    {
        isAttack = true;

        yield return new WaitForSeconds(0.7f); // 때리기 전 딜레이

        while (true)
        {
            if (weaponBase.AttackType == EAttackType.Range)
            {
                transform.LookAt(targetTr);
                if (weaponBase.CurAmmo <= 0)
                {
                    yield return new WaitForSeconds(3.0f);
                    weaponBase.Reload();
                }
            }

            
            weaponBase.OnAttack();
            yield return new WaitForSeconds(weaponBase.AttackRate);

            if (Vector3.SqrMagnitude(targetTr.position - transform.position) > Mathf.Pow(weaponBase.LimitDistance, 2f))
            {
                StartCoroutine("FindPath");
                isAttack = false;
                yield break;
            }
        }
    }

    private IEnumerator LerpAxisY()
    {
        yield return new WaitForSeconds(1.0f);

        float startTime = Time.time;
        float lerpTime = 0.7f;
        Vector3 targetPos = new Vector3(transform.position.x, -1.2f, transform.position.z);

        while (true)
        {
            transform.position = 
                Vector3.Lerp(transform.position, targetPos, (Time.time - startTime) / lerpTime);

            yield return null;
        }
    }

    [SerializeField]
    private EnemyWeaponBase weaponBase = null;

    private bool isAttack = false;
    private bool isDead = false;

    private Transform targetTr = null;
    private Rigidbody rigid = null;
    private StatusHP statusHp = null;
    private StatusSpeed statusSpeed = null;
    private RoomCollider roomCollider = null;
    private Animator anim = null;
}