using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemShoot : MonoBehaviour
{
    [SerializeField] private float cooldown;
    [SerializeField] private float waitBefore;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject spawnPoint;

    private Animator _animator;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    
    IEnumerator Start()
    {
        yield return new WaitForSeconds(waitBefore);
        StartShoot();
    }

    public IEnumerator Shoot()
    {
        
        Instantiate(projectilePrefab, spawnPoint.transform.position, transform.rotation);
        _animator.SetBool("Shoot", false);
        yield return new WaitForSeconds(cooldown);
        StartShoot();
        
        
    }

    public void StartShoot()
    {
            _animator.SetBool("Shoot", true);
    }
}
