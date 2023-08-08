using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECrystalCategory
{
    None = -1,
    Slot1,
    Slot2,
    Slot3,
    Slot4,
    Presize
}

public enum ECrystalColor
{
    Red = 0, Purple, Lilac,
    Lightblue, Blue, Darkblue,
    Yellow, Green, Emerald,
    Pink, Violet, LightViolet
}

public class CrystalManager : MonoBehaviour
{
    public GameObject EquipCrystal(ItemCrystal _crystal)
    {
        int prevIdx = imageCrystalSlots[(int)_crystal.crystalInfo.myCategory].PrevCrystalIdx;

        if (prevIdx == (int)_crystal.crystalInfo.myColor) // ³¢°íÀÖ´Â ³à¼®À» ´Ù½Ã ³¢·Á°í ÇÏ¸é µî¾÷
        {
            ++crystalPrefabs[prevIdx].GetComponent<ItemCrystal>().MyRank;
            if (crystalPrefabs[prevIdx].GetComponent<ItemCrystal>().MyRank > 3)
                crystalPrefabs[prevIdx].GetComponent<ItemCrystal>().MyRank = 3;

            SetStatus(_crystal.crystalInfo, crystalPrefabs[prevIdx].GetComponent<ItemCrystal>().MyRank);
            imageCrystalSlots[(int)_crystal.crystalInfo.myCategory].GetComponentInChildren<ImageCrystalRank>().SetRank(crystalPrefabs[prevIdx].GetComponent<ItemCrystal>().MyRank);
            return null;
        }
        else if (prevIdx < 12)
        {
            if (crystalPrefabs[prevIdx].GetComponent<ItemCrystal>().MyRank > 1) // ³¢°íÀÖ´Â ³à¼®ÀÌ 2µî±ÞÀÎµ¥ »õ·Î¿î ³à¼®À» ³¢·Á°í ÇÏ¸é
            {
                crystalPrefabs[prevIdx].GetComponent<ItemCrystal>().MyRank = 1; // µî±Þ 1·Î ÃÊ±âÈ­
                imageCrystalSlots[(int)_crystal.crystalInfo.myCategory].GetComponentInChildren<ImageCrystalRank>().SetRank(crystalPrefabs[prevIdx].GetComponent<ItemCrystal>().MyRank);
            }
        }

        SetStatus(_crystal.crystalInfo);
        imageCrystalSlots[(int)_crystal.crystalInfo.myCategory].ChangeCrystal((int)_crystal.crystalInfo.myColor);
        

        return prevIdx < 12 ? crystalPrefabs[prevIdx] : null;
    }

    private void SetStatus(SCrystalInfo _crystalInfo, int rank = 1)
    {
        switch (_crystalInfo.myCategory)
        {
            case ECrystalCategory.None:
                break;
            case ECrystalCategory.Slot1:
                weapon.ChangeDmg(_crystalInfo.increaseAttackDmg * rank);
                weapon.ChangeAttackRate(_crystalInfo.ratioAttackRate * rank);
                break;
            case ECrystalCategory.Slot2:
                player.GetComponent<StatusSkill>().ChangeSkillDmgs(_crystalInfo.increaseSkillDmg * rank);
                player.GetComponent<StatusSkill>().ChangeSkillCooltimes(_crystalInfo.ratioSkillRate * rank);
                break;
            case ECrystalCategory.Slot3:
                player.GetComponent<StatusHP>().ChangeMaxHp(_crystalInfo.increaseMaxHp * rank);
                player.GetComponent<StatusDefense>().ChangeDefense(_crystalInfo.increaseDefense * rank);
                player.GetComponent<StatusSpeed>().ChangeSpeed(_crystalInfo.ratioMoveSpeed * rank);
                break;
            case ECrystalCategory.Slot4:
                player.GetComponent<StatusDefense>().ChangeAttributeDefenses(_crystalInfo.increaseAttributeDefense * rank);
                weapon.ChangeAttributeDmgs(_crystalInfo.increaseAttributeDmg * rank);
                break;
            case ECrystalCategory.Presize:
                break;
        }
    }

    private void Awake()
    {
        weapon = player.GetComponentInChildren<WeaponAssaultRifle>();
    }

    [Header("-Crystal Slots & Rank")]
    [SerializeField]
    private ImageCrystalSlot[] imageCrystalSlots;

    [Header("-Player")]
    [SerializeField]
    private GameObject player;

    [Header("-Crystal Prefabs")]
    [SerializeField]
    private GameObject[] crystalPrefabs;

    private WeaponAssaultRifle weapon;
}
