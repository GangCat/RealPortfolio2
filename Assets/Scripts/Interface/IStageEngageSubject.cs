using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStageEngageSubject
{
    /// <summary>
    /// �������� ���� ������ ��� �޼ҵ�
    /// </summary>
    /// <param name="_observer"></param>
    void RegisterStageobserver(IStageEngageObserver _observer);

    /// <summary>
    /// �������� ���� ������ ���� �޼ҵ�
    /// </summary>
    /// <param name="_observer"></param>
    void RemoveStageObserver(IStageEngageObserver _observer);

    /// <summary>
    /// ���ŵ� �������� ���� ����
    /// </summary>
    void StageStart();
}
