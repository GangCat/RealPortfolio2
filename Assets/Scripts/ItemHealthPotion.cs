using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealthPotion : ItemBase
{
    public override void Use(GameObject _entity)
    {
        if (_entity.GetComponent<StatusHP>() != null)
        {
            _entity.GetComponent<StatusHP>().IncreaseHP(increaseHP);
            //Instantiate(healthEffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }


    [SerializeField]
    private GameObject healthEffectPrefab;
    [SerializeField]
    private int increaseHP = 50;
}