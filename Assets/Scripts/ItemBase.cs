using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    public abstract void Use(GameObject _entity);

    protected IEnumerator Start()
    {
        float y = transform.position.y;

        while (true)
        {
            transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);

            Vector3 pos = transform.position;
            pos.y = Mathf.Lerp(y, y + moveDistance, Mathf.PingPong(Time.time * pingpongSpeed, 1f));
            transform.position = pos;

            yield return null;
        }
    }

    [SerializeField]
    protected float moveDistance = 0.2f;
    [SerializeField]
    protected float pingpongSpeed = 0.5f;
    [SerializeField]
    protected float rotSpeed = 50f;
}