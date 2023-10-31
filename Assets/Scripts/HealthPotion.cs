using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            Use();
    }

    private void Use()
    {
        HealthSystem.instance.Heal(50);
        _animator.SetBool("IsUsed", true);
    }
    private void DestroyMe()
    {
        Destroy(gameObject);
    }
}
