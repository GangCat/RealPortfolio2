using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDmgEvent : UnityEngine.Events.UnityEvent<float[]> { }
public class SkillCooltimeEvent : UnityEngine.Events.UnityEvent<float[]> { }
public class StatusSkill : MonoBehaviour
{
    public SkillDmgEvent onSkillDmgEvent = new SkillDmgEvent();
    public SkillCooltimeEvent onSkillCooltimeEvent = new SkillCooltimeEvent();
    public float[] SkillDmgs => skillDmgs;
    public float[] SkillCooltimes => skillCooltimes;


    public void ChangeSkillDmgs(float _ratio, float _duration)
    {
        if (isSkillDmgBuff)
            StopCoroutine("ResetSkillDmgs");

        isSkillDmgBuff = true;

        for (int i = 0; i < skillDmgs.Length; ++i)
            skillDmgs[i] = skillDmgs[i] * _ratio > maxSkillDmg ? maxSkillDmg : skillDmgs[i] * _ratio;


        onSkillDmgEvent.Invoke(skillDmgs);

        StartCoroutine("ResetSkillDmgs", _duration);
    }

    private IEnumerator ResetSkillDmgs(float _duration)
    {
        yield return new WaitForSeconds(_duration);

        for (int i = 0; i < skillDmgs.Length; ++i)
            skillDmgs[i] = oriSkillDmgs[i];

        onSkillDmgEvent.Invoke(skillDmgs);
        isSkillDmgBuff = false;
    }

    public void ChangeSkillCooltimes(float _ratio, float _duration)
    {
        if (isSkillCooltimeBuff)
            StopCoroutine("ResetSkillCooltimes");

        isSkillCooltimeBuff = true;

        for (int i = 0; i < skillCooltimes.Length; ++i)
            skillCooltimes[i] = skillCooltimes[i] * _ratio < minSkillCooltime ? minSkillCooltime : skillCooltimes[i] * _ratio;

        onSkillDmgEvent.Invoke(skillCooltimes);

        StartCoroutine("ResetSkillCooltimes", _duration);
    }

    private IEnumerator ResetSkillCooltimes(float _duration)
    {
        yield return new WaitForSeconds(_duration);

        for (int i = 0; i < skillCooltimes.Length; ++i)
            skillCooltimes[i] = oriSkillCooltimes[i];

        onSkillDmgEvent.Invoke(skillCooltimes);
        isSkillCooltimeBuff = false;
    }

    private void Start()
    {
        oriSkillDmgs = new float[skillDmgs.Length];
        oriSkillCooltimes = new float[SkillCooltimes.Length];

        for (int i = 0; i < oriSkillCooltimes.Length; ++i)
            oriSkillCooltimes[i] = skillCooltimes[i];

        for (int i = 0; i < oriSkillDmgs.Length; ++i)
            oriSkillDmgs[i] = skillDmgs[i];
    }



    [SerializeField]
    private float[] skillDmgs; // 스킬 데미지
    [SerializeField]
    private float maxSkillDmg; // 최대 스킬 데미지
    [SerializeField]
    private float[] skillCooltimes; // 스킬 쿨타임
    [SerializeField]
    private float minSkillCooltime; // 최소 스킬 쿨타임

    private float[] oriSkillDmgs;
    private float[] oriSkillCooltimes;

    private bool isSkillDmgBuff = false;
    private bool isSkillCooltimeBuff = false;
}
