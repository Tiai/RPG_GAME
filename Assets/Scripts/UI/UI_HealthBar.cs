using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    private Entity entity => GetComponentInParent<Entity>();
    private CharacterStats myStats => GetComponentInParent<CharacterStats>();
    
    private RectTransform myTransform;
    private Slider hpSlider;


    private void Start()
    {
        //entity = GetComponentInParent<Entity>();
        //myStats = GetComponentInParent<CharacterStats>();
        
        myTransform = GetComponent<RectTransform>();
        hpSlider = GetComponentInChildren<Slider>();

        UpdateHealthUI();
    }


    private void UpdateHealthUI()
    {
        hpSlider.maxValue = myStats.GetMaxHealthValue();
        hpSlider.value = myStats.currentHealth;
    }

    private void OnEnable()
    {
        entity.onFlipped += FlipUI; //add to event
        myStats.onHealthChanged += UpdateHealthUI;
    }

    private void OnDisable()
    {
        if(entity != null)
        {
            entity.onFlipped -= FlipUI;
        }

        if(myStats != null)
        {
            myStats.onHealthChanged -= UpdateHealthUI;
        }
    }
    private void FlipUI() => myTransform.Rotate(0, 180, 0);
}
