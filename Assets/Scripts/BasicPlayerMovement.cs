using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BasicPlayerMovement : MonoBehaviour
{
    //Compontents
    private Rigidbody2D _rb;
    private Animator animator;
    private SpriteRenderer _sprite;

    //Movement
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _jumpForce = 5;
    private float _xInput;
    private bool _performJump;
    private bool _isGrounded;

    //Animator
    private static readonly int XVelocity = Animator.StringToHash("xVelocity");
    private static readonly int YVelocity = Animator.StringToHash("yVelocity");
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private static readonly int IsJumping = Animator.StringToHash("isJumping");


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    //Update is called once per frame
    private void Update()
    {
        _xInput = Input.GetAxisRaw("Horizontal");

        //Jump check
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _performJump = true;
        }
    }

    private void FixedUpdate()
    {
        //Variables
        var velocity = new Vector2(_xInput * _speed, _rb.velocity.y);
        var isMoving = velocity.x != 0;

        //Left-right movement & sprite orientation
        _rb.velocity = velocity;
        if (velocity.x != 0)
        {
            var isFacingRight = velocity.x > 0 ? 1 : 0;
            _sprite.flipX = isFacingRight != 1;
        }

        //Jumping
        if (_performJump)
        {
            _performJump = false;
            _rb.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
        }

        //UpdateAnimation
        UpdateAnimation(velocity, isMoving);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _isGrounded = true;
    }


    private void OnCollisionExit2D(Collision2D other)
    {
        _isGrounded = false;
    }

    private void UpdateAnimation(Vector2 velocity, bool isMoving)
    {
        //Animator variables
        animator.SetFloat(XVelocity, velocity.x);
        animator.SetFloat(YVelocity, velocity.y);
        animator.SetBool(IsMoving, isMoving);
        animator.SetBool(IsJumping, !_isGrounded);
    }


    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     _isGrounded = true;
    // }
    //
    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     _isGrounded = false;
    // }
}