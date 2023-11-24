using System.Collections;
using UnityEngine;

public class CannonballRight : MonoBehaviour
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
        _rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        //Knockback
        other.GetComponent<BasicPlayerMovement>().knockbackTimer =
            other.GetComponent<BasicPlayerMovement>().knockbackTotal;
        other.GetComponent<BasicPlayerMovement>().knockbackRight =
            other.transform.position.x <= transform.position.x;
        
        //Update health
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