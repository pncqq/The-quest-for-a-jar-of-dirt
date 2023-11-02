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

    //Movement
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 5;
    public static float XInput;
    private static bool _isFacingRight = true;
    
    //Jump
    [SerializeField] private int doubleJump = 1;
    private bool _canJump = true;
    private float _coyoteTime = 0.3f;
    private int _doubleJump;
    
    //Timers
    private float _lastPressedJumpTime;
    private float _lastOnGroundTime;
    private bool _isJumping;
    


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    //Update is called once per frame
    private void Update()
    {
        _lastOnGroundTime -= Time.deltaTime;
        _lastPressedJumpTime -= Time.deltaTime;
        XInput = Input.GetAxisRaw("Horizontal");


     

        if (Input.GetButtonDown("Jump"))
        {
            _lastPressedJumpTime = 0.3f;
            if (CanJump() && _lastPressedJumpTime > 0)
            {
                _isJumping = true;
                Jump();
            } else if (_lastPressedJumpTime > 0 && _doubleJump > 0)
            {
                _isJumping = true;
                _doubleJump--;
                Jump();
            }
        }

        if (!_isJumping)
        {
            if (IsGrounded())
            {
                _lastOnGroundTime = _coyoteTime;
                _doubleJump = doubleJump;
            }
        }


        if (_isJumping && _rb.velocity.y < 0)
        {
            _isJumping = false;
        }

       
        
        
    }
    


    private void FixedUpdate()
    {
        Run();
        Flip();
    }

    private void Run()
    {
        var runDistance = XInput * speed;
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