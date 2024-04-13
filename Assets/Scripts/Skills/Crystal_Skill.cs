using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crystal_Skill : Skill
{
    [SerializeField] private float crystalDuration;
    [SerializeField] private GameObject crystalPrefab;
    private GameObject currentCrystal;

    [Header("Crystal simple")]
    [SerializeField] private UI_SkillTreeSlot unlockCrystalButton;
    public bool crystalUnlocked {  get; private set; }

    [Header("Crystal mirage")]
    [SerializeField] private UI_SkillTreeSlot unlockMirageCrystalButton;
    [SerializeField] private bool crystalCanMirage;

    [Header("Explosive crystal")]
    [SerializeField] private UI_SkillTreeSlot unlockExplosiveCrystalButton;
    [SerializeField] private bool crystaslCanExplode;

    [Header("Moving crystal")]
    [SerializeField] private UI_SkillTreeSlot unlockMovingCrystalButton;
    [SerializeField] private bool crystalCanMove;
    [SerializeField] private float moveSpeed;

    [Header("Multi stacking crystal")]
    [SerializeField] private UI_SkillTreeSlot unlockMultiCrystalButton;
    [SerializeField] private bool crystalCanMulti;
    [SerializeField] private int amountOfStacks;
    [SerializeField] private float multistackCooldown;
    [SerializeField] private float useTimeWindow;
    [SerializeField] private List<GameObject> crystalLeft = new List<GameObject>();

    protected override void Start()
    {
        base.Start();

        unlockCrystalButton.GetComponent<Button>().onClick.AddListener(UnlockCrystal);
        unlockMirageCrystalButton.GetComponent<Button>().onClick.AddListener(UnlockMirageCrystal);
        unlockExplosiveCrystalButton.GetComponent<Button>().onClick.AddListener(UnlockExplosiveCrystal);
        unlockMovingCrystalButton.GetComponent<Button>().onClick.AddListener(UnlockMovingCrystal);
        unlockMultiCrystalButton.GetComponent<Button>().onClick.AddListener(UnlockMultiCrystal);
    }

    //unlock crystal skill
    #region Unlock crystal skill region

    protected override void CheckUnlock()
    {
        UnlockCrystal();
        UnlockMirageCrystal();
        UnlockExplosiveCrystal();
        UnlockMovingCrystal();
        UnlockMultiCrystal();
    }
    private void UnlockCrystal()
    {
        if (unlockCrystalButton.unlocked)
        {
            crystalUnlocked = true;
        }
    }

    private void UnlockMirageCrystal()
    {
        if (unlockMirageCrystalButton.unlocked)
        {
            crystalCanMirage = true;
        }
    }

    private void UnlockExplosiveCrystal()
    {
        if (unlockExplosiveCrystalButton.unlocked)
        {
            crystaslCanExplode = true;
        }
    }

    private void UnlockMovingCrystal()
    {
        if (unlockMovingCrystalButton.unlocked)
        {
            crystalCanMove = true;
        }
    }


    private void UnlockMultiCrystal()
    {
        if (unlockMultiCrystalButton.unlocked)
        {
            crystalCanMulti = true;
        }
    }
    #endregion

    public override void UseSkill()
    {
        base.UseSkill();

        if (canUseMultiCrystal())
        {
            return;
        }

        if(currentCrystal == null)
        {
            CreateCrystal();
        }
        else
        {
            if (crystalCanMove)
            {
                return;
            }

            Vector2 playerPos = player.transform.position;
            player.transform.position = currentCrystal.transform.position;
            currentCrystal.transform.position = playerPos;

            if (crystalCanMirage)
            {
                SkillManager.instance.clone.CreateClone(currentCrystal.transform, Vector3.zero);
                Destroy(currentCrystal);
            }
            else
            {
                currentCrystal.GetComponent<Crystal_Skill_Controller>()?.FinishCrystal();
            }
        }
    }

    public void CreateCrystal()
    {
        currentCrystal = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity);
        Crystal_Skill_Controller currentCrystalScript = currentCrystal.GetComponent<Crystal_Skill_Controller>();

        currentCrystalScript.SetupCrystal(crystalDuration, crystaslCanExplode, crystalCanMove, moveSpeed, FindClosestEnemy(currentCrystal.transform), player);
    }

    public void CurrentCrystalChooseRandomTarget() => currentCrystal.GetComponent<Crystal_Skill_Controller>().ChooseRandomEnemy();

    private bool canUseMultiCrystal()
    {
        if(crystalCanMulti)
        {
            if(crystalLeft.Count > 0)
            {
                if(crystalLeft.Count == amountOfStacks)
                {
                    Invoke("ResetAbility", useTimeWindow);
                }

                cooldown = 0;
                GameObject crystalToSpawn = crystalLeft[crystalLeft.Count - 1];
                GameObject newCrystal = Instantiate(crystalToSpawn, player.transform.position, Quaternion.identity);

                crystalLeft.Remove(crystalToSpawn);

                newCrystal.GetComponent<Crystal_Skill_Controller>().
                    SetupCrystal(crystalDuration, crystaslCanExplode, crystalCanMove, moveSpeed, FindClosestEnemy(newCrystal.transform), player);

                if(crystalLeft.Count <= 0)
                {
                    cooldown = multistackCooldown;
                    RefilCrystal();

                }
            
                return true;
            }

        }

        return false;
    }

    private void RefilCrystal()
    {
        int amountToAdd = amountOfStacks - crystalLeft.Count;

        for(int i=0; i < amountToAdd; ++i)
        {
            crystalLeft.Add(crystalPrefab);
        }
    }

    private void ResetAbility()
    {
        if(cooldownTimer > 0)
        {
            return;
        }

        cooldownTimer = multistackCooldown;
        RefilCrystal();
    }
}
