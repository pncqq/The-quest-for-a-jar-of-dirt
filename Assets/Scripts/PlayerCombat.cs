using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCombat : MonoBehaviour
{
    //Fields
    public Transform attackPoint;
    public Transform attackPointJumping;
    public LayerMask enemyLayers;
    public PlayerAnimationController playerAnimController;
    private double _lastAttackedAt;
    [SerializeField] private int attackPower = 30;
    [SerializeField] private float attackRange = 0.5f;


    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.F) || !playerAnimController.IsSworded) return;

        if (Time.time <= _lastAttackedAt + playerAnimController.AttackDelay) return;

        Attack();
        _lastAttackedAt = Time.time;
    }

    private void Attack()
    {
        Collider2D[] hitEnemies;

        //Detect enemies in range of attack. Hitbox z boku albo z dolu
        hitEnemies =
            Physics2D.OverlapCircleAll(
                !BasicPlayerMovement.IsGroundedVar ? attackPointJumping.position : attackPoint.position, attackRange,
                enemyLayers);


        //Damage enemies
        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackPower);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}