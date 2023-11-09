using System;
using Unity.VisualScripting;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public static HealthSystem instance;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    
    
    [SerializeField] private BasicPlayerMovement _playerMovement;


    private void Awake()
    {
        instance = this;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        
    }

    private void Update()
    {
        //Nie zmniejszaj poniżej 0
        if (currentHealth < 0) currentHealth = 0;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        if (currentHealth <= 0) Destroy(_playerMovement);
    }



    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
    public void Heal(int healSize)
    {
        currentHealth += healSize;
        healthBar.SetHealth(currentHealth);
    }
}