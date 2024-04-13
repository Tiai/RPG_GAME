using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Controller : MonoBehaviour
{
    private Animator Animator;

    private CharacterStats myStats;
    private float growSpeed = 15;
    private float maxSize = 6;
    private float explosionRadius;

    private bool canGrow = true;

    private void Update()
    {
        if (canGrow)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);
        }

        if(maxSize - transform.localScale.x < .5f)
        {
            canGrow = false;
            Animator.SetTrigger("Explode");
        }
    }

    public void SetupExplosion(CharacterStats _mystats, float _growSpeed, float _maxSize, float _radius)
    {
        Animator = GetComponent<Animator>();

        myStats = _mystats;
        growSpeed = _growSpeed;
        maxSize = _maxSize;
        explosionRadius = _radius;
    }

    private void AnimationExplodeEvent()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<CharacterStats>() != null)
            {
                hit.GetComponent<Entity>().SetupKnockbackDirection(transform);

                myStats.DoDamage(hit.GetComponent<CharacterStats>());
            }
        }
    }

    private void SelfDestory() => Destroy(gameObject);
}
