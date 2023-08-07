using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HPEvent : UnityEngine.Events.UnityEvent<float, float> { }
public class MaxHpEvent : UnityEngine.Events.UnityEvent<float> { }
public class StatusHP : MonoBehaviour
{
    [HideInInspector]
    public HPEvent onHPEvent = new HPEvent();
    public MaxHpEvent onMaxHpEvent = new MaxHpEvent();
    public float CurHP => curHP;
    public float MaxHP => maxHP;

    public bool DecreaseHP(float _dmg)
    {
        // 공격 받기 전 HP
        float prevHP = curHP;

        if(statusDefense != null)
            _dmg = statusDefense.DefenseDmg(_dmg);

        //float defense = 

        curHP -= (_dmg > 0 ? _dmg : 0);


        Debug.Log(this.name + curHP + "/" + maxHP);

        // 죽었으면 true 반환
        if (curHP <= 0)
        {
            curHP = 0;
            onHPEvent.Invoke(prevHP, curHP);
            return true;
        }

        onHPEvent.Invoke(prevHP, curHP);

        return false;
    }

    public void IncreaseHP(float _HP)
    {
        float prevHP = curHP;

        curHP = curHP + _HP > maxHP ? maxHP : curHP + _HP;

        onHPEvent.Invoke(prevHP, curHP);
    }

    public void ChangeMaxHP(float _ratio)
    {
        maxHP = maxHP * _ratio;
        onMaxHpEvent.Invoke(maxHP);
    }

    public float GetRatio()
    {
        return curHP / maxHP;
    }

    private void OnEnable()
    {
        curHP = maxHP;
    }

    private void Start()
    {
        oriMaxHp = maxHP;
    }

    private void Awake()
    {
        statusDefense = GetComponent<StatusDefense>();
    }

    [Header("-HP")]
    [SerializeField]
    private float curHP = 100;
    [SerializeField]
    private float maxHP = 200;

    private StatusDefense statusDefense;

    private float oriMaxHp;
}
