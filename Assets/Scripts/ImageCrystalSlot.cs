using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageCrystalSlot : MonoBehaviour
{
    public int PrevCrystalIdx => prevCrystalIdx;

    public void ChangeCrystal(int _crystalIdx)
    {
        image.sprite = crystalSprites[_crystalIdx];
        prevCrystalIdx = _crystalIdx;
    }

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    [SerializeField]
    private Sprite[] crystalSprites;


    private Image image = null;
    private int prevCrystalIdx = 12;
}
