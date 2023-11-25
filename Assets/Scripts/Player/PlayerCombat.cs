using UnityEngine;


public class PlayerCombat : MonoBehaviour
{
    //Fields
    [SerializeField] private float attackPower = 30;
    [SerializeField] private float attackRange = 0.5f;
    private float _lastAttackedAt;
    internal float StrengthBoost = 1;
    public Transform attackPoint;
    public Transform attackPointJumping;
    public LayerMask enemyLayers;
    public PlayerAnimationController playerAnimController;
    public float potionTime;


    private void Update()
    {
        // Reduce potion time by the time elapsed since the last frame
        if (potionTime > 0)
            potionTime -= Time.deltaTime;

        // Reset strength boost if potion effect has ended
        if (potionTime <= 0 && StrengthBoost > 1)
        {
            StrengthBoost = 1;
            potionTime = 0;
        }

        //Check if player has sword
        if (!Input.GetKeyDown(KeyCode.F) || !playerAnimController.IsSworded) return;
        //Check if player still making animation of last attack
        if (Time.time <= _lastAttackedAt + playerAnimController.AttackDelay) return;

        //Attack
        Attack();
        _lastAttackedAt = Time.time;
    }

    private void Attack()
    {
        //Detect enemies in range of attack. Hitbox z boku albo z dolu
        var enemy =
            Physics2D.OverlapCircle(
                !BasicPlayerMovement.IsGroundedVar
                    ? attackPointJumping.position
                    : attackPoint.position, attackRange,
                enemyLayers);


        //Damage enemies
        if (enemy == null || !enemy.gameObject.activeInHierarchy) return;
        var enemyHealth = enemy.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
            enemyHealth.TakeDamage(attackPower * StrengthBoost);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}