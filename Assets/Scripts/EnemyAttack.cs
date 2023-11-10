using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int dmg;
    [SerializeField] private BasicPlayerMovement playerMovement;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerMovement.knockbackTimer = playerMovement.knockbackTotal;
            if (other.transform.position.x <= transform.position.x)
                playerMovement.knockbackRight = true;
            else
            {
                playerMovement.knockbackRight = false;
            }
            HealthSystem.instance.TakeDamage(dmg);
        }
    }
}
