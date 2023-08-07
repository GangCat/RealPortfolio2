using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class AmmoEvent : UnityEngine.Events.UnityEvent<float> { }
public enum EWeaponState { None = -1, Idle, Attack, Reload }
public class WeaponAssaultRifle : MonoBehaviour
{
    [HideInInspector]
    public AmmoEvent onAmmoEvent = new AmmoEvent();
    public bool IsReload => isReload;
    public bool IsAttack => isAttack;
    public Vector3 HitPoint { get => hitPoint; set => hitPoint = value; }

    public void ChangeDmg(float _ratio, float _duration)
    {
        if (isBuff)
            StopCoroutine("ResetDmg");

        isBuff = true;
        float prevDmg = weaponSetting.dmg;

        weaponSetting.dmg = oriDmg * _ratio > weaponSetting.maxDmg ? weaponSetting.maxDmg : oriDmg * _ratio;

        if (prevDmg > weaponSetting.dmg)
            weaponSetting.dmg = prevDmg;

        StartCoroutine("ResetDmg", _duration);
    }

    private IEnumerator ResetDmg(float _duration)
    {
        yield return new WaitForSeconds(_duration);

        weaponSetting.dmg = oriDmg;
        isBuff = false;
    }

    public void ChangeState(EWeaponState _newState)
    {
        if (curState == _newState) return;

        if (isReload || playerAnim.CurAnimationIs("Dash")) return;

        if (isAttack)
        {
            isAttack = false;
        }

        StopCoroutine(curState.ToString());
        curState = _newState;
        StartCoroutine(curState.ToString());
    }

    private IEnumerator Idle()
    {
        yield return null;
    }

    private IEnumerator Attack()
    {
        playerAnim.SetBool("isRun", false);

        bool prevIsAttack = isAttack;

        while (true)
        {
            if (weaponSetting.curAmmo <= 0)
            {
                ChangeState(EWeaponState.Reload);
                yield break;
            }

            --weaponSetting.curAmmo;

            onAmmoEvent.Invoke(weaponSetting.curAmmo);

            playerAnim.PlayAttack();

            isAttack = true;
            if(prevIsAttack != isAttack)
                yield return null;

            StartCoroutine("OnMuzzleEffect");

            OnAttack();

            prevIsAttack = isAttack;

            yield return new WaitForSeconds(weaponSetting.attackRate);
        }
    }

    private IEnumerator OnMuzzleEffect()
    {
        EffectMuzzle.SetActive(true);

        yield return new WaitForSeconds(weaponSetting.attackRate * 0.8f);

        EffectMuzzle.SetActive(false);
    }

    private void OnAttack()
    {
        hitPoint.y = 1.0f;
        trMuzzleOfWeapon.rotation = Quaternion.LookRotation(hitPoint - trMuzzleOfWeapon.position);

        projectileMemoryPool.SpawnProjectile(trMuzzleOfWeapon.position, trMuzzleOfWeapon.rotation, weaponSetting.dmg);
    }

    private IEnumerator Reload()
    {
        playerAnim.SetBool("isRun", false);

        isReload = true;
        playerAnim.PlayReload();

        yield return new WaitForSeconds(0.2f);

        while (true)
        {
            if (!playerAnim.CurAnimationIs("Reload", 1))
                break;

            yield return null;
        }

        weaponSetting.curAmmo = weaponSetting.maxAmmo;
        onAmmoEvent.Invoke(weaponSetting.curAmmo);
        playerAnim.SetBool("isAmmoEmpty", false);
        Debug.Log("Ammo Refill");

        isReload = false;
        if (playerAnim.GetBool("isAttack"))
            ChangeState(EWeaponState.Attack);
        else
            ChangeState(EWeaponState.Idle);
    }

    private void Awake()
    {
        playerAnim = GetComponentInParent<PlayerAnimatorController>();
        projectileMemoryPool = GetComponent<ProjectileMemoryPool>();
    }

    private void Start()
    {
        weaponSetting.curAmmo = weaponSetting.maxAmmo;
        onAmmoEvent.Invoke(weaponSetting.curAmmo);
        EffectMuzzle.SetActive(false);
        oriDmg = weaponSetting.dmg;
    }

    private void OnEnable()
    {
        curState = EWeaponState.Idle;
    }

    private void OnDisable()
    {
        curState = EWeaponState.None;
    }



    [Header("-MuzzleEffect")]
    [SerializeField]
    private Transform trMuzzleOfWeapon;
    [SerializeField]
    private GameObject EffectMuzzle;

    [Header("-ETC")]
    [SerializeField]
    private LayerMask interactiveLayerMask;
    [SerializeField]
    private WeaponSetting weaponSetting;

    private bool isReload = false;
    private bool isAttack = false;
    private bool isBuff = false;

    private float oriDmg = 0;

    private PlayerAnimatorController playerAnim = null;
    private ProjectileMemoryPool projectileMemoryPool = null;
    private Vector3 hitPoint = Vector3.zero;

    private EWeaponState curState = EWeaponState.None;
}
