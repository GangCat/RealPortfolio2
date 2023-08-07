using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            if (!isInvenOpen)
            {
                StopCoroutine("CloseInventory");
                StartCoroutine("OpenInventory");
            }
            else
            {
                StopCoroutine("OpenInventory");
                StartCoroutine("CloseInventory");
            }
        }
    }

    private IEnumerator OpenInventory()
    {
        isInvenOpen = true;
        float percent = 0.0f;
        float curTime = Time.time;
        while (percent < 1.0f)
        {
            percent = (Time.time - curTime) * 0.5f;

            equipPartsPanelRt.anchoredPosition = Vector2.Lerp(equipPartsPanelRt.anchoredPosition, new Vector3(0, equipPartsPanelRt.anchoredPosition.y), percent);
            statusPanelRt.anchoredPosition = Vector2.Lerp(statusPanelRt.anchoredPosition, new Vector2(0, statusPanelRt.anchoredPosition.y), percent);
            setEffectPanelRt.anchoredPosition = Vector2.Lerp(setEffectPanelRt.anchoredPosition, new Vector2(600, setEffectPanelRt.anchoredPosition.y), percent);

            yield return null;
        }

        equipPartsPanelRt.anchoredPosition = new Vector2(0, equipPartsPanelRt.anchoredPosition.y);
        statusPanelRt.anchoredPosition = new Vector2(0, statusPanelRt.anchoredPosition.y);
        setEffectPanelRt.anchoredPosition = new Vector2(600, setEffectPanelRt.anchoredPosition.y);
    }

    private IEnumerator CloseInventory()
    {
        isInvenOpen = false;
        float percent = 0.0f;
        float curTime = Time.time;
        while (percent < 1.0f)
        {
            percent = (Time.time - curTime) * 0.5f;

            equipPartsPanelRt.anchoredPosition = Vector2.Lerp(equipPartsPanelRt.anchoredPosition, new Vector3(-600, equipPartsPanelRt.anchoredPosition.y), percent);
            statusPanelRt.anchoredPosition = Vector2.Lerp(statusPanelRt.anchoredPosition, new Vector2(-600, statusPanelRt.anchoredPosition.y), percent);
            setEffectPanelRt.anchoredPosition = Vector2.Lerp(setEffectPanelRt.anchoredPosition, new Vector2(-300, setEffectPanelRt.anchoredPosition.y), percent);

            yield return null;
        }
        equipPartsPanelRt.anchoredPosition = new Vector3(-600, equipPartsPanelRt.anchoredPosition.y);
        statusPanelRt.anchoredPosition = new Vector2(-600, statusPanelRt.anchoredPosition.y);
        setEffectPanelRt.anchoredPosition = new Vector2(-300, setEffectPanelRt.anchoredPosition.y);
    }


    [Header("-Inventory Equip Item Slot")]
    [SerializeField]
    private Image imageMuzzleSlot;
    [SerializeField]
    private Image imageBarrelSlot;
    [SerializeField]
    private Image imageScopeSlot;
    [SerializeField]
    private Image imageMagazineSlot;

    [Header("-Status Text")]
    [SerializeField]
    private TextMeshProUGUI textMaxHp;
    [SerializeField]
    private TextMeshProUGUI textWeaponDmg;
    [SerializeField]
    private TextMeshProUGUI textSkillDmg;
    [SerializeField]
    private TextMeshProUGUI textAttackRate;
    [SerializeField]
    private TextMeshProUGUI textDefense;

    [SerializeField]
    private TextMeshProUGUI textMoveSpeed; // 걷기속도 / 달리기 속도
    [SerializeField]
    private TextMeshProUGUI textAttributeDmg;
    [SerializeField]
    private TextMeshProUGUI textAttributeDefense;
    [SerializeField]
    private TextMeshProUGUI textSkillCooltime;

    [Header("-Panel Transform")]
    [SerializeField]
    private RectTransform statusPanelRt;
    [SerializeField]
    private RectTransform equipPartsPanelRt;
    [SerializeField]
    private RectTransform setEffectPanelRt;


    private bool isInvenOpen = false;

}
