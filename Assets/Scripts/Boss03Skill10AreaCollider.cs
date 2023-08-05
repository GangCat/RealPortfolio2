using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss03Skill10AreaCollider : BossAttackAreaCollider
{
    public override void OnAttack()
    {
        effectEnergyBallCharge.SetActive(false);
        GameObject energyBallGo = Instantiate(energyBallPrefab, energyBallSpawnTransform.position, energyBallSpawnTransform.rotation);
        energyBallGo.GetComponent<EnergyBallController>().Setdmg(dmg);
        StartCoroutine("AutoDeActivate");
    }

    private void OnEnable()
    {
        effectEnergyBallCharge.transform.parent = energyBallSpawnTransform;
        effectEnergyBallCharge.SetActive(true);
    }

    [SerializeField]
    private Transform energyBallSpawnTransform = null;
    [SerializeField]
    private GameObject effectEnergyBallCharge = null;
    [SerializeField]
    private GameObject energyBallPrefab = null;
}
