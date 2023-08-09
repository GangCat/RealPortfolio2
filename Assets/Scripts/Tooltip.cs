using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public void ShowTooltip(string _itemInfo, string _itemStatus, Sprite _itemSprite, Vector2 _pos)
    {
        EditInfoText(_itemInfo);
        EditStatusText(_itemStatus);
        ChangeSprite(_itemSprite);
        SetPosition(_pos);

        gameObject.SetActive(true);
    }

    public void UpdateTooltipPos(Vector2 _pos)
    {
        SetPosition(_pos);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public void UpdateTooltipInfo(string _itemInfo, string _itemStatus, Sprite _itemSprite)
    {
        EditInfoText(_itemInfo);
        EditStatusText(_itemStatus);
        ChangeSprite(_itemSprite);
    }



    private void EditInfoText(string _textInfo)
    {
        textInfo.text = _textInfo;
    }

    private void EditStatusText(string _textStatus)
    {
        textStatus.text = _textStatus;
    }

    private void ChangeSprite(Sprite _sprite)
    {
        imageItem.sprite = _sprite;
    }

    private void SetPosition(Vector2 _pos)
    {
        rectTr.position = _pos;
    }

    private void Awake()
    {
        rectTr = GetComponent<RectTransform>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    [SerializeField]
    private TextMeshProUGUI textInfo = null;
    [SerializeField]
    private TextMeshProUGUI textStatus = null;
    [SerializeField]
    private Image imageItem = null;

    private RectTransform rectTr = null;
}
