using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelPauseMenu : MonoBehaviour
{
    public void ShowPauseMenu()
    {
        StopCoroutine("PauseMenuAnimation");
        StartCoroutine("PauseMenuAnimation", true);
    }

    public void ClosePauseMenu()
    {
        StopCoroutine("PauseMenuAnimation");
        StartCoroutine("PauseMenuAnimation", false);
    }

    private IEnumerator PauseMenuAnimation(bool _isOpen)
    {
        float percent = 0f;
        float curTime = Time.time;

        while(percent < 1f)
        {
            float scaleY = Mathf.Lerp(rectTr.localScale.y, _isOpen == true ? 1 : 0, percent);
            rectTr.localScale = new Vector2(1, scaleY);
            percent = (Time.time - curTime) / 0.5f ;
            yield return null;
        }
    }

    private void Awake()
    {
        rectTr = GetComponent<RectTransform>();
    }

    private void Start()
    {
        rectTr.localScale = new Vector2(1f, 0f);
    }

    private RectTransform rectTr = null;
}
