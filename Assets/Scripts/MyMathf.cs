using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMathf : MonoBehaviour
{
    public static float CalcAngleToTarget(Vector3 _oriPos, Vector3 _targetPos)
    {
        Vector3 oriPos = _oriPos;
        oriPos.y = 0f;
        Vector3 targetPos = _targetPos;
        targetPos.y = 0f;

        Vector3 dirToTarget = (targetPos - oriPos).normalized;
        return Mathf.Atan2(dirToTarget.z, dirToTarget.x) * Mathf.Rad2Deg;
    }

    public static float CalcAngleToTarget(Vector3 _dirVector)
    {
        return Mathf.Atan2(_dirVector.z, _dirVector.x) * Mathf.Rad2Deg;
    }

    public static void SetRotation(Transform _tr, Vector3 _euler)
    {
        _tr.rotation = Quaternion.Euler(_euler);
    }

    public static void RotateYaw(Transform _tr, float _angle)
    {
        _tr.rotation = Quaternion.Euler(0f, -_angle + 90f, 0f);
    }

    //public static void RotatePitch(Transform _tr, float _angle)
    //{
    //    _tr.rotation = Quaternion.Euler(0f, -_angle + 90f, 0f);
    //}

    //public static void RotateRoll(Transform _tr, float _angle)
    //{
    //    _tr.rotation = Quaternion.Euler(0f, -_angle + 90f, 0f);
    //}


}
