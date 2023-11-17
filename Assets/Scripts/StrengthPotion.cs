using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthPotion : MonoBehaviour
{
    public PlayerCombat pc;
    private Animator _animator;
    private static readonly int IsUsed = Animator.StringToHash("IsUsed");

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
        //Boost strength
        pc.StrengthBoost = 1.3f;
        _animator.SetBool(IsUsed, true);
    }
    
    private void DestroyMe()
    {
        Destroy(gameObject);
    }
}
