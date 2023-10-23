using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(SpriteRenderer))]
public class BasicPlayerMovement : MonoBehaviour
{
    //Compontents
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _sprite;
    private BoxCollider2D _boxCollider2D;

    [SerializeField] private LayerMask jumpableGround;

    //Movement
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 5;
    private float _xInput;
    private float _jumpInput;
    private bool _doubleJump;
    private bool isFacingRight = true;


    private bool isWallSliding;
    private float wallSlidingSpeed = 2;
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float walljumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 8f);


    [SerializeField] private Transform wallCheck;

    //Animator
    private static readonly int YVelocity = Animator.StringToHash("yVelocity");
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private static readonly int IsJumping = Animator.StringToHash("isJumping");


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    //Update is called once per frame
    private void Update()
    {
        _xInput = Input.GetAxisRaw("Horizontal");
        _jumpInput = Input.GetAxisRaw("Jump");


        if (IsGrounded() && !Input.GetButton("Jump"))
        {
            _doubleJump = false;
        }

        //Jump check
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded() || _doubleJump)
            {
                _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                _doubleJump = !_doubleJump;
            }
        }

        WallSlide();
        WallJump();
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size, 0f, Vector2.down, .1f,
            jumpableGround);
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, jumpableGround);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && _xInput != 0f)
        {
            isWallSliding = true;
            _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Clamp(_rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            walljumpingCounter = wallJumpingTime;
        }
        else
        {
            walljumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && walljumpingCounter > 0)
        {
            isWallJumping = true;
            _rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            walljumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }


    private void FixedUpdate()
    {
        //Variables
        var velocity = new Vector2(_xInput * speed, _rb.velocity.y);
        var isMoving = velocity.x != 0;

        //Move
        _rb.velocity = velocity;

        if (!isWallJumping)
        {
            _rb.velocity = new Vector2(_xInput * speed, _rb.velocity.y);
        }

        //Flip sprite
        Flip();

        //UpdateAnimation
        UpdateAnimation(velocity, isMoving);
    }


    private void Flip()
    {
        //Left-right movement & sprite orientation

        if (isFacingRight && _xInput < 0f || !isFacingRight && _xInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }


    private void UpdateAnimation(Vector2 velocity, bool isMoving)
    {
        //Animator variables
        _animator.SetFloat(YVelocity, velocity.y);
        _animator.SetBool(IsMoving, isMoving);
        _animator.SetBool(IsJumping, !IsGrounded());
    }
}