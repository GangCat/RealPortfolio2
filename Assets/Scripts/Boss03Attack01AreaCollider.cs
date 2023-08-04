using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss03Attack01AreaCollider : BossNormalAttackAreaCollider
{
    public override void Setup(float _dmg)
    {
        dmg = _dmg;
        foreach (BossNormalAttackAreaCollider col in myColliders)
            col.Setup(dmg);
    }

    private void OnEnable()
    {
        myColliders[0].gameObject.SetActive(true);
        Invoke("ActivateSecondArea", 0.33f);
    }

    private void ActivateSecondArea()
    {
        myColliders[1].gameObject.SetActive(true);
    }

    public override void OnAttack()
    {
        if (isFirstCall)
        {
            myColliders[0].OnAttack();
            isFirstCall = false;
        }
        else
        {
            myColliders[1].OnAttack();
            isFirstCall = true;
        }
        
        StartCoroutine("AutoDeActivate");
    }

    protected override IEnumerator AutoDeActivate()
    {
        yield return new WaitForSeconds(DeactivateTime);

        gameObject.SetActive(false);
    }

    protected override void Awake()
    {
    }

    protected override void Start()
    {
    }


    private bool isFirstCall = true;

    [SerializeField]
    private BossNormalAttackAreaCollider[] myColliders;
}
