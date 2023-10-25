using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    //====================FIELDS===========================/
    private Animator _animator;

    private float _attackDelay;
    private float _airAttackDelay;
    private string _currentAnimation;
    private bool _isAttackPressed;
    private bool _isAttacking;
    private bool _isGrounded;

    private double _lockedTill;
    //=====================================================/

    private void Awake()
    {
        //Initialize
        _animator = GetComponent<Animator>();

        //Get attackDelay state length and attackAir
        var clips = _animator.runtimeAnimatorController.animationClips;
        foreach (var clip in clips)
        {
            switch (clip.name)
            {
                case "Player_Attack1":
                    _attackDelay = clip.length;
                    break;
                case "Player_AirAttack1":
                    _airAttackDelay = clip.length;
                    break;
            }
        }
    }

    private void Update()
    {
        //Check if attack key pressed
        if (Input.GetKeyDown(KeyCode.F))
            _isAttackPressed = true;

        //Update Ground Check
        _isGrounded = BasicPlayerMovement.IsGroundedVar;

        //Animation state & CrossFade animation
        var state = GetState();

        if (state == _currentState) return;
        _animator.CrossFade(state, 0, 0);
        _currentState = state;
    }


    private int GetState()
    {
        //If animation still playing
        if (Time.time < _lockedTill) return _currentState;

        //Movement animations
        if (!_isAttackPressed)
            return _isGrounded switch
            {
                //Run and idle animation
                true => BasicPlayerMovement.XInput != 0 ? Walk : Idle,
                //Jumping animation
                false when BasicPlayerMovement.Velocity.y > 0 => Jump,
                false => Fall
            };
        _isAttackPressed = false;

        //Attack 
        return _isGrounded
            ? LockState(Attack, _attackDelay)
            : LockState(AirAttack, _airAttackDelay);


        int LockState(int s, float t)
        {
            _lockedTill = Time.time + t;
            return s;
        }
    }

    #region Cached Properties

    private int _currentState;

    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Walk = Animator.StringToHash("Movement");
    private static readonly int Jump = Animator.StringToHash("Player_Jump");
    private static readonly int Fall = Animator.StringToHash("Player_Fall");
    private static readonly int Attack = Animator.StringToHash("Player_Attack1");
    private static readonly int AirAttack = Animator.StringToHash("Player_AirAttack1");

    #endregion
}