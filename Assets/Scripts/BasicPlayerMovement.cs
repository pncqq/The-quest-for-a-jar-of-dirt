using UnityEngine;

public class BasicPlayerMovement : MonoBehaviour
{
    internal float Horizontal;
    internal Vector2 Velocity;
    private const float Speed = 5f;
    private const float JumpingPower = 7.5f;
    private bool _isFacingRight = true;
    private const float CoyoteTime = 0.3f;

    private bool _isWallSliding;
    private const float WallSlidingSpeed = 2f;

    private bool _isWallJumping;
    private float _wallJumpingDirection;
    private const float WallJumpingTime = 0.2f;
    private float _wallJumpingCounter;
    private const float WallJumpingDuration = 0.4f;
    private readonly Vector2 _wallJumpingPower = new Vector2(5f, 7.5f);
    public static bool IsGroundedVar;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    private float _lastPressedJumpTime;
    private bool _isJumping;
    [SerializeField] private int doubleJump = 1;
    private int _doubleJump;
    private float _lastOnGroundTime;

    private void Update()
    {
        //Zmienna do animacji potrzebna yasss bitch purrr
        Velocity = _rb.velocity;

        Horizontal = Input.GetAxisRaw("Horizontal");
        _lastOnGroundTime -= Time.deltaTime;
        _lastPressedJumpTime -= Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            if (Input.GetButtonDown("Jump"))
            {
                _lastPressedJumpTime = 0.3f;
                if (CanJump() && _lastPressedJumpTime > 0)
                {
                    _isJumping = true;
                    Jump();
                }
                else if (_lastPressedJumpTime > 0 && _doubleJump > 0)
                {
                    _isJumping = true;
                    _doubleJump--;
                    Jump();
                }
            }
        }

        WallSlide();
        WallJump();
        if (!_isJumping)
        {
            if (IsGrounded())
            {
                _lastOnGroundTime = CoyoteTime;
                _doubleJump = doubleJump;
            }
        }

        if (_isJumping && _rb.velocity.y < 0)
        {
            _isJumping = false;
        }

        if (!_isWallJumping)
        {
            Flip();
        }
    }


    private bool CanJump()
    {
        return _lastOnGroundTime > 0 && !_isJumping;
    }

    private void Jump()
    {
        _lastPressedJumpTime = 0;
        _lastOnGroundTime = 0;
        var force = JumpingPower;
        if (_rb.velocity.y < 0) // jeśli postać już spada wzmocniony będzie skok
        {
            force -= _rb.velocity.y;
        }

        if (_rb.velocity.y > 0)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0f); // unikniecie wysokiego wyskoku podczas 2 x spacja
        }

        _rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        if (!_isWallJumping)
        {
            _rb.velocity = new Vector2(Horizontal * Speed, _rb.velocity.y);
        }
    }

    public bool IsGrounded()
    {
        IsGroundedVar = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        return IsGroundedVar;
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && Horizontal != 0f)
        {
            _isWallSliding = true;
            _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Clamp(_rb.velocity.y, -WallSlidingSpeed, float.MaxValue));
        }
        else
        {
            _isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (_isWallSliding)
        {
            _isWallJumping = false;
            _wallJumpingDirection = -transform.localScale.x;
            _wallJumpingCounter = WallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            _wallJumpingCounter -= Time.deltaTime;
        }


        if (Input.GetButtonDown("Jump") && _wallJumpingCounter > 0f)
        {
            _isWallJumping = true;
            _rb.velocity = new Vector2(_wallJumpingDirection * _wallJumpingPower.x, _wallJumpingPower.y);
            _wallJumpingCounter = 0f;

            if (transform.localScale.x != _wallJumpingDirection)
            {
                _isFacingRight = !_isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), WallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        _isWallJumping = false;
    }

    private void Flip()
    {
        if (_isFacingRight && Horizontal < 0f || !_isFacingRight && Horizontal > 0f)
        {
            _isFacingRight = !_isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}