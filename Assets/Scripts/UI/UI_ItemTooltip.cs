using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ItemTooltip : UI_ToolTip
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private int defaultNameFontSize = 36;
    [SerializeField] private float fontSizeNameMultiplier = .65f;

    [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;


    public void ShowToolTip(ItemData_Equipment _item)
    {
        if(_item == null)
        {
            return;
        }

        itemNameText.text = _item.itemName;
        itemTypeText.text = _item.equipmentType.ToString();
        itemDescriptionText.text = _item.GetDescription();

        AdjustFontSize(itemNameText, fontSizeNameMultiplier);
        AdjustPosition();

        gameObject.SetActive(true);
    }

    public void HideToolTip()
    {
        itemNameText.fontSize = defaultNameFontSize;
        gameObject.SetActive(false);
    }
}
