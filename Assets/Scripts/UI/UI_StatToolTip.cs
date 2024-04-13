using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_StatToolTip : UI_ToolTip
{
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private int defaultFontSize = 36;
    [SerializeField] private float fontSizeDescriptionMultiplier = .7f;

    public void ShowStatToolTip(string _text)
    {
        description.text = _text;

        AdjustPosition();

        AdjustFontSize(description, fontSizeDescriptionMultiplier);

        gameObject.SetActive(true);
    }

    public void HideStatToolTip()
    {
        description.text = "";

        description.fontSize = defaultFontSize;
        gameObject.SetActive(false);
    }
}
