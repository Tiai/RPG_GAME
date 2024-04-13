using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CraftSlot : UI_ItemSlot
{
    [SerializeField] private int defaultFontSize = 24;

    protected override void Start()
    {
        base.Start();
    }

    public void SetupCraftSlot(ItemData_Equipment _data)
    {
        if(_data == null )
        {
            return;
        }

        item.data = _data;

        itemImage.sprite = _data.icon;
        itemText.text = _data.itemName;

        if(itemText.text.Length > 10)
        {
            itemText.fontSize = itemText.fontSize * .7f;
        }
        else
        {
            itemText.fontSize = defaultFontSize;
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        ui.craftWindow.SetupCraftWindow(item.data as ItemData_Equipment);
    }
}
