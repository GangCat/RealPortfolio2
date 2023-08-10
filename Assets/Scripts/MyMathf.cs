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
}
