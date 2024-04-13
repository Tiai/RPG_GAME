using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_SkillToolTip : UI_ToolTip
{
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillCost;

    [SerializeField] private TextMeshProUGUI skillDescription;
    [SerializeField] private float defaultDescriptionFontSize = 24;
    [SerializeField] private float fontSizeDescriptionMultiplier = .85f;

    public void ShowToolTip(string _skillDescription, string _skillName, int _price)
    {
        skillName.text = _skillName;
        skillDescription.text = _skillDescription;
        skillCost.text = "Cost : " + _price;

        AdjustPosition();

        AdjustFontSize(skillDescription, fontSizeDescriptionMultiplier);

        gameObject.SetActive(true);
    }

    public void HideToolTip()
    {
        skillDescription.fontSize = defaultDescriptionFontSize;
        gameObject.SetActive(false);
    } 
}
