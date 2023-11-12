using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //Fields
    public Transform attackPoint;
    public Transform attackPointJumping;
    public LayerMask enemyLayers;
    public PlayerAnimationController playerAnimController;
    private double _lastAttackedAt;
    [SerializeField] private double attackPower = 30;
    internal double StrengthBoost = 1;
    [SerializeField] private float attackRange = 0.5f;
    private float _lockedTill;


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
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackPower * LockState(StrengthBoost, 15f));
        }

        //Change StrengthBoost to 1 after time
        if (Time.time > _lockedTill)
        {
            StrengthBoost = 1;
        }


        //Locking method
        double LockState(double s, float t)
        {
            _lockedTill = Time.time + t;
            return s;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}