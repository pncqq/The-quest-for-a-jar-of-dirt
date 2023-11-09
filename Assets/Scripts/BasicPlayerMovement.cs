using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(SpriteRenderer))]
public class BasicPlayerMovement : MonoBehaviour
{
    //Compontents
    private Rigidbody2D _rb;
    private BoxCollider2D _boxCollider2D;
    [SerializeField] private LayerMask jumpableGround;

    //Movement
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 5;
    public static float XInput;
    private bool _doubleJump;
    public static bool IsFacingRight = true;
    public static Vector2 Velocity;
    public static bool IsMoving;
    public static bool IsGroundedVar;


    private bool isWallSliding;
    private float wallSlidingSpeed = 2;
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float walljumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 8f);


    [SerializeField] private Transform wallCheck;


    
    public float knockbackForce;
    public float knockbackTimer;
    public float knockbackTotal;
    public bool knockbackRight;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    //Update is called once per frame
    private void Update()
    {
        XInput = Input.GetAxisRaw("Horizontal");


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
        IsGroundedVar =
            Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size, 0f, Vector2.down, .1f,
                jumpableGround);
        return IsGroundedVar;
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, jumpableGround);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && XInput != 0f)
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
                IsFacingRight = !IsFacingRight;
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
        Velocity = new Vector2(XInput * speed, _rb.velocity.y);
        IsMoving = Velocity.x != 0;

        //Move
        if(knockbackTimer <= 0)
            _rb.velocity = Velocity;
        else
        {
            //Knock-back
            if (knockbackRight)
                _rb.velocity = new Vector2(-knockbackForce*2, knockbackForce/2);
            else
                _rb.velocity = new Vector2(knockbackForce*2, knockbackForce/2);
            knockbackTimer -= Time.deltaTime;
        }

        //Jump
        /*if (!isWallJumping)
        {
            _rb.velocity = new Vector2(XInput * speed, _rb.velocity.y);
        }*/

        //Flip sprite
        Flip();
    }


    private void Flip()
    {
        //Left-right movement & sprite orientation
        if (IsFacingRight && XInput < 0f || !IsFacingRight && XInput > 0f)
        {
            IsFacingRight = !IsFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}