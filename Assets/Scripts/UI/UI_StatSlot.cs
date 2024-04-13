using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_StatSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private UI ui;

    [SerializeField] private string statName;
    [SerializeField] private StatType statType;
    [SerializeField] private TextMeshProUGUI statValueText;
    [SerializeField] private TextMeshProUGUI statNameText;

    [TextArea]
    [SerializeField] private string statDescription;

    private void OnValidate()
    {
        gameObject.name = "Stat - " + statName;

        if (statNameText != null)
        {
            statNameText.text = statName;
        }
    }

    void Start()
    {
        UpdateStatValueUI();

        ui = GetComponentInParent<UI>();
    }

    public void UpdateStatValueUI()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        if (playerStats != null)
        {
            statValueText.text = playerStats.GetStat(statType).GetValue().ToString();

            switch (statType)
            {
                case StatType.maxHealth:
                    statValueText.text = playerStats.GetMaxHealthValue().ToString();
                    break;
                case StatType.damage:
                    statValueText.text = (playerStats.damage.GetValue() + playerStats.strength.GetValue()).ToString();
                    break;
                case StatType.critPower:
                    statValueText.text = (playerStats.critPower.GetValue() + playerStats.strength.GetValue()).ToString();
                    break;
                case StatType.critChance:
                    statValueText.text = (playerStats.critChance.GetValue() + playerStats.agility.GetValue()).ToString();
                    break;
                case StatType.evasion:
                    statValueText.text = (playerStats.evasion.GetValue() + playerStats.agility.GetValue()).ToString();
                    break;
                case StatType.magicResistance:
                    statValueText.text = (playerStats.magicResistance.GetValue() + (playerStats.intelligence.GetValue() * 3)).ToString();
                    break;
                default:
                    break;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.statToolTip.ShowStatToolTip(statDescription);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.statToolTip.HideStatToolTip();
    }
}
