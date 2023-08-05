using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusGold : MonoBehaviour
{
    public int MaxGold => maxGold;
    public int CurGold
    {
        get => curGold;
        set => curGold = value;
    }

    public void IncreaseGold(int _gold)
    {
        curGold = curGold + _gold >= maxGold ? maxGold : curGold + _gold;
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
        curGold = 100;
    }

    [SerializeField]
    private int curGold = 100;
    [SerializeField]
    private int maxGold = 99999;

}
