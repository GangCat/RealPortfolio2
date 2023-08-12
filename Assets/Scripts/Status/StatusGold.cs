using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusGold : MonoBehaviour
{
    public OnGoldChangeDelegate OnGoldChangeCallback
    {
        set => onGoldChangeCallback = value;
    }

    public void IncreaseGold(int _gold)
    {
        curGold += _gold;

        if (curGold > maxGold)
        {
            onGoldChangeCallback?.Invoke(_gold - (curGold - maxGold));
            curGold = maxGold;
        }
        else
            onGoldChangeCallback?.Invoke(_gold);
    }

    /// <summary>
    /// �������� �Ű�������ŭ ��µ� �������� �����ϸ� false ��ȯ
    /// </summary>
    /// <param name="_gold"></param>
    /// <returns></returns>
    public bool DecreaseGold(int _gold)
    {
        if (curGold < _gold)
            return false;
        else
        {
            curGold -= _gold;
            return true;
        }
    }

    private void OnEnable()
    {
        curGold = 1000;
    }

    private void Start()
    {
        Invoke("SetupGold", 0.5f);
    }

    private void SetupGold()
    {
        onGoldChangeCallback?.Invoke(curGold);
    }

    [SerializeField]
    private int curGold = 100;
    [SerializeField]
    private int maxGold = 99999;

    private OnGoldChangeDelegate onGoldChangeCallback = null;
}
