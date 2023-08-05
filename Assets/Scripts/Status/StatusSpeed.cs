using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusSpeed : MonoBehaviour
{
    public float RunSpeed => runSpeed;
    public float WalkSpeed => walkSpeed;

    public void ChangeSpeed(float _ratio, float _duration)
    {
        if (isBuff)
            StopCoroutine("ResetSpeed");

        isBuff = true;
        float prevWalkSpeed = walkSpeed;
        float prevRunSpeed = runSpeed;

        walkSpeed = oriWalkSpeed * _ratio > maxWalkSpeed ? maxWalkSpeed : oriWalkSpeed * _ratio;
        runSpeed = oriRunSpeed * _ratio > maxRunSpeed ? maxRunSpeed : oriRunSpeed * _ratio;

        if (prevWalkSpeed > walkSpeed)
            walkSpeed = prevWalkSpeed;
        if (prevRunSpeed > runSpeed)
            runSpeed = prevRunSpeed;

        StartCoroutine("ResetSpeed", _duration);
    }

    private IEnumerator ResetSpeed(float _duration)
    {
        yield return new WaitForSeconds(_duration);

        runSpeed = oriRunSpeed;
        walkSpeed = oriWalkSpeed;
        isBuff = false;
    }

    private void Start()
    {
        oriWalkSpeed = walkSpeed;
        oriRunSpeed = runSpeed;
    }


    [Header("-Movement Speed")]
    [SerializeField]
    private float runSpeed = 10.0f;
    [SerializeField]
    private float walkSpeed = 6.0f;
    [SerializeField]
    private float maxRunSpeed = 25.0f;
    [SerializeField]
    private float maxWalkSpeed = 15.0f;

    private bool isBuff = false;

    private float oriWalkSpeed;
    private float oriRunSpeed;
}
