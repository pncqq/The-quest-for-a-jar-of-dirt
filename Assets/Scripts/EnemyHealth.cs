using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //Fields
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Rigidbody2D _playerRB;
    private Animator _animator;
    private int _currentHealth;
    private Collider2D _collider2D;
    private Rigidbody2D _rb;
    [SerializeField] private enemyPatrol patrol;

    //Animator fields
    private static readonly int IsHurt = Animator.StringToHash("isHurt");
    private static readonly int IsDead = Animator.StringToHash("isDead");

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
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
        Vector2 direction = new Vector2(150*(transform.position.x - _playerRB.transform.position.x), 75f);
        
        _rb.AddForce(direction);
        

        //If enemy dies
        if (_currentHealth <= 0)
        {
            //Die
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        //Die animation
        _animator.SetBool(IsDead, true);
        Destroy(patrol);
        _rb.AddForce(new Vector2(transform.position.x, 100f));
        //Disable enemy
        _collider2D.enabled = false;
        enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(transform.parent.gameObject);
    }
}