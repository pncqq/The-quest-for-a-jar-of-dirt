using Unity.VisualScripting;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public static HealthSystem Instance;
    public int maxHealth = 100;
    public int currentHealth;
    internal bool IsDead;
    internal bool IsHit;

    public HealthBar healthBar;
    [SerializeField] Rigidbody2D _playerRB;
    [SerializeField] private BasicPlayerMovement _playerMovement;


    private void Awake()
    {
        Instance = this;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        //Check if dead
        if (currentHealth <= 0)
            IsDead = true;

        //Nie zmniejszaj poniżej 0
        if (currentHealth < 0) currentHealth = 0;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        if (currentHealth <= 0) Destroy(_playerMovement);
    }


    public void TakeDamage(int damage, Rigidbody2D enemy)
    {
        //Update health
        currentHealth -= damage;
        //Update healthbar
        healthBar.SetHealth(currentHealth);
        //Update isHit field
        IsHit = true;

        //Knockback
        _playerRB.velocity = enemy.transform.position.x > transform.position.x
            ? new Vector2(-1f, 5f)
            : new Vector2(1f, 5f);
    }

    public void Heal(int healSize)
    {
        currentHealth += healSize;
        healthBar.SetHealth(currentHealth);
    }
}