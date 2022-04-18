using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CharacterController)), AddComponentMenu("Player/Player Locomotion")]
public class BC_CharacterControllerMovement : MonoBehaviour
{
    const float gravityValue = -9.81f;

    CharacterController controller;
    Transform cam;
    CinemachineFreeLook cmFreeLook;
    [HideInInspector] public Animator animator;

    Vector3 playerVelocity;
    [HideInInspector] public Vector3 moveInput;
    [HideInInspector] public bool isSprinting = false;
    [SerializeField] bool grounded;
    [SerializeField] bool wasGrounded;
    
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float sprintSpeed = 5f;
    [SerializeField] float jumpHeight = 1.0f;
    [SerializeField] float turnSmoothTime = 0.1f;

    [Header("Particle Effects")]
    [SerializeField] ParticleSystem landingParticles;
    [SerializeField] ParticleSystem movementParticles;


    private const string startJump = "Jump_Launch";
    private const string InAir = "Jump_Air";
    private const string landJump = "Jump_Land";


    private void Awake()
    {
        //rigidbody = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        cam = GetComponentInChildren<Camera>().transform;
        cmFreeLook = GetComponentInChildren<CinemachineFreeLook>();
    }

    private void Start()
    {
        cam.parent = null;
        cmFreeLook.Follow = transform;
        cmFreeLook.LookAt = transform;

        movementParticles.Stop();

        if(Cursor.visible) { Cursor.lockState = CursorLockMode.Confined; Cursor.visible = false; }

        //canJump = true;
    }
    bool landCheck = false;
    private void Update()
    {
        CheckFalling();
        Movement();
        Rotation();
        HandleSprinting();
        SetCorrectAnimation();
        isGrounded();
        Idle();

        if(movementParticles != null && moveInput != Vector3.zero) { movementParticles.Play(); }
        else { movementParticles.Stop(); }

        if(grounded && !landCheck)     //if the player was just grounded
        {
            landCheck = true;
            animator.CrossFade(landJump, 0.2f);
            if (landingParticles != null)
            {
                ParticleSystem landingPart = Instantiate(landingParticles, transform);
                landingPart.Play();
                landingPart.transform.parent = null;
            }
        }
    }

    public void OnEnableControls() { GetComponent<PlayerInput>().enabled = true; }

    public void OnDisableControls() { GetComponent<PlayerInput>().enabled = false; }

    #region Sprinting

    void HandleSprinting()
    {
        if (!isSprinting) moveSpeed = 5;
        else moveSpeed = sprintSpeed;
    }

    #endregion

    #region Movement & Falling
    void Movement()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        move = cam.transform.forward * move.z + cam.transform.right * move.x;
        move.y = 0;
        
        controller.Move(move * Time.deltaTime * moveSpeed);
    }

    void Rotation()
    {
        if(moveInput == Vector3.zero) { return; }   //Comment out this line to allow turning while stationary.
        
        float targetAngle = Mathf.Atan2(moveInput.x, moveInput.y) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * turnSmoothTime);
        
    }

    void CheckFalling()
    {

        //Stop Falling
        if (playerVelocity.y < 0 && grounded) { playerVelocity.y = 0; }

        //Applies gravity
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void isGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, .2f, 1 << LayerMask.NameToLayer("Environment"))) 
        { 
            grounded = true; 
            wasGrounded = false;
            //animator.ResetTrigger("Jump");
        }
        else { grounded = false; }
    }
    #endregion

    #region Jumping

    public void Jump()
    {
        if (!grounded || animator.GetBool("Sit") == true) return;     //Guard clause for double jumping.

        //animator.SetTrigger("Jump");

        animator.CrossFade(startJump, 0.2f);
        if(grounded) 
        { 
            wasGrounded = true;
            landCheck = false;
        }
        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3 * gravityValue);
    }

    #endregion

    #region Applying Animation Values

    private void SetCorrectAnimation()
    {
        float CharMoveSpeed;
        /*
        if (moveInput.x > 0 && moveInput.x < .55f) { moveX = .5f; }
        else if (moveInput.x > .55f) { moveX = 1; }
        else if (moveInput.x < 0 && moveInput.x > -.55f) { moveX = -.5f; }
        else if (moveInput.x < -.55f) { moveX = -1; }
        else { moveX = 0; }

        if (moveInput.y > 0 && moveInput.y < .55f) { moveY = .5f; }
        else if (moveInput.y > .55f) { moveY = 1; }
        else if (moveInput.y < 0 && moveInput.y > -.55f) { moveY = -.5f; }
        else if (moveInput.y < -.55f) { moveY = -1; }
        else { moveY = 0; }
        */

        if(moveInput.x > 0 && moveInput.x < 0.55f || moveInput.y > 0 && moveInput.y < 0.55f) { CharMoveSpeed = 0.5f; }
        else if(moveInput.x > 0.55f || moveInput.y > 0.55f) { CharMoveSpeed = 1; }
        else if(moveInput.x < 0 && moveInput.x > -0.55f || moveInput.y < 0 && moveInput.y > -0.55f) { CharMoveSpeed = 0.5f; }
        else if(moveInput.x < -0.55f || moveInput.y < -0.55f) { CharMoveSpeed = 1; }
        else { CharMoveSpeed = 0; }

        animator.SetFloat("Move", CharMoveSpeed, .1f, Time.deltaTime);
    }

    #endregion

    // This is just a small thing that plays an animation when the player is idle for a while.
    #region Idle

    float idleTimer;

    void Idle()
    {
        if(moveInput != Vector3.zero || animator.GetBool("GetUp") == true) { idleTimer = 0; return; }
        else { idleTimer += Time.deltaTime; }

        if(idleTimer >= 20) { animator.SetTrigger("Sit"); idleTimer = 20; }
    }

    #endregion
}