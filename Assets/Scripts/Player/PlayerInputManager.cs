using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public bool IsInteract
    {
        get
        {
            if (isInteract && !isSelected)
            {
                isSelected = true;
                return true;
            }
            return false;
        }
    }

    public bool IsSellCrystal => isSellCrystal;

    private void Awake()
    {
        playerMove = GetComponent<PlayerMovementController>();
        playerAnim = GetComponent<PlayerAnimatorController>();
        weaponAR = GetComponentInChildren<WeaponAssaultRifle>();
    }

    private void Update()
    {
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
        isInteract = Input.GetButton("Interact");
        isSellCrystal = Input.GetButton("SellItem");
        if (!isInteract)
        {
            isSelected = false;
        }
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
    private bool isInteract = false;
    private bool isSellCrystal = false;
    private bool isSelected = false;

    private WeaponAssaultRifle weaponAR = null;
    private PlayerMovementController playerMove = null;
    private PlayerAnimatorController playerAnim = null;
}
