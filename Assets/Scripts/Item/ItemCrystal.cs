using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemCrystal : ItemBase
{
    public int MyRank 
    { 
        get => crystalInfo.myRank;
        set => crystalInfo.myRank = value;
    }
    private void Awake()
    {
        statusUIManager = GetComponentInParent<PlayerStatusUIManager>();
    }

    private void OnEnable()
    {
        Invoke("SetColliderEnable", 1.0f);
    }

    private void SetColliderEnable()
    {
        GetComponent<Collider>().enabled = true;
    }

    public override void Use(GameObject _entity)
    {
        if (_entity.CompareTag("Player"))
        {
            GameObject tempGo = statusUIManager.EquipCrystal(this);
            if (tempGo == null)
            {
                Destroy(gameObject);
                return;
            }

            GameObject crystalGo = Instantiate(tempGo, statusUIManager.transform);

            if (crystalGo == null)
                Destroy(gameObject);

            Vector3 spawnPos = transform.position;
            spawnPos.y = 0.4f;

            crystalGo.transform.position = spawnPos;

            Destroy(gameObject);
        }
    }

    public void SellCrystal(GameObject _entity)
    {
        if (_entity.CompareTag("Player"))
        {
            _entity.GetComponent<StatusGold>().IncreaseGold(60);
            Destroy(gameObject);
        }
    }

    private PlayerStatusUIManager statusUIManager;

    public SCrystalInfo crystalInfo;
}
