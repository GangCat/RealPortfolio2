using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageCrystalRank : MonoBehaviour
{
    public void SetRank(int _rank)
    {
        image.sprite = spriteRanks[_rank - 1];
    }

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    [SerializeField]
    private Sprite[] spriteRanks;

    private Image image = null;
}
