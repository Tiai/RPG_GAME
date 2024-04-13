using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirageImageFX : MonoBehaviour
{
    private SpriteRenderer sr;
    private float colorLoseRate;

    public void SetupMirageImage(float _losingSpeed, Sprite _spriteImage)
    {
        sr = GetComponent<SpriteRenderer>();

        sr.sprite = _spriteImage;

        colorLoseRate = _losingSpeed;
    }

    private void Update()
    {
        float alpha = sr.color.a - colorLoseRate * Time.deltaTime;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
        
        if(sr.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
