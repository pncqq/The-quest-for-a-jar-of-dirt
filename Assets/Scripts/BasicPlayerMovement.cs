using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(SpriteRenderer))]
public class BasicPlayerMovement : MonoBehaviour
{
    //Compontents
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _sprite;
    private BoxCollider2D  _boxCollider2D;

    [SerializeField] private LayerMask jumpableGround;

    //Movement
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 5;
    private float _xInput;
    private float _jumpInput;
    private bool _performJump;
    private bool _isGrounded;
    private bool _doubleJump;

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
            if (IsGrounded()|| _doubleJump)
            {
                _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                _doubleJump = !_doubleJump;
            }
            
        } 
    }
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void FixedUpdate()
    {
        //Variables
        var velocity = new Vector2(_xInput * speed, _rb.velocity.y);
        var isMoving = velocity.x != 0;

        //Move
        _rb.velocity = velocity;

        //Flip sprite
        Flip();

        //Jumping
        /*PlayerJump();*/

        //UpdateAnimation
        UpdateAnimation(velocity, isMoving);
    }

    /*private void PlayerJump()
    {
        if (_performJump)
        {
            _performJump = false;
            _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }*/

    private void Flip()
    {
        //Left-right movement & sprite orientation
        if (_xInput == 0) return;

        var isFacingRight = _xInput > 0 ? 1 : 0;
        _sprite.flipX = isFacingRight != 1;
    }


    private void UpdateAnimation(Vector2 velocity, bool isMoving)
    {
        //Animator variables
        _animator.SetFloat(YVelocity, velocity.y);
        _animator.SetBool(IsMoving, isMoving);
        _animator.SetBool(IsJumping, !IsGrounded());
    }

    /*private void OnCollisionEnter2D(Collision2D other)
    {
        _isGrounded = IsGrounded();
    }


    private void OnCollisionExit2D(Collision2D other)
    {
        if (_jumpInput != 0) _isGrounded = false;
    }*/


}