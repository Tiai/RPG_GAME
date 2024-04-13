using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator animator;
    public string id;
    public bool isActive;

    private void Awake()
    {
        if(id == null)
        {
            GenerateId();
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    [ContextMenu("Generate checkpoint id")]
    private void GenerateId()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      if(collision.GetComponent<Player>() != null)
        {
            ActivateCheckpoint();
        }
    }

    public void ActivateCheckpoint()
    {
        if(!isActive)
        {
            AudioManager.instance.PlaySFX(5, transform);
        }
        isActive = true;
        animator.SetBool("active", true);
    }
}
