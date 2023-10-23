using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //Fields
    private Animator _animator;
    private Transform _attackPoint;
    public Transform attackPointRight;
    public Transform attackPointLeft;
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
        //Play attack anim
        _animator.SetTrigger(IsAttacking);

        //Check which attack point (left or right)
        _attackPoint = _basicPlayerMovement.isFacingRight ? attackPointRight : attackPointLeft;
        
        //Detect enemies in range of attack
        var hitEnemies =
            Physics2D.OverlapCircleAll(_attackPoint.position, attackRange, enemyLayers);

        //Damage enemies
        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackPower);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null) return;

        Gizmos.DrawWireSphere(_attackPoint.position, attackRange);
    }
}