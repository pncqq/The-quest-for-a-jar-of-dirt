using System;
using UnityEngine;

public class SpikesScript : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField] private int damage;
    [SerializeField] private Rigidbody2D _playerRB;
    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerRB.velocity = new Vector2(0, 8f);
            HealthSystem.Instance.TakeDamage(damage);
        }
    }
}