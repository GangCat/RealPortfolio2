using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractiveBarrel : InteractiveObject
{
    public override void TakeDmg(float _dmg)
    {
        if (statusHp.DecreaseHP(_dmg))
        {
            Instantiate(ItemPrefabs[Random.Range(0,ItemPrefabs.Length)], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private GameObject[] ItemPrefabs;
}
