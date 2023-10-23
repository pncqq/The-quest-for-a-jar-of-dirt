using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //Fields
    private Animator _animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    [SerializeField] private int attackPower = 30;
    [SerializeField] private float attackRange = 0.5f;

    //Animator fields
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");
    private BasicPlayerMovement _basicPlayerMovement;

    private void Awake()
    {
        _basicPlayerMovement = GetComponent<BasicPlayerMovement>();
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
        //Attack anim
        _animator.SetTrigger(IsAttacking);
        
        //Detect enemies in range of attack
        var hitEnemies =
            Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

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