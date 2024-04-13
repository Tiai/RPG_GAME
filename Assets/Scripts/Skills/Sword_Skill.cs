using System;
using UnityEngine;
using UnityEngine.UI;

public enum SwordType
{
    Regular,
    Bounce,
    Pierce,
    Spin
}

public class Sword_Skill : Skill
{
    public SwordType swordType = SwordType.Regular;

    [Header("Bounce info")]
    [SerializeField] private UI_SkillTreeSlot swordBounceUnlockButton;
    [SerializeField] private int bounceAmount;
    [SerializeField] private float bounceGravity;
    [SerializeField] private float bounceSpeed;

    [Header("Pierce info")]
    [SerializeField] private UI_SkillTreeSlot swordPierceUnlockButton;
    [SerializeField] private int pierceAmount;
    [SerializeField] private float pierceGravity;


    [Header("Spin info")]
    [SerializeField] private UI_SkillTreeSlot swordSpinUnlockButton;
    [SerializeField] private float hitCooldown = .35f;
    [SerializeField] private float maxTravelDistance = 7;
    [SerializeField] private float spinDuration = 2;
    [SerializeField] private float spinGravity = 1;

    [Header("Skill info")]
    [SerializeField] private UI_SkillTreeSlot swordUnlockButton;
    public bool swordUnlocked { get; private set; }
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2 launchForce;
    [SerializeField] private float swordGravity;
    [SerializeField] private float freezeTimeDuration;
    [SerializeField] private float returnSpeed;

    [Header("Passive skills")]
    [SerializeField] private UI_SkillTreeSlot timeStopUnlockButton;
    public bool timeStopUnlocked {  get; private set; }
    [SerializeField] private UI_SkillTreeSlot vulnerabilityUnlockButton;
    public bool vulnerabilityUnlocked {  get; private set; }

    private Vector2 finalDirection;

    [Header("Aim dots")]
    [SerializeField] private int numberOfDots;
    [SerializeField] private float spaceBetweenDots;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotsParent;

    private GameObject[] dots;

    protected override void Start()
    {
        base.Start();

        GenerateDots();

        SetupGravity();

        swordUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockSword);
        swordBounceUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockSwordBounce);
        swordPierceUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockSwordPierce);
        swordSpinUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockSwordSpin);
        timeStopUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockTimeStop);
        vulnerabilityUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockVulnerability);
    }

    private void SetupGravity()
    {
        switch(swordType)
        {
            case SwordType.Regular:
                break;
            case SwordType.Bounce:
                swordGravity = bounceGravity;
                break;
            case SwordType.Pierce:
                swordGravity = pierceGravity;
                break;
            case SwordType.Spin:
                swordGravity = spinGravity;
                break;
            default: break;
        }
    }

    protected override void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            finalDirection = new Vector2(AimDirection().normalized.x * launchForce.x, AimDirection().normalized.y * launchForce.y);
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            for (int i = 0; i < dots.Length; ++i)
            {
                dots[i].transform.position = DotsPosition(i * spaceBetweenDots);
            }
        }
    }
    public void CreateSword()
    {
        GameObject newSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
        Sword_Skill_Controller newSwordScript = newSword.GetComponent<Sword_Skill_Controller>();

        switch (swordType)
        {
            case SwordType.Regular:
                break;
            case SwordType.Bounce:
                newSwordScript.SetupBounce(true, bounceAmount, bounceSpeed);
                break;
            case SwordType.Pierce:
                newSwordScript.SetupPierce(pierceAmount);
                break;
            case SwordType.Spin:
                newSwordScript.SetupSpin(true, maxTravelDistance, spinDuration, hitCooldown);
                break;
            default: break;
        }

        newSwordScript.SetupSword(finalDirection, swordGravity, player, freezeTimeDuration, returnSpeed);

        player.AssignNewSword(newSword);

        DotsActive(false);
    }

    #region Unlock region

    protected override void CheckUnlock()
    {
        UnlockSword();
        UnlockSwordBounce();
        UnlockSwordPierce();
        UnlockSwordSpin();
        UnlockTimeStop();
        UnlockVulnerability();
    }

    private void UnlockTimeStop()
    {
        if (timeStopUnlockButton.unlocked)
        {
            timeStopUnlocked = true;
        }
    }

    private void UnlockVulnerability()
    {
        if(vulnerabilityUnlockButton.unlocked)
        {
            vulnerabilityUnlocked = true;
        }
    }


    private void UnlockSword()
    {
        if(swordUnlockButton.unlocked)
        {
            swordType = SwordType.Regular;
            swordUnlocked = true;
        }
    }

    private void UnlockSwordBounce()
    {
        if (swordBounceUnlockButton.unlocked)
        {
            swordType = SwordType.Bounce;
        }
    }

    private void UnlockSwordPierce()
    {
        if (swordPierceUnlockButton.unlocked)
        {
            swordType = SwordType.Pierce;
        }
    }

    private void UnlockSwordSpin()
    {
        if (swordSpinUnlockButton.unlocked)
        {
            swordType = SwordType.Spin;
        }
    }

    #endregion

    #region Aim region
    public Vector2 AimDirection()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - playerPosition;

        return direction;
    }

    public void DotsActive(bool _isActive)
    {
        for (int i = 0; i < dots.Length; ++i)
        {
            dots[i].SetActive(_isActive);
        }
    }

    private void GenerateDots()
    {
        dots = new GameObject[numberOfDots];
        for (int i = 0; i < numberOfDots; ++i)
        {
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotsParent);
            dots[i].SetActive(false);
        }
    }

    private Vector2 DotsPosition(float _t)
    {
        Vector2 position = (Vector2)player.transform.position + new Vector2(
            AimDirection().normalized.x * launchForce.x,
            AimDirection().normalized.y * launchForce.y) * _t + .5f * (Physics2D.gravity * swordGravity) * (_t * _t);

        return position;
    }
    #endregion
}
