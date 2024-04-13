using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFX : EntityFX
{
    [Header("Screen shake fx")]
    private CinemachineImpulseSource screenShake;
    [SerializeField] private float shakeMultiplier;
    public Vector3 shakeSwordImpact;
    public Vector3 shakeHighDamageImpact;

    [Header("Mirage image fx")]
    [SerializeField] private GameObject mirageImagePrefab;
    [SerializeField] private float colorLoseRate;
    [SerializeField] private float mirageImageCooldown;
    private float mirageImageCooldownTimer;


    [Space]
    [SerializeField] private ParticleSystem dustFx;


    protected override void Start()
    {
        base.Start();

        screenShake = GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        mirageImageCooldownTimer -= Time.deltaTime;
    }

    public void ScreenShake(Vector3 _shakePower)
    {
        screenShake.m_DefaultVelocity = new Vector3(_shakePower.x * player.facingDirection, _shakePower.y) * shakeMultiplier;
        screenShake.GenerateImpulse();
    }

    public void CreateMirageImage()
    {
        if (mirageImageCooldownTimer < 0)
        {
            mirageImageCooldownTimer = mirageImageCooldown;
            GameObject newMirageImage = Instantiate(mirageImagePrefab, transform.position, transform.rotation);
            newMirageImage.GetComponent<MirageImageFX>().SetupMirageImage(colorLoseRate, sr.sprite);
        }
    }

    public void PlayDustFX()
    {
        if (dustFx != null)
        {
            dustFx.Play();
        }
    }
}
