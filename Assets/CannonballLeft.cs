using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballLeft : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D _rb;
    private Animator _animator;
    private static readonly int IsDestroyed = Animator.StringToHash("isDestroyed");


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
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
        _animator.SetTrigger(IsDestroyed);

        //Turn off physics
        _rb.velocity = Vector2.zero;
        _rb.isKinematic = true;
        foreach (var coll in GetComponents<Collider2D>())
        {
            coll.enabled = false;
        }

        StartCoroutine(DestroyAfterAnimation());
    }

    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length + 0.5f);
        Destroy(gameObject);
    }
}