using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(SpriteRenderer))]
public class zapis : MonoBehaviour
{
    public static Vector2 Velocity;
    public static bool IsGroundedVar;
    
    //Compontents
    private Rigidbody2D _rb;
    private BoxCollider2D _boxCollider2D;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private Transform frontWallCheckPoint;
    [SerializeField] private Transform backWallCheckPoint;
    [SerializeField] private Vector2 wallCheckSize = new Vector2(0.5f, 1f);
    

    //Movement
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 5;
    public static  float horizontal;
    private static bool _isFacingRight = true;
   
    
    //Jump
    [SerializeField] private int doubleJump = 1;
    private const float CoyoteTime = 0.3f;
    private int _doubleJump;
    
    //Wall jump
   
    private bool _isWallJumping;
    private float _wallJumpStartTime;
    private Vector2 _wallJumpForce = new Vector2(7.5f, 7.5f);
    private float _wallJumpingCounter;
    private float _wallJumpingDuration = 0.4f;
    private float _wallJumpTime = 0.2f;
    
    
    //Wall slide
    private bool _isWallSliding;
    private bool _isOnLeftWall;
    private bool _isOnRightWall;
    private float _wallJumpingDirection;
    private float _wallSlidingSpeed = 2f;
    
    //Timers
    private float _lastPressedJumpTime;
    private float _lastOnGroundTime;
    private bool _isJumping;
    private float _lastOnWallRightTime;
    private float _lastOnWallLeftTime;
    private float _lastOnWallTime;
    private int _lastWallJumpDir;
    
    


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    //Update is called once per frame
    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        _lastOnGroundTime -= Time.deltaTime;
        _lastPressedJumpTime -= Time.deltaTime;
        _lastOnWallLeftTime -= Time.deltaTime;
        _lastOnWallRightTime -= Time.deltaTime;
        _lastOnWallTime -= Time.deltaTime;
        




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
        
        WallSlide();
        WallJump();
        if (!_isWallJumping)
        {
            Flip();
        }

        if (!_isJumping)
        {
            if (IsGrounded())
            {
                _lastOnGroundTime = CoyoteTime;
                _doubleJump = doubleJump;
            }

  

            if (!(((Physics2D.OverlapBox(frontWallCheckPoint.position, wallCheckSize, 0, jumpableGround) &&
                   _isFacingRight)
                  || (Physics2D.OverlapBox(backWallCheckPoint.position, wallCheckSize, 0, jumpableGround) &&
                      !_isFacingRight)) && !_isWallJumping) && !(((Physics2D.OverlapBox(frontWallCheckPoint.position, wallCheckSize, 0, jumpableGround) &&
                                                                   !_isFacingRight)
                                                                  || (Physics2D.OverlapBox(backWallCheckPoint.position, wallCheckSize, 0, jumpableGround) &&
                                                                      _isFacingRight)) && !_isWallJumping) ) 
            {
                _isOnLeftWall = false;
                _isOnRightWall = false;
            }
       

            //Two checks needed for both left and right walls since whenever the play turns the wall checkPoints swap sides
            _lastOnWallTime = Mathf.Max(_lastOnWallLeftTime, _lastOnWallRightTime);
           

        }


        if (_isJumping && _rb.velocity.y < 0)
        {
            _isJumping = false;
        }

        if (_isWallJumping && Time.time - _wallJumpStartTime > _wallJumpTime )
        {
            _isWallJumping = false;
        }

       
        
        
    }

    private bool IsWalled()
    {
        //Right Wall Check
        if (((Physics2D.OverlapBox(frontWallCheckPoint.position, wallCheckSize, 0, jumpableGround) &&
              _isFacingRight)
             || (Physics2D.OverlapBox(backWallCheckPoint.position, wallCheckSize, 0, jumpableGround) &&
                 !_isFacingRight)) && !_isWallJumping)
        {
            _lastOnWallRightTime = CoyoteTime;
            _isOnRightWall = true;
            return true;
        }


        //Left Wall Check
        if (((Physics2D.OverlapBox(frontWallCheckPoint.position, wallCheckSize, 0, jumpableGround) &&
              !_isFacingRight)
             || (Physics2D.OverlapBox(backWallCheckPoint.position, wallCheckSize, 0, jumpableGround) &&
                 _isFacingRight)) && !_isWallJumping)
        {
            _lastOnWallLeftTime = CoyoteTime;
            _isOnLeftWall = true;
            return true;
        }

        return false;
    }
    private bool CanWallJump()
    {
        return _lastPressedJumpTime > 0 && _lastOnWallTime > 0 && _lastOnGroundTime <= 0 && (!_isWallJumping ||
            (_lastOnWallRightTime > 0 && _lastWallJumpDir == 1) || (_lastOnWallLeftTime > 0 && _lastWallJumpDir == -1));
    }

    
    private void WallSlide()
    {
        Debug.Log("Slide");
        if (IsWalled()&& !IsGrounded() && horizontal != 0f)
        {
            _isWallSliding = true;
            _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Clamp(_rb.velocity.y, -_wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            _isWallSliding = false;
        }
    }
    private void StopWallJumping()
    {
        _isWallJumping = false;
    }
    private void WallJump()
    {


        if (_isWallSliding)
        {
            _isWallJumping = false;
            _wallJumpingDirection = -transform.localScale.x;
            _wallJumpingCounter = _wallJumpTime;
            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            _wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && _wallJumpingCounter > 0f)
        {
            _isWallJumping = true;
            _rb.velocity = new Vector2(_wallJumpingDirection * _wallJumpForce.x, _wallJumpForce.y);
            _wallJumpingCounter = 0f;
            if (transform.localScale.x != _wallJumpingDirection)
            {
                _isFacingRight = !_isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
            Invoke(nameof(StopWallJumping), _wallJumpingDuration);
        }
        
       

    }

    private void FixedUpdate()
    {
        if (!_isWallJumping)
        {
            Run();
        }
    }

    private void Run()
    {
        var runDistance = horizontal * speed;
        _rb.velocity = new Vector2(runDistance, _rb.velocity.y);
    }

    private void Jump()
    {
        _lastPressedJumpTime = 0;
        _lastOnGroundTime = 0;
        var force = this.jumpForce;
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
    private bool IsGrounded() //ground check
    {
        
        IsGroundedVar =
            Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size, 0f, Vector2.down, .1f,
                jumpableGround);
        return IsGroundedVar;
    }
    
   
    
    private bool CanJump()
    {
        return _lastOnGroundTime > 0 && !_isJumping;
    }
    
    private void Flip()
    {
        //Left-right movement & sprite orientation
        if (_isFacingRight && horizontal < 0f || !_isFacingRight && horizontal > 0f)
        {
            _isFacingRight = !_isFacingRight;
            var transform1 = transform;
            var localScale = transform1.localScale;
            localScale.x *= -1f;
            transform1.localScale = localScale;
        }
    }
}