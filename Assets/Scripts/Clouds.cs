using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cloudv2 : MonoBehaviour
{
    public Transform start, end;
    [Range(-10.0f, 10.0f)]
    public float speed;
    private float width;

    //szerokoœæ sprita w x
    void Start()
    {
        //width = GetComponent<SpriteRenderer>().bounds.size.x;
        transform.position = start.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if((int)transform.position.x == (int)end.position.x)
        {
            Vector3 newPosition = start.position;
            transform.position = newPosition;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(start.position, end.position);
    }
}
