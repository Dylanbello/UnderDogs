using System.Collections;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CharacterController)), AddComponentMenu("Player/Player Locomotion")]
public class BC_CharacterControllerMovement : MonoBehaviour
{
    const float gravityValue = -9.81f;

    CharacterController controller;
    Transform cam;
    CinemachineFreeLook cmFreeLook;
    Animator animator;
    Rigidbody rigidbody;

    Vector3 playerVelocity;
    [HideInInspector] public Vector3 moveInput;
    [HideInInspector] public bool isSprinting = false;
    bool grounded;
    bool wasGrounded;
    
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float sprintSpeed = 5f;
    [SerializeField] float jumpHeight = 1.0f;
    [SerializeField] float turnSmoothTime = 0.1f;

    [Header("Particle Effects")]
    [SerializeField] ParticleSystem landingParticles;
    [SerializeField] ParticleSystem movementParticles;


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

        if (movementParticles != null)
        {
            if (moveInput != Vector3.zero) { movementParticles.Play(); }
            else { movementParticles.Stop(); }
        }

        if(grounded && !landCheck && landingParticles != null)     //if the player was just grounded
        {
            landCheck = true;
            ParticleSystem landingPart = Instantiate(landingParticles, transform);
            landingPart.Play();
            landingPart.transform.parent = null;
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
        if (Physics.Raycast(transform.position, Vector3.down, .1f, 1 << LayerMask.NameToLayer("Environment"))) { grounded = true; wasGrounded = false; }
        else { grounded = false; }
    }
    #endregion

    #region Jumping


    public void Jump()
    {
        if (!grounded) return;     //Guard clause for double jumping.

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
        float charMoveSpeed = 0;
        float moveX = moveInput.x;
        float moveY = moveInput.y;

        if (moveX == 0 && moveY == 0) charMoveSpeed = 0;
        else if (moveX > 0.8f || moveY > 0.8f) charMoveSpeed = 1;
        else if (moveX < -0.8f || moveY < -0.8f) charMoveSpeed = 1;

        //Sprinting
        if (charMoveSpeed != 0 && isSprinting)
        {
            animator.SetBool("IsRunning", true);
            animator.SetBool("IsWalking", false);
            charMoveSpeed = 2;
        }
        else if (charMoveSpeed != 0 && !isSprinting)
        {
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsWalking", false);
        }

        animator.SetFloat("Move", charMoveSpeed);
    }

    #endregion

    #region Idle

    float idleTimer;

    void Idle()
    {
        if (moveInput == Vector3.zero) { idleTimer += Time.deltaTime; }
        else { idleTimer = 0; animator.SetTrigger("GetUp"); }

        if(idleTimer >= 60 && idleTimer < 300) { animator.SetTrigger("Sit"); idleTimer = 60; }
    }

    #endregion
}