using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clone_Skill : Skill //meanful for renaming this as Clone_Skill_Controllers p.87(https://www.bilibili.com/video/BV1cM4y1p7RF?p=87&vd_source=79b213604124ecc7b3fef6edda51766d)
{

    [Header("Clone info")]
    [SerializeField] private float attackMultiplier;
    [SerializeField] private GameObject clonePrefab;
    [SerializeField] private float cloneDuration;
    [Space]

    [Header("Clone attack")]
    [SerializeField] private UI_SkillTreeSlot cloneAttackUnlockButton;
    [SerializeField] private float cloneAttackMultiplier;
    [SerializeField] private bool cloneCanAttack;

    [Header("Aggressive clone")]
    [SerializeField] private UI_SkillTreeSlot cloneAggressiveAttackUnlockButton;
    [SerializeField] private float cloneAggressiveAttackMultiplier;
    public bool canApplyOnHitEffect { get; private set; }

    [Header("Multiple clone")]
    [SerializeField] private UI_SkillTreeSlot cloneMultipleUnlockButton;
    [SerializeField] private float cloneMultipleAttackMultiplier;
    [SerializeField] private bool cloneCanMultiple;
    [SerializeField] private float chanceToDuplicate;

    [Header("Crystal instead of clone")]
    [SerializeField] private UI_SkillTreeSlot cloneCrystalUnlockButton;
    public bool cloneCanCrystal;

    protected override void Start()
    {
        base.Start();

        cloneAttackUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneAttack);
        cloneAggressiveAttackUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneAggressiveAttack);
        cloneMultipleUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneMultipleAttack);
        cloneCrystalUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneCrystalAttack);
    }

    #region Unlock region

    protected override void CheckUnlock()
    {
        UnlockCloneAttack();
        UnlockCloneAggressiveAttack();
        UnlockCloneMultipleAttack();
        UnlockCloneCrystalAttack();

    }

    private void UnlockCloneAttack()
    {
        if(cloneAttackUnlockButton.unlocked)
        {
            cloneCanAttack = true;
            attackMultiplier = cloneAttackMultiplier;
        }
    }

    private void UnlockCloneAggressiveAttack()
    {
        if (cloneAggressiveAttackUnlockButton.unlocked)
        {
            canApplyOnHitEffect = true;
            attackMultiplier = cloneAggressiveAttackMultiplier;
        }
    }

    private void UnlockCloneMultipleAttack()
    {
        if(cloneMultipleUnlockButton.unlocked)
        {
            cloneCanMultiple = true;
            attackMultiplier = cloneMultipleAttackMultiplier;
        }
    }

    private void UnlockCloneCrystalAttack()
    {
        if (cloneCrystalUnlockButton.unlocked)
        {
            cloneCanCrystal = true;
        }
    }

    #endregion

    public void CreateClone(Transform _clonePosition, Vector3 _offset)
    {
        if (cloneCanCrystal)
        {
            SkillManager.instance.crystal.CreateCrystal();
            return;
        }

        GameObject newClone = Instantiate(clonePrefab);

        newClone.GetComponent<Clone_Skill_Controllers>().
            SetupClone(_clonePosition, cloneDuration, cloneCanAttack, _offset, FindClosestEnemy(newClone.transform), cloneCanMultiple, chanceToDuplicate, player, attackMultiplier);
    }

    public void CreateCloneWithDelay(Transform _enemyTransform)
    {
        StartCoroutine(CloneDelayCoroutine(_enemyTransform, new Vector3(2 * player.facingDirection, 0)));
    }

    private IEnumerator CloneDelayCoroutine(Transform _transform, Vector3 _offset)
    {
        yield return new WaitForSeconds(.4f);
        
        CreateClone(_transform, _offset);
    }
}
