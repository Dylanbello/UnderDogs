using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extension;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]

public class GoodCharacterController : MonoBehaviour
{
    public CharacterController _controller;

    private Animator _animator;
    private Rigidbody _rigidbody;

    private float _smoothTurnVelocity;
    private Vector3 _direction;
    public float _smoothTurnTime = 0.3f;
    private Vector3 directionToMoveIn;

    private Quaternion _rotation;
    private Vector3 _rotationVelocity;
    public float _rotationSpeed;
    public float _rotationDefaultSpeed = 180f;
    public float _walkingSpeed = 1f;
    public float _runningSpeed = 3f;
    private float _horizontal;
    private float _vertical;

    private bool _isWalking;
    private bool _isRunning;
    private bool _triggerSit;
    private bool _triggerLieDown;
    private bool _triggerGetUp;
    private bool _triggerJump;
    private bool _triggerSurprise;
    private bool _triggerBark;
    private bool _triggerBite;
    private bool _triggerDeath;

    public Transform cam;

    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();

        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    // Update is called once per frame
    void Update()
    {
        Getinput();
        Move();
        UpdateAnimator();
    }

    /// <summary>
    /// 
    /// </summary>
    private void Getinput()
    {
        _isWalking = false;
        _isRunning = false;

        //Get MovementInput
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        _horizontal = Input.GetAxis(AxisNames.Horizontal);
        _vertical = Input.GetAxis(AxisNames.Vertical);

        //Get DirectionalInput
        var cameraForwardDirection = Camera.main.transform.forward;
        directionToMoveIn = Vector3.Scale(cameraForwardDirection, (Vector3.right + Vector3.forward));

        //Get input
        _isWalking = (_vertical > 0f) || (_horizontal != 0f);
        _isRunning = (_vertical > 0f) && Input.GetKey(KeyCode.LeftShift);
        _rotationSpeed = (_horizontal > 0f) ? _rotationDefaultSpeed : ((_horizontal < 0f) ? (_rotationDefaultSpeed * -1) : 0f);


        _triggerSit = Input.GetKeyDown(KeyCode.LeftControl);
        _triggerLieDown = Input.GetKeyDown(KeyCode.LeftAlt);
        _triggerJump = Input.GetKeyDown(KeyCode.Space);
        _triggerSurprise = Input.GetKeyDown(KeyCode.Q);
        _triggerBite = Input.GetKeyDown(KeyCode.R);
        _triggerBark = Input.GetKeyDown(KeyCode.E);
        _triggerDeath = Input.GetKeyDown(KeyCode.T);


        _direction = new Vector3(_horizontal, 0f, _vertical).normalized;

        if (Methods.AnimationBeingPlayed(_animator, AnimationNames.SitIdle) || Methods.AnimationBeingPlayed(_animator, AnimationNames.LyingDownIdle))
            _triggerGetUp = Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.LeftAlt) || _isWalking;
    }

    /// <summary>
    /// Movement and rotation
    /// </summary>
    private void Move()
    {
        //if (Methods.AnimationBeingPlayed(_animator, AnimationNames.Walk) || Methods.AnimationBeingPlayed(_animator, AnimationNames.Run) || Methods.AnimationBeingPlayed(_animator, AnimationNames.Jump))
        if(_direction.magnitude > 0.1f)
        {

            //float _targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg * cam.eulerAngles.y;
            //float _angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetAngle, ref _smoothTurnVelocity, _smoothTurnTime);

            //Vector3 _moveDirection = Quaternion.Euler(0f, _targetAngle, 0f) * Vector3.forward;
            //transform.rotation = Quaternion.Euler(0f, _targetAngle, 0f);

            //Move
            if (_isRunning)
                //_controller.Move(_moveDirection.normalized * _walkingSpeed * Time.deltaTime);
            _rigidbody.velocity = directionToMoveIn * _vertical * _runningSpeed + new Vector3(0, _rigidbody.velocity.y, 0);

            else if (_isWalking)
                //_controller.Move(_moveDirection.normalized * _runningSpeed * Time.deltaTime);
            _rigidbody.velocity = directionToMoveIn * _vertical * _walkingSpeed + new Vector3(0, _rigidbody.velocity.y, 0);



            //Rotate
            _rotationVelocity = new Vector3(0f, _rotationSpeed, 0f);
            _rotation = Quaternion.Euler(_rotationVelocity * Time.deltaTime);
            _rigidbody.MoveRotation(_rigidbody.rotation * _rotation);
        }
    }

    /// <summary>
    /// Animation
    /// </summary>
    private void UpdateAnimator()
    {
        if (_triggerDeath && !Methods.AnimationBeingPlayed(_animator, AnimationNames.Die))
        {
            _animator.SetTrigger(AnimationParameters.Die);
        }

        if (Methods.AnimationBeingPlayed(_animator, AnimationNames.Idle) || Methods.AnimationBeingPlayed(_animator, AnimationNames.Walk) || Methods.AnimationBeingPlayed(_animator, AnimationNames.Run))
        {
            _animator.SetBool(AnimationParameters.IsWalking, _isWalking);
            _animator.SetBool(AnimationParameters.IsRunning, _isRunning);

            if (_triggerJump)
                _animator.SetTrigger(AnimationParameters.Jump);
        }

        if (Methods.AnimationBeingPlayed(_animator, AnimationNames.Idle))
        {
            if (_triggerSit)
                _animator.SetTrigger(AnimationParameters.Sit);
            else if (_triggerLieDown)
                _animator.SetTrigger(AnimationParameters.LayDown);
            else if (_triggerSurprise)
                _animator.SetTrigger(AnimationParameters.Surprise);
            else if (_triggerBite)
                _animator.SetTrigger(AnimationParameters.Bite);
            else if (_triggerBark)
                _animator.SetTrigger(AnimationParameters.Bark);
        }

        if (Methods.AnimationBeingPlayed(_animator, AnimationNames.SitIdle) || Methods.AnimationBeingPlayed(_animator, AnimationNames.LyingDownIdle))
        {
            if (_triggerGetUp)
            {
                _animator.SetTrigger(AnimationParameters.GetUp);
            }
        }

        _triggerSit = false;
        _triggerLieDown = false;
        _triggerJump = false;
        _triggerGetUp = false;
        _triggerSurprise = false;
        _triggerBite = false;
        _triggerBark = false;
    }
    
}
