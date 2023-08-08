using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestructibleBarrel : DestructibleObject
{
    public override void TakeDmg(float _dmg)
    {
        if (statusHp.DecreaseHp(_dmg))
        {
            Instantiate(ItemPrefabs[Random.Range(0,ItemPrefabs.Length)], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private GameObject[] ItemPrefabs;
}
