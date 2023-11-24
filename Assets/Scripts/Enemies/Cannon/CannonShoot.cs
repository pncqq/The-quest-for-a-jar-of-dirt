using System.Collections;
using UnityEngine;

public class CannonShoot : MonoBehaviour
{
    [SerializeField] private AudioSource shootAudioSource;
    [SerializeField] private float cooldown;
    [SerializeField] private float waitBefore;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private Animator effect;
    private Animator _animator;
    private bool _playerInArea;
    private static readonly int CanShoot = Animator.StringToHash("Shoot");
    private static readonly int Fire = Animator.StringToHash("Fire");

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
        //Spawn projectile
        Instantiate(projectilePrefab, spawnPoint.transform.position, transform.rotation);

        //Effect anim and shoot anim
        shootAudioSource.Play();
        effect.SetTrigger(Fire);
        _animator.SetBool(CanShoot, false);

        //Wait
        yield return new WaitForSeconds(cooldown);

        //Anim
        if (_playerInArea)
        {
            
            _animator.SetBool(CanShoot, true);
        }
            
    }


    private IEnumerator TriggerShoot()
    {
        yield return new WaitForSeconds(waitBefore);

        if (!_playerInArea) yield break;
        
        _animator.SetBool(CanShoot, true);
    }
}