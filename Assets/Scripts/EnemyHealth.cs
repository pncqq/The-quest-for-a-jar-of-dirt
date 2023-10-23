using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //Fields
    [SerializeField] private int maxHealth = 100;
    private Animator _animator;
    private int _currentHealth;
    private Collider2D _collider2D;

    //Animator fields
    private static readonly int IsHurt = Animator.StringToHash("isHurt");
    private static readonly int IsDead = Animator.StringToHash("isDead");

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _currentHealth = maxHealth;
    }

    //Taking damage
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _animator.SetTrigger(IsHurt);

        //If enemy dies
        if (_currentHealth <= 0)
        {
            //Die
            Die();
        }
    }

    private void Die()
    {
        //Die animation
        _animator.SetBool(IsDead, true);

        //Disable enemy
        _collider2D.enabled = false;
        enabled = false;
        
    }
}