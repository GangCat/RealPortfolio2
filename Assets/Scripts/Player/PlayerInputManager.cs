using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{

    private void Awake()
    {
        playerMove = GetComponent<PlayerMovementController>();
        playerAnim = GetComponent<PlayerAnimatorController>();
        weaponAR = GetComponentInChildren<WeaponAssaultRifle>();
    }

    private void Update()
    {
        //Debug.DrawRay(transform.position, transform.forward * 1.5f, Color.red, 0.1f);
        UpdateInput();
        UpdateMove();
        UpdateDash();
        UpdateAttack();
        UpdateReload();
    }

    private void UpdateInput()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        isRun = Input.GetButton("Run");
    }

    private void UpdateMove()
    {
        if (!playerAnim.CurAnimationIs("Dash"))
        {
            bool isMove = playerAnim.ControllPlayerStateAnim(Input.GetButton("Horizontal"), Input.GetButton("Vertical"), isRun);

            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical") || Input.GetMouseButton(0))
                playerMove.RotateUpdate(x, z, Input.GetMouseButton(0));

            if (isMove)
                playerMove.MoveUpdate(x, z, isRun);
            else
                playerMove.MoveLerp(x, z, isRun);
        }
    }

    private void UpdateDash()
    {
        if (Input.GetButton("Dash") && !playerAnim.CurAnimationIs("Dash"))
        {
            if (playerMove.CanDash)
                playerMove.DashUpdate(x, z);
            else
                Debug.Log("DashCooltime!!");
        }
    }

    private void UpdateAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerAnim.SetBool("isAttack", true);
            weaponAR.ChangeState(EWeaponState.Attack);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            playerAnim.SetBool("isAttack", false);
            weaponAR.ChangeState(EWeaponState.Idle);
        }
    }

    private void UpdateReload()
    {
        if (Input.GetButtonDown("Reload"))
            weaponAR.ChangeState(EWeaponState.Reload);
    }

    private float x = 0.0f;
    private float z = 0.0f;
    private bool isRun = false;

    private WeaponAssaultRifle weaponAR = null;
    private PlayerMovementController playerMove = null;
    private PlayerAnimatorController playerAnim = null;
}
