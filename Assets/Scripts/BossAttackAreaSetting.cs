using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackAreaSetting : MonoBehaviour
{
    public BossAttackAreaCollider[] CloseNormalAttacks => closeRangeNormalAttackAreaColliders;
    public BossAttackAreaCollider[] LongNormalAttacks => longRangeNormalAttackAreaColliders;
    public BossAttackAreaCollider[] CloseSkillAttacks => closeRangeSkillAttackAreaColliders;
    public BossAttackAreaCollider[] LongSkillAttacks => longRangeSkillAttackAreaColliders;


    private void Start()
    {
        int i = 0;
        for (i = 0; i < closeRangeAttackDamages.Length; ++i)
            closeRangeNormalAttackAreaColliders[i].Setup(closeRangeAttackDamages[i]);

        for (i = 0; i < longRangeAttackDamages.Length; ++i)
            longRangeNormalAttackAreaColliders[i].Setup(longRangeAttackDamages[i]);

        for (i = 0; i < closeRangeSkillDamages.Length; ++i)
            closeRangeSkillAttackAreaColliders[i].Setup(closeRangeSkillDamages[i]);

        for (i = 0; i < longRangeSkillDamages.Length; ++i)
            longRangeSkillAttackAreaColliders[i].Setup(longRangeSkillDamages[i]);
    }



    [Header("-Boss Attack Damege")]
    [SerializeField]
    private float[] closeRangeAttackDamages;
    [SerializeField]
    private float[] longRangeAttackDamages;
    [SerializeField]
    private float[] closeRangeSkillDamages;
    [SerializeField]
    private float[] longRangeSkillDamages;

    [Header("-Attack Area Colliders")]
    [SerializeField]
    private BossAttackAreaCollider[] closeRangeNormalAttackAreaColliders;
    [SerializeField]
    private BossAttackAreaCollider[] longRangeNormalAttackAreaColliders;
    [SerializeField]
    private BossAttackAreaCollider[] closeRangeSkillAttackAreaColliders;
    [SerializeField]
    private BossAttackAreaCollider[] longRangeSkillAttackAreaColliders;
}
