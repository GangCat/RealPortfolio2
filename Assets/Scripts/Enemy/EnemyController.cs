using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IPauseObserver
{
    public OnEnemyDeadDelegate OnEnemyDeadCallback
    {
        set => onEnemyDeadCallback = value;
    }

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
            onEnemyDeadCallback?.Invoke();
            StartCoroutine("SetDeactive", 3f);
        }
    }

    public void CheckPaused(bool _isPaused)
    {
        isPaused = _isPaused;

        if (isPaused)
            anim.StartPlayback();
        else
            anim.StopPlayback();

        weapon.TogglePause();
    }

    public void Setup(Transform _targetTr, OnEnemyDeadDelegate _onEnemyDeadCallback)
    {
        targetTr = _targetTr;
        weapon.TargetTr = _targetTr;
        onEnemyDeadCallback = _onEnemyDeadCallback;
    }


    private void Awake()
    {
        statusHp = GetComponent<StatusHP>();
        statusSpeed = GetComponent<StatusSpeed>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        gameManager = GameManager.Instance;
    }

    private void Start()
    {
        gameManager.RegisterPauseObserver(GetComponent<IPauseObserver>());
        StartCoroutine("FindPath");
    }

    private void Update()
    {
        if (!isPaused)
            CalcDistanceToTarget();
    }

    private void CalcDistanceToTarget()
    {
        if (targetTr == null) return;

        if (isDead) return;

        if (Vector3.SqrMagnitude(targetTr.position - transform.position) < Mathf.Pow(weapon.AttackDistance, 2f) && !isAttack)
        {
            anim.SetTrigger("onEngage");
            StopCoroutine("FindPath");
            StartCoroutine("Attack");
        }
    }

    private IEnumerator FindPath()
    {
        anim.SetTrigger("onWalk");

        while (true)
        {
            while (isPaused)
                yield return null;

            Vector3 moveDir = (targetTr.position - transform.position).normalized;
            transform.Translate(moveDir * statusSpeed.WalkSpeed * Time.deltaTime, Space.World);

            Rotate(moveDir);
            yield return null;
        }
    }

    private void Rotate(Vector3 _rotDir)
    {
        float angle = MyMathf.CalcAngleToTarget(_rotDir);
        MyMathf.RotateYaw(transform, angle);
    }

    private IEnumerator Attack()
    {
        isAttack = true;

        yield return StartCoroutine("WaitSeconds", 0.7f);

        while (true)
        {
            if (weapon.AttackType == EAttackType.Range)
            {
                Rotate(targetTr.position - transform.position);

                if (weapon.CurAmmo <= 0)
                {
                    weapon.Reload();
                    yield return StartCoroutine("WaitSeconds", 3f);
                }
            }

            yield return null;

            weapon.OnAttack();

            yield return StartCoroutine("WaitSeconds", weapon.AttackRate);

            if (Vector3.SqrMagnitude(targetTr.position - transform.position) > Mathf.Pow(weapon.LimitDistance, 2f))
            {
                StartCoroutine("FindPath");
                isAttack = false;
                yield break;
            }
        }
    }

    private IEnumerator WaitSeconds(float _delayTime)
    {
        float curTime = Time.time;
        while (Time.time - curTime < _delayTime)
        {
            if (isPaused)
                curTime += Time.deltaTime;

            yield return null;
        }
    }

    private IEnumerator SetDeactive(float _delayTime)
    {
        yield return StartCoroutine("WaitSeconds", _delayTime);

        gameObject.SetActive(false);
    }

    /// <summary>
    /// 죽는 애니메이션이 이상해서 y축 포지션을 보완해주려고 만든 코루틴
    /// </summary>
    /// <returns></returns>
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

    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision.gameObject.CompareTag("Floor"))
        {
            roomCollider = _collision.gameObject.GetComponent<RoomCollider>();
            //SetEvent();
        }
        else if (_collision.gameObject.CompareTag("Player"))
        {
            rigid.isKinematic = true;
        }
    }

    //private void SetEvent()
    //{
    //    roomCollider.onEngageEvent.AddListener(
    //        (_targetTr) =>
    //        {
    //            targetTr = _targetTr;
    //            weapon.TargetTr = targetTr;
                
    //        }
    //        );
    //}

    private void OnCollisionExit(Collision _collision)
    {
        if (_collision.gameObject.CompareTag("Player") && rigid.isKinematic)
            rigid.isKinematic = false;
    }


    [SerializeField]
    private WeaponBase weapon = null;

    private bool isAttack = false;
    private bool isDead = false;
    private bool isPaused = false;

    private Transform targetTr = null;
    private Rigidbody rigid = null;
    private StatusHP statusHp = null;
    private StatusSpeed statusSpeed = null;
    private RoomCollider roomCollider = null;
    private Animator anim = null;
    private GameManager gameManager = null;
    private OnEnemyDeadDelegate onEnemyDeadCallback = null;
}