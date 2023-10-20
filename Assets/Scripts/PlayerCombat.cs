using System;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator _animator;
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //TODO: Attack if has sword in hand and 'F' is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack();
        }
    }

    private void Attack()
    {
        //Play attack anim
        _animator.SetTrigger(IsAttacking);

        //Detect enemies in range of attack

        //Damage enemies
    }
}