using System;
using UnityEngine;

public class MovingPlatform : MonoBehaviour

{
    public Transform posA, posB;
    public int speed;
    private Vector2 _targetPos;

    private void Start()
    {
        _targetPos = posB.position;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, posA.position) < 0.1f)
            _targetPos = posB.position;
        if (Vector2.Distance(transform.position, posB.position) < 0.1f)
            _targetPos = posA.position;


        transform.position =
            Vector2.MoveTowards(transform.position, _targetPos, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
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
        Gizmos.DrawLine(posA.position, posB.position);
    }
}