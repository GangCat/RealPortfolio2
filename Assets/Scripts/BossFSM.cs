using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossFSM : MonoBehaviour
{
    public enum EBossState { None = -1, Birth, Idle, Run, CloseRangeAttack, LongRangeAttack, CloseRangeSkillAttack, LongRangeSkillAttack, Dead }

    public void ChangeState(EBossState _newState)
    {
        if (bossState == _newState) return;

        StopCoroutine(bossState.ToString()); // 이전에 하던 행동을 먼저 정지
        bossState = _newState;
        StartCoroutine(bossState.ToString()); // 새롭게 설정된 행동을 실행
    }

    private IEnumerator Birth()
    {
        while (true)
        {
            yield return null;

            if (!bossAnim.CurAnimationIs("Birth"))
            {
                ChangeState(EBossState.Idle);
                StartCoroutine("CalcSkillDelay");
                yield break;
            }
        }
    }

    private IEnumerator Idle()
    {
        Debug.Log("Idle");
        yield return new WaitForSeconds(delayIdle);

        ChangeState(EBossState.Run);
        yield break;
    }

    private IEnumerator Run()
    {
        Debug.Log("Run");
        bossAnim.SetBool("isBossRun", true);
        float sqrDist = 0.0f;
        Vector3 moveDir = Vector3.zero;

        while (true)
        {
            yield return null;

            moveDir = CalcMoveDir();
            sqrDist = CalcDistanceToTarget(moveDir);
            ChangeStateWithDistance(sqrDist);

            Quaternion q = Quaternion.LookRotation(moveDir);
            transform.rotation = q;
            transform.Translate(transform.forward * statusSpeed.RunSpeed * Time.deltaTime, Space.World);

            /*rigid.velocity = moveDir.normalized * statusSpeed.WalkSpeed;
            //transform.Translate(moveDir.normalized * statusSpeed.WalkSpeed * Time.deltaTime, Space.World);
            //transform.LookAt(targetTr);
            //moveDir.y = transform.position.;
            //q = Quaternion.Euler(moveDir);
            //Matrix4x4 matRot = Matrix4x4.Rotate(q);
            //transform.forward = matRot.MultiplyPoint3x4(transform.forward);
             *   Matrix4x4 matRot = Matrix4x4.Rotate(transform.rotation);
                 matRot = matRot.inverse;
                 _moveDir = matRot.MultiplyPoint3x4(_moveDir).normalized;
             * 
             */
        }
    }

    private IEnumerator CloseRangeAttack()
    {
        int attackType = Random.Range(0, bossAttackSetting.closeRangeAttackCount);
        while (prevCloseRangeAttackNum == attackType)
        {
            if (bossAttackSetting.closeRangeAttackCount < 2)
                break;

            attackType = Random.Range(0, bossAttackSetting.closeRangeAttackCount);
            yield return null;
        }

        prevCloseRangeAttackNum = attackType;
        bossAnim.SetInteger("attackType", attackType);
        yield return new WaitForSeconds(1f);

        while (bossAnim.CurAnimationIs("Attack" + attackType))
            yield return null;

        bossAnim.SetInteger("attackType", -1);
        ChangeState(EBossState.Idle);
    }

    private IEnumerator LongRangeAttack()
    {
        int attackType = Random.Range(10, 10 + bossAttackSetting.longRangeAttackCount);
        while(prevLongRangeAttackNum == attackType)
        {
            if (bossAttackSetting.longRangeAttackCount < 2)
                break;

            attackType = Random.Range(10, 10 + bossAttackSetting.longRangeAttackCount);
            yield return null;
        }

        prevLongRangeAttackNum = attackType;
        bossAnim.SetInteger("attackType", attackType);
        yield return new WaitForSeconds(0.5f);

        while (bossAnim.CurAnimationIs("Attack" + attackType))
            yield return null;

        bossAnim.SetInteger("attackType", -1);
        ChangeState(EBossState.Idle);
    }

    private IEnumerator CloseRangeSkillAttack()
    {
        StopCoroutine("CalcSkillDelay");
        int skillType = Random.Range(0, bossAttackSetting.closeRangeSkillCount);
        while (prevCloseRangeSkillNum == skillType)
        {
            if (bossAttackSetting.closeRangeSkillCount < 2)
                break;

            skillType = Random.Range(0, bossAttackSetting.closeRangeSkillCount);
            yield return null;
        }

        prevCloseRangeSkillNum = skillType;
        bossAnim.SetInteger("skillType", skillType);
        yield return new WaitForSeconds(0.5f);

        while (bossAnim.CurAnimationIs("Skill" + skillType))
            yield return null;

        bossAnim.SetInteger("skillType", -1);
        ChangeState(EBossState.Idle);
        StartCoroutine("CalcSkillDelay");
        yield break;
    }

    private IEnumerator LongRangeSkillAttack()
    {
        StopCoroutine("CalcSkillDelay");
        int skillType = Random.Range(10, 10 + bossAttackSetting.closeRangeSkillCount);
        while (prevLongRangeSkillNum == skillType)
        {
            if (bossAttackSetting.longRangeSkillCount < 2)
                break;

            skillType = Random.Range(10, 10 + bossAttackSetting.closeRangeSkillCount);
            yield return null;
        }

        prevLongRangeSkillNum = skillType;
        bossAnim.SetInteger("skillType", skillType);
        yield return new WaitForSeconds(0.5f);

        while (bossAnim.CurAnimationIs("Skill" + skillType))
            yield return null;

        bossAnim.SetInteger("skillType", -1);
        ChangeState(EBossState.Idle);
        StartCoroutine("CalcSkillDelay");
        yield break;
    }

    private IEnumerator Dead()
    {


        yield return null;
    }

    private IEnumerator CalcSkillDelay()
    {
        timeAfterSkiilAttack = 0.0f;
        while (true)
        {
            timeAfterSkiilAttack += Time.deltaTime;
            yield return null;
        }
    }

    private Vector3 CalcMoveDir()
    {
        return targetTr.position - transform.position;
    }

    private float CalcDistanceToTarget(Vector3 _vec)
    {
        return Vector3.SqrMagnitude(_vec);
    }

    private void ChangeStateWithDistance(float _sqrDist)
    {
        if (_sqrDist > Mathf.Pow(bossAttackSetting.longRangeAttackRange, 2.0f))
            return;
        else if (_sqrDist > Mathf.Pow(bossAttackSetting.closeRangeAttackRange, 2.0f))
        {
            if (bossAttackSetting.hasLongRangeSkill)
            {
                if (timeAfterSkiilAttack > bossAttackSetting.skillDelayTime)
                {
                    bossAnim.SetBool("isBossRun", false);
                    ChangeState(EBossState.LongRangeSkillAttack);
                }
            }

            if (bossAttackSetting.hasLongRangeAttack)
            {
                bossAnim.SetBool("isBossRun", false);
                ChangeState(EBossState.LongRangeAttack);
            }

        }
        else
        {
            if (bossAttackSetting.hasCloseRangeSkill)
            {
                if (timeAfterSkiilAttack > bossAttackSetting.skillDelayTime)
                {
                    bossAnim.SetBool("isBossRun", false);
                    ChangeState(EBossState.CloseRangeSkillAttack);
                }
            }

            if (bossAttackSetting.hasCloseRangeAttack)
            {
                bossAnim.SetBool("isBossRun", false);
                ChangeState(EBossState.CloseRangeAttack);
            }

        }
    }


    private void Awake()
    {
        statusHp = GetComponent<StatusHP>();
        statusSpeed = GetComponent<StatusSpeed>();
    }

    private void OnEnable()
    {
        ChangeState(EBossState.Birth);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, bossAttackSetting.longRangeAttackRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, bossAttackSetting.closeRangeAttackRange);
    }


    [Header("-Conponents")]
    [SerializeField]
    private Collider[] bossColliders;
    [SerializeField]
    private BossAnimatorController bossAnim = null;

    [Header("-Test")]
    [SerializeField]
    private Transform targetTr = null;

    [Header("-BossAttackSetting")]
    [SerializeField]
    private BossAttackSetting bossAttackSetting;


    private float delayIdle = 2.0f;
    private float timeAfterSkiilAttack = 0.0f;

    private int prevCloseRangeAttackNum = -1;
    private int prevLongRangeAttackNum = -1;
    private int prevCloseRangeSkillNum = -1;
    private int prevLongRangeSkillNum = -1;

    private EBossState bossState = EBossState.None;
    private StatusHP statusHp = null;
    private StatusSpeed statusSpeed = null;
}
