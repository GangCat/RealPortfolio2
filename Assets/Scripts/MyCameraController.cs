using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour, IStageEngageObserver
{
    public void CheckStageEngage()
    {
        StartCoroutine("CameraMoveCoroutine");
        //transform.position += Vector3.forward * 60f;
    }

    private IEnumerator CameraMoveCoroutine()
    {
        float percent = 0;
        float curTime = Time.time;

        Vector3 camPos = transform.position;
        Vector3 nextCamPos = camPos;
        nextCamPos.z += 60f;

        while(percent < 1)
        {
            percent = (Time.time - curTime) * 5; // / 0.2
            transform.position = Vector3.Lerp(camPos, nextCamPos, percent);
            yield return null;
        }
    }

    private void Start()
    {
        GameManager.Instance.RegisterStageobserver(GetComponent<IStageEngageObserver>());
    }
}
