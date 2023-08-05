using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public void SetCollider(bool _boolean)
    {
        myCollider.enabled = _boolean;
    }

    public void TakeDmg(float _dmg)
    {
        if (!playerAnim.CurAnimationIs("Dash"))
        {
            if (statusHp.DecreaseHP(_dmg))
                Debug.Log("GameOver");
        }
    }

    public void IgnoreLayer(int _minLayerMask, int _maxLayerMask, bool _ignore)
    {
        Physics.IgnoreLayerCollision(_minLayerMask, _maxLayerMask, _ignore);
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Item"))
        {
            _other.GetComponent<ItemBase>().Use(gameObject);
        }
    }

    private void Awake()
    {
        myCollider = GetComponent<Collider>();
        statusHp = GetComponent<StatusHP>();
        playerAnim = GetComponent<PlayerAnimatorController>();
    }

    private Collider myCollider = null;
    private StatusHP statusHp = null;
    private PlayerAnimatorController playerAnim = null;
}
