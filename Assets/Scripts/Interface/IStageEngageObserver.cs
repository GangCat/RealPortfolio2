using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStageEngageObserver
{
    /// <summary>
    /// ��ü�κ��� ������Ʈ�� �޴� �޼ҵ�
    /// </summary>
    /// <param name="_curStage"></param>
    void CheckStageEngage();
}
