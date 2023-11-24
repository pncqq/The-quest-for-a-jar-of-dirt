using System.Collections;
using UnityEngine;

public class EnemyBoat : MonoBehaviour
{
    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;
    [SerializeField] private Animator animator;
    [SerializeField] private float smoothTime;
    private Transform _currPoint;
    private Vector2 _currentVelocity;
    private bool _isIdle = true;
    private bool _isDone = true;

    private static readonly int IsSailing = Animator.StringToHash("IsSailing");

    private void Start()
    {
        _currPoint = pointB.transform;
        _isIdle = false;
        _isDone = false;
    }

    private void Update()
    {
        if (!_isIdle)
        {
            transform.position =
                Vector2.SmoothDamp(transform.position,
                    _currPoint.position, ref _currentVelocity, smoothTime);
            animator.SetBool(IsSailing, true);
            
            _isIdle = false;
            _isDone = false;
        }


        if (Vector2.Distance(transform.position, _currPoint.position) < 0.5f && _currPoint == pointA.transform)
        {
            _currPoint = pointB.transform;
            animator.SetBool(IsSailing, false);
            _isIdle = true;
            StartCoroutine(Flip());
        }

        if (!(Vector2.Distance(transform.position, _currPoint.position) < 0.5f) ||
            _currPoint != pointB.transform) return;
        
        _currPoint = pointA.transform;
        animator.SetBool(IsSailing, false);
        _isIdle = true;
        StartCoroutine(Flip());
    }


    private IEnumerator Flip()
    {
        yield return new WaitForSeconds(1f);
        
        var transform1 = transform;
        var localScale = transform1.localScale;
        
        localScale.x *= -1;
        transform1.localScale = localScale;
        
        _isDone = true;
        _isIdle = false;
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        switch (_isIdle)
        {
            case true when _isDone:
            case false when !_isDone:
                other.transform.SetParent(transform);
                break;
            case true when !_isDone:
                other.transform.SetParent(null);
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}