using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillTreeSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISaveManager
{
    private UI ui;
    private Image skillImage;

    [SerializeField] private int skillCost;
    [SerializeField] private string skillName;
    [TextArea]
    [SerializeField] private string skillDescription;
    [SerializeField] private Color lockedSkillColor;

    public bool unlocked;

    [SerializeField] private UI_SkillTreeSlot[] shouldBeUnlocked;
    [SerializeField] private UI_SkillTreeSlot[] shouldBeLocked;


    private void OnValidate()
    {
        gameObject.name = "SkillTreeSlot_UI - " + skillName;
    }

    public void AssignEvents(UI uiInstance)
    {
        ui = uiInstance;
        GetComponent<Button>().onClick.AddListener(() => UnlockSkillSlot());
    }

    private void Awake()
    {
        AssignEvents(ui);
    }

    private void Start()
    {
        skillImage = GetComponent<Image>();

        GameData loadedGameData = SaveManager.instance.LoadGame();

        LoadData(loadedGameData);

        ui = GetComponentInParent<UI>();
    }

    public void UnlockSkillSlot()
    {
        if (PlayerManager.instance.HaveEnoughMoney(skillCost) == false)
        {
            return;
        }

        for (int i = 0; i < shouldBeUnlocked.Length; ++i)
        {
            if (shouldBeUnlocked[i].unlocked == false)
            {
                Debug.Log("Can't not unlock skill");
                return;
            }
        }

        for (int i = 0; i < shouldBeLocked.Length; ++i)
        {
            if (shouldBeLocked[i].unlocked == true)
            {
                Debug.Log("Can't not unlock skill");
                return;
            }
        }

        unlocked = true;
        SaveManager.instance.SaveGame();
        UpdateSkillVisuals();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.skillToolTip.ShowToolTip(skillDescription, skillName, skillCost);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.skillToolTip.HideToolTip();
    }

    //public void LoadData(GameData _data)
    //{
    //    if (_data.skillTree.TryGetValue(skillName, out bool value))
    //    {
    //        unlocked = value;
    //    }
    //}

    public void LoadData(GameData _data)
    {
        if (_data.skillTree.TryGetValue(skillName, out bool value))
        {
            unlocked = value;
            UpdateSkillVisuals();
        }
        else
        {
            unlocked = false;
            UpdateSkillVisuals();
        }
    }

    private void UpdateSkillVisuals()
    {
        if (skillImage != null)
        {
            if (unlocked)
            {
                skillImage.color = Color.white;
            }
            else
            {
                skillImage.color = lockedSkillColor;
            }
        }
    }

    public void SaveData(ref GameData _data)
    {
        if (_data.skillTree.ContainsKey(skillName))
        {
            _data.skillTree[skillName] = unlocked;
        }
        else
        {
            _data.skillTree.Add(skillName, unlocked);
        }
    }
}