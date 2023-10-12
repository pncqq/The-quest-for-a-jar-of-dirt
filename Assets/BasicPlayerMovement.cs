using System;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class BasicPlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _xInput;
    [SerializeField] private float _speed = 5;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    //Update is called once per frame
   private void Update()
   {
       _xInput = Input.GetAxis("Horizontal");
   }

   private void FixedUpdate()
   {
       _rb.velocity = new Vector2(_xInput * _speed, _rb.velocity.y );
   }
}
