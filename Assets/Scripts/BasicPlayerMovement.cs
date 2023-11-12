using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(SpriteRenderer))]
public class BasicPlayerMovement : MonoBehaviour
{
    public static Vector2 Velocity;
    public static bool IsGroundedVar;
    
    //Compontents
    private Rigidbody2D _rb;
    private BoxCollider2D _boxCollider2D;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private Transform frontWallCheckPoint;
    [SerializeField] private Transform backWallCheckPoint;
    [SerializeField] private Vector2 wallCheckSize = new Vector2(0.01f, 0f);

    //Movement
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 5;
    public static float XInput;
    private static bool _isFacingRight = true;
    
    //Jump
    public float jumpHangAccelerationMult = 0.5f; 
    public float jumpHangMaxSpeedMult = 0.5f; 
    [SerializeField] private int doubleJump = 1;
    private const float CoyoteTime = 0.3f;
    private int _doubleJump;
    
    //Wall jump
    [SerializeField] private float wallSlideSpeed = 2f;
    private const float WallJumpTime = 0.3f;
    private bool _isWallJumping;
    private float _wallJumpStartTime;
    private Vector2 WallJumpForce = new Vector2(3f, 3f);
    private bool _isJumpCut;
    private float _wallJumpRunLerp = 0.5f;
    
    
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
        Debug.Log(_lastOnWallTime);
        _lastOnGroundTime -= Time.deltaTime;
        _lastPressedJumpTime -= Time.deltaTime;
        _lastOnWallLeftTime -= Time.deltaTime;
        _lastOnWallRightTime -= Time.deltaTime;
        _lastOnWallTime -= Time.deltaTime;
        XInput = Input.GetAxisRaw("Horizontal");


        if (XInput != 0)
        {
            CheckDirectionToFace(XInput > 0);
        }

        if (Input.GetButtonDown("Jump"))
        {
            _lastPressedJumpTime = 0.3f;
        }

        if (Input.GetButtonUp("Jump"))
        {
            OnJumpUpInput();
        }

        if (!_isJumping)
        {
            if (IsGrounded() && !_isJumping)
            {
                _lastOnGroundTime = CoyoteTime;
            }
            //Right Wall Check
            if (((Physics2D.OverlapBox(frontWallCheckPoint.position, wallCheckSize, 0, jumpableGround) &&
                  _isFacingRight)
                 || (Physics2D.OverlapBox(backWallCheckPoint.position, wallCheckSize, 0, jumpableGround) &&
                     !_isFacingRight)) && !_isWallJumping)
            {
                _lastOnWallRightTime = CoyoteTime;
              
            }


            //Left Wall Check
            if (((Physics2D.OverlapBox(frontWallCheckPoint.position, wallCheckSize, 0, jumpableGround) &&
                  !_isFacingRight)
                 || (Physics2D.OverlapBox(backWallCheckPoint.position, wallCheckSize, 0, jumpableGround) &&
                     _isFacingRight)) && !_isWallJumping)
            {
                _lastOnWallLeftTime = CoyoteTime;
              
            }

            _lastOnWallTime = Mathf.Max(_lastOnWallLeftTime, _lastOnWallRightTime);
        }



        if (_isJumping && _rb.velocity.y < 0)
        {
            _isJumping = false;
        }

        if (_isWallJumping && Time.time - _wallJumpStartTime > WallJumpTime)
        {
            _isWallJumping = false;
        }

        if (_lastOnGroundTime > 0 && !_isJumping && !_isWallJumping)
        {
            _isJumpCut = false;
        }

        if (CanJump() && _lastPressedJumpTime > 0)
        {
            _isJumping = true;
            _isWallJumping = false;
            _isJumpCut = false;
            Jump();
        }
        else if (CanWallJump() && _lastPressedJumpTime > 0)
        {
            _isWallJumping = true;
            _isJumping = false;
            _isJumpCut = false;
            _wallJumpStartTime = Time.time;
            _lastWallJumpDir = (_lastOnWallRightTime > 0) ? -1 : 1;
            WallJump(_lastWallJumpDir);
        }

      
        


    }

    private bool CanWallJump()
    {
        return _lastPressedJumpTime > 0 && _lastOnWallTime > 0 && _lastOnGroundTime <= 0 && (!_isWallJumping ||
            (_lastOnWallRightTime > 0 && _lastWallJumpDir == 1) || (_lastOnWallLeftTime > 0 && _lastWallJumpDir == -1));
    }
    
    private void Turn()
    {
        //stores scale and flips the player along the x axis, 
        Vector3 scale = transform.localScale; 
        scale.x *= -1;
        transform.localScale = scale;

        _isFacingRight = !_isFacingRight;
    }
    private bool CanJumpCut()
    {
        return _isJumping && _rb.velocity.y > 0;
    }
    public void OnJumpUpInput()
    {
        if (CanJumpCut() || CanWallJumpCut())
            _isJumpCut = true;
    }
    private bool CanWallJumpCut()
    {
        return _isWallJumping && _rb.velocity.y > 0;
    }
    
    public void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != _isFacingRight)
            Turn();
    }

    private void WallJump(int dir)
    {
        //Ensures we can't call Wall Jump multiple times from one press
        _lastPressedJumpTime = 0;
        _lastOnGroundTime = 0;
        _lastOnWallRightTime = 0;
        _lastOnWallLeftTime = 0;
       

        Vector2 force = new Vector2(WallJumpForce.x, WallJumpForce.y);
        force.x *= dir; //apply force in opposite direction of wall

        if (Mathf.Sign(_rb.velocity.x) != Mathf.Sign(force.x))
        {
            force.x -= _rb.velocity.x;
        }
            

        if (_rb.velocity.y < 0)
        {
            force.y -= _rb.velocity.y;
        }
            
        
        _rb.AddForce((force), ForceMode2D.Impulse);

    }

    private void FixedUpdate()
    {
        if (_isWallJumping)
        {
            Run(_wallJumpRunLerp);
            
        }else 
            Run(1);
    }

    private void Run(float lerp)
    {
        if (IsGrounded())
        {
            var runDistance = XInput * speed;
            runDistance = Mathf.Lerp(_rb.velocity.x, runDistance, lerp);

            float speedDif = runDistance - _rb.velocity.x;
        
            _rb.AddForce(speedDif * Vector2.right, ForceMode2D.Force);
        }
      
        
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
        _rb.AddForce(Vector2.up * (force), ForceMode2D.Impulse);
        
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
        if (_isFacingRight && XInput < 0f || !_isFacingRight && XInput > 0f)
        {
            _isFacingRight = !_isFacingRight;
            var transform1 = transform;
            var localScale = transform1.localScale;
            localScale.x *= -1f;
            transform1.localScale = localScale;
        }
    }
}

/**
 *
 * 
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
            else if (CanWallJump() && _lastPressedJumpTime > 0)
            {
                _isWallJumping = true;
                _isJumping = false;
                _wallJumpStartTime = Time.time;
                _lastWallJumpDir = (_lastOnWallRightTime > 0) ? -1 : 1;
                WallJump(_lastWallJumpDir);
            }

        }
        if (!_isJumping)
        {
            if (IsGrounded())
            {
                _lastOnGroundTime = CoyoteTime;
                _doubleJump = doubleJump;
            }

            //Right Wall Check
            if (((Physics2D.OverlapBox(frontWallCheckPoint.position, wallCheckSize, 0, jumpableGround) &&
                  _isFacingRight)
                 || (Physics2D.OverlapBox(backWallCheckPoint.position, wallCheckSize, 0, jumpableGround) &&
                     !_isFacingRight)) && !_isWallJumping)
            {
                _lastOnWallRightTime = CoyoteTime;
              
            }


            //Left Wall Check
            if (((Physics2D.OverlapBox(frontWallCheckPoint.position, wallCheckSize, 0, jumpableGround) &&
                  !_isFacingRight)
                 || (Physics2D.OverlapBox(backWallCheckPoint.position, wallCheckSize, 0, jumpableGround) &&
                     _isFacingRight)) && !_isWallJumping)
            {
                _lastOnWallLeftTime = CoyoteTime;
              
            }

       

            //Two checks needed for both left and right walls since whenever the play turns the wall checkPoints swap sides
            _lastOnWallTime = Mathf.Max(_lastOnWallLeftTime, _lastOnWallRightTime);
           

        }


        if (_isJumping && _rb.velocity.y < 0)
        {
            _isJumping = false;
        }

        if (_isWallJumping && Time.time - _wallJumpStartTime > WallJumpTime )
        {
            _isWallJumping = false;
        }

**/