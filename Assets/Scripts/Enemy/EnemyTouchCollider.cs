using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTouchCollider : MonoBehaviour
{
    public void SetLayer(int _layerIdx)
    {
        gameObject.layer = _layerIdx;
    }
}
