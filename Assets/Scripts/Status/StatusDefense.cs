using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseEvent : UnityEngine.Events.UnityEvent<float> { }
public class AttributeDefenseEvent : UnityEngine.Events.UnityEvent<float[]> { }
public class StatusDefense : MonoBehaviour
{
    public DefenseEvent onDefenseEvent = new DefenseEvent();
    public AttributeDefenseEvent onAttributeDefenseEvent = new AttributeDefenseEvent();

    public float CurDefense => curDefense;
    public float[] AttributeDefenses => attributeDefenses;


    public float DefenseDmg(float _dmg)
    {
        return _dmg -= curDefense > 0 ? curDefense : 0;
    }

    public void ChangeDefense(float _ratio, float _duration)
    {
        if (isDefenseBuff)
            StopCoroutine("ResetDefense");

        isDefenseBuff = true;

        curDefense = curDefense * _ratio > maxDefense ? maxDefense : curDefense * _ratio;

        onDefenseEvent.Invoke(curDefense);

        StartCoroutine("ResetDefense", _duration);
    }

    private IEnumerator ResetDefense(float _duration)
    {
        yield return new WaitForSeconds(_duration);

        curDefense = oriDefense;

        onDefenseEvent.Invoke(curDefense);
        isDefenseBuff = false;
    }

    public void ChangeAttributeDefenses(float _ratio, float _duration)
    {
        if (isAttributeDefenseBuff)
            StopCoroutine("ResetAttDefences");

        isAttributeDefenseBuff = true;

        for(int i = 0; i < attributeDefenses.Length; ++i)
            attributeDefenses[i] = attributeDefenses[i] * _ratio > maxAttributeDefense ? maxAttributeDefense : attributeDefenses[i] * _ratio;

        onAttributeDefenseEvent.Invoke(attributeDefenses);

        StartCoroutine("ResetAttDefences", _duration);
    }

    private IEnumerator ResetAttDefences(float _duration)
    {
        yield return new WaitForSeconds(_duration);

        for (int i = 0; i < attributeDefenses.Length; ++i)
            attributeDefenses[i] = oriAttributeDefense[i];

        onAttributeDefenseEvent.Invoke(attributeDefenses);

        isAttributeDefenseBuff = false;
    }

    private void Start()
    {
        oriDefense = curDefense;
        oriAttributeDefense = new float[attributeDefenses.Length];

        for (int i = 0; i < attributeDefenses.Length; ++i)
            oriAttributeDefense[i] = attributeDefenses[i];
    }

    [SerializeField]
    private float curDefense;
    [SerializeField]
    private float maxDefense;
    [SerializeField]
    private float[] attributeDefenses;
    [SerializeField]
    private float maxAttributeDefense;

    private bool isDefenseBuff = false;
    private bool isAttributeDefenseBuff = false;

    private float oriDefense;
    private float[] oriAttributeDefense;
}
