using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour
{   
    private void Awake()
    {
        Cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        transform.position = playerTr.position + offset;
    }

    [SerializeField]
    private Vector3     offset = Vector3.zero;
    [SerializeField]
    private Transform   playerTr = null;

    private Camera      Cam = null; // 나중에 카메라 흔들림 넣을 때 쓸거심
}
