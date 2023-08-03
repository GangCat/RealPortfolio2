using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimatorController : AnimatorControllerBase
{
    /// <summary>
    /// 해당 이름의 파라미터의 int값을 변경한다.
    /// </summary>
    /// <param name="_paramName"></param>
    /// <param name="_intVal"></param>
    public void SetInteger(string _paramName, int _intVal)
    {
        anim.SetInteger(_paramName, _intVal);
    }

    public void OnAttack(string _attackType)
    {
        if (_attackType.Equals("CloseRangeAttack"))
            bossNoramlAttackAreaColliders[anim.GetInteger("attackType")].OnAttack(3.0f);
        else if(_attackType.Equals("LongRangeAttack"))
            bossNoramlAttackAreaColliders[anim.GetInteger("attackType") - 10].OnAttack(3.0f);
        else if(_attackType.Equals("CloseRangeSkillAttack"))
            bossSkillAttackAreaColliders[anim.GetInteger("skillType")].OnAttack(3.0f);
        else if(_attackType.Equals("LongRangeSkillAttack"))
            bossSkillAttackAreaColliders[anim.GetInteger("skillType") - 10].OnAttack(3.0f);
    }


    [SerializeField]
    private BossAttackAreaCollider[] bossNoramlAttackAreaColliders;
    [SerializeField]
    private BossAttackAreaCollider[] bossSkillAttackAreaColliders;
}