using System;
using UnityEngine;

public class SpikesScript : MonoBehaviour
{
    
    [SerializeField] private int damage;
    [SerializeField] private int knockup;
    
    private GameObject player;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, knockup);
            HealthSystem.Instance.TakeDamage(damage);
        }
    }
}