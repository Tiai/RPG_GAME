using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ToolTip : MonoBehaviour
{
    [SerializeField] private float xlimit = 960;
    [SerializeField] private float ylimit = 540;

    [SerializeField] private float xOffset = 150;
    [SerializeField] private float yOffset = 150;

    [SerializeField] private int textlength;

    public virtual void AdjustPosition()
    {
        Vector2 mousePosition = Input.mousePosition;

        float newXoffset = 0;
        float newYoffset = 0;

        if (mousePosition.x > xlimit)
        {
            newXoffset = -xOffset;
        }
        else
        {
            newXoffset = xOffset;
        }

        if (mousePosition.y > ylimit)
        {
            newYoffset = -yOffset;
        }
        else
        {
            newYoffset = yOffset;
        }

        transform.position = new Vector2(mousePosition.x + newXoffset, mousePosition.y + newYoffset);
    }

    public void AdjustFontSize(TextMeshProUGUI _text, float _fontSizeMultiplier)
    {
        if (_text.text.Length > textlength)
        {
            _text.fontSize = _text.fontSize * _fontSizeMultiplier;
        }
    }
}
