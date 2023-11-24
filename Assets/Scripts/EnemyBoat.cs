using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyBoat : MonoBehaviour
{
    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;
    [SerializeField] private Animator animator;
    [SerializeField] private float speed;
    [SerializeField] private float smoothTime;
    private Transform _currPoint;
    private Vector2 _currentVelocity;

    private static readonly int IsSailing = Animator.StringToHash("IsSailing");

    private void Start()
    {
        _currPoint = pointB.transform;
    }

    private void Update()
    {
        //Move boat
        // transform.position =
        //     Vector2.Lerp(transform.position,
        //         _currPoint.transform.position, speed * Time.deltaTime);

        // transform.position =
        //     Vector2.MoveTowards(transform.position,
        //         _currPoint.position, speed * Time.deltaTime);
        
        transform.position = 
            Vector2.SmoothDamp(transform.position,
                _currPoint.position, ref _currentVelocity, smoothTime);

        animator.SetBool(IsSailing, true);

        
        //Current positon
        if (Vector2.Distance(transform.position, _currPoint.position) < 0.5f && _currPoint == pointA.transform)
        {
            _currPoint = pointB.transform;
            animator.SetBool(IsSailing, false);
            Flip();
        }

        if (!(Vector2.Distance(transform.position, _currPoint.position) < 0.5f) ||
            _currPoint != pointB.transform) return;
        _currPoint = pointA.transform;

        //
        animator.SetBool(IsSailing, false);
        Flip();
    }


    private void Flip()
    {
        var transform1 = transform;
        var localScale = transform1.localScale;

        localScale.x *= -1;
        transform1.localScale = localScale;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
            // other.transform.SetParent(null);
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