using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShoot : MonoBehaviour
{
    [SerializeField] private float cooldown;
    [SerializeField] private float waitBefore;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject spawnPoint;
    private Animator _animator;
    private bool _playerInArea;
    private static readonly int CanShoot = Animator.StringToHash("Shoot");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        _playerInArea = true;
        StartCoroutine(TriggerShoot());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        _playerInArea = false;
        _animator.SetBool(CanShoot, false);
    }

    public IEnumerator Shoot()
    {
        Instantiate(projectilePrefab, spawnPoint.transform.position, transform.rotation);

        _animator.SetBool(CanShoot, false);

        yield return new WaitForSeconds(cooldown);

        if (_playerInArea)
            _animator.SetBool(CanShoot, true);
    }


    private IEnumerator TriggerShoot()
    {
        yield return new WaitForSeconds(waitBefore);

        if (!_playerInArea) yield break;
        
        _animator.SetBool(CanShoot, true);
        StartCoroutine(Shoot());
    }
}