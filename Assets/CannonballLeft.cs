using System;
using UnityEngine;
using UnityEngine.Serialization;

public class CannonballLeft : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D _rb;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rb.velocity = -transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            HealthSystem.Instance.TakeDamage(50);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
    

    private void OnDestroy()
    {
        Debug.Log("Znszczone ;pp");
    }
}