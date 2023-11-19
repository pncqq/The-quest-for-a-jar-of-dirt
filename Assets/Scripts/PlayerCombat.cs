using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //Fields
    public Transform attackPoint;
    public Transform attackPointJumping;
    public LayerMask enemyLayers;
    public PlayerAnimationController playerAnimController;
    private float _lastAttackedAt;
    [SerializeField] private float attackPower = 30;
    internal float StrengthBoost = 1;
    [SerializeField] private float attackRange = 0.5f;
    public float _lockedTill;
    public float potionTime;


    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.F) || !playerAnimController.IsSworded) return;

        if (Time.time <= _lastAttackedAt + playerAnimController.AttackDelay) return;
        potionTime -= Time.time;
        if(potionTime <= 0)
            StrengthBoost = 1;
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
            enemy.GetComponent<EnemyHealth>().TakeDamage((float)(attackPower * LockState(StrengthBoost, 15f)));
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