using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileRight : MonoBehaviour
{
    
    [SerializeField] private float _speed;
    private Rigidbody2D _rb;

    
    // Start is called before the first frame update
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
            _rb.velocity = transform.right * _speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            Destroy(other);
        if (!other.CompareTag("Enemy"))
            Destroy(gameObject);

    }
    
}