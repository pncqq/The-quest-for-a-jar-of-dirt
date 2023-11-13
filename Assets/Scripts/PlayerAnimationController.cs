using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    //====================FIELDS===========================/
    private Animator _animator;

    internal float AttackDelay;
    private float _airAttackDelay;
    private string _currentAnimation;
    private bool _isAttacking;
    private bool _isGrounded;
    internal bool IsSworded;
    private bool _isDead;
    
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
                    AttackDelay = clip.length - 0.025f;
                    break;
                case "Player_AirAttack1":
                    _airAttackDelay = clip.length;
                    break;
            }
        }
    }

    private void Update()
    {
        //Check if dead
        if (HealthSystem.instance.currentHealth <= 0)
            _isDead = true;
        
        //Check if attack key pressed
        if (Input.GetKeyDown(KeyCode.F) && IsSworded)
            _isAttacking = true;

        //Update Ground Check
        _isGrounded = BasicPlayerMovement.IsGroundedVar;

        //Animation state & CrossFade animation
        var state = GetState();
        _previousState = _currentState;

        if (state == _currentState) return;
        _animator.CrossFade(state, 0, 0);
        _currentState = state;
    }


    private int GetState()
    {
        if (_isDead) return DeadHit;
        //If animation still playing
        if (Time.time < _lockedTill) return _currentState;

        if (!IsSworded)
            return _isGrounded switch
            {
                //Run and idle animation
                true => BasicPlayerMovement.Horizontal != 0 ? Run : Idle,
                //Jumping animation
                false when BasicPlayerMovement.Velocity.y > 0 => Jump,
                false => Fall
            };

        //Movement animations
        if (!_isAttacking)
        {
            return _isGrounded switch
            {
                //Run and idle animation
                true => BasicPlayerMovement.Horizontal != 0 ? RunSword : IdleSword,
                //Jumping animation
                false when BasicPlayerMovement.Velocity.y > 0 => JumpSword,
                false => FallSword
            };
        }

        _isAttacking = false;

        //Attack 
        return !_isGrounded
            ? LockState(_previousState == AirAttack1 ? AirAttack2 : AirAttack1, _airAttackDelay)
            : _previousState == Attack1
                ? LockState(Attack2, AttackDelay)
                : LockState(_previousState == Attack2 ? Attack3 : Attack1, AttackDelay);


        int LockState(int s, float t)
        {
            _lockedTill = Time.time + t;
            return s;
        }
    }

    #region Cached Properties

    private int _currentState;
    private int _previousState;

    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Run = Animator.StringToHash("Movement");
    private static readonly int Jump = Animator.StringToHash("Player_Jump");
    private static readonly int Fall = Animator.StringToHash("Player_Fall");
    private static readonly int Ground = Animator.StringToHash("Player_Ground");

    private static readonly int Attack1 = Animator.StringToHash("Player_Attack1");
    private static readonly int Attack2 = Animator.StringToHash("Player_Attack2");
    private static readonly int Attack3 = Animator.StringToHash("Player_Attack3");
    private static readonly int AirAttack1 = Animator.StringToHash("Player_AirAttack1");
    private static readonly int AirAttack2 = Animator.StringToHash("Player_AirAttack2");
    private static readonly int IdleSword = Animator.StringToHash("Player_IdleSword");
    private static readonly int RunSword = Animator.StringToHash("Player_RunSword");
    private static readonly int JumpSword = Animator.StringToHash("Player_JumpSword");
    private static readonly int FallSword = Animator.StringToHash("Player_FallSword");
    private static readonly int GroundSword = Animator.StringToHash("Player_GroundSword");
    private static readonly int DeadHit = Animator.StringToHash("Player_Dead_Hit");

    #endregion
}