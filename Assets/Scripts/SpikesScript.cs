using System;
using UnityEngine;

public class SpikesScript : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField] private int damage;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        HealthSystem.Instance.TakeDamage(damage);
    }
}