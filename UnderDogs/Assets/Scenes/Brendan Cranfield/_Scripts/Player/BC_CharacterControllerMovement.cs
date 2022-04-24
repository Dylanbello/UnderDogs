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
    [SerializeField] bool jumping;
    
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

        if(Cursor.visible) { Cursor.lockState = CursorLockMode.Confined; Cursor.visible = false; }
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
    }

    #region Sprinting

    void HandleSprinting()
    {
        if (grounded)
        {
            if (!isSprinting)
            {
                moveSpeed = 5;
                if (movementParticles.isPlaying) movementParticles.Stop();
            }
            else
            {
                moveSpeed = sprintSpeed;
                if(movementParticles.isPlaying) movementParticles.Play();
            }
        }
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
        if (Physics.Raycast(transform.position, Vector3.down, .1f, 1 << LayerMask.NameToLayer("Environment"))) 
        {
            animator.SetBool("Grounded", true);
            grounded = true;

            animator.SetBool("Jumping", false);
            jumping = false;

            animator.SetBool("Falling", false);
        }
        else 
        { 
            animator.SetBool("Grounded", false);
            grounded = false;

            animator.SetBool("Falling", true);
        }
    }
    #endregion

    #region Jumping

    public void Jump()
    {
        if (!grounded) return;     //Guard clause

        animator.SetBool("Jumping", true);
        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3 * gravityValue);
    }

    #endregion

    #region Applying Animation Values

    private void SetCorrectAnimation()
    {
        float CharMoveSpeed;

        if(moveInput.x > 0 && moveInput.x < 0.55f || moveInput.y > 0 && moveInput.y < 0.55f) { CharMoveSpeed = 0.5f; }
        else if(moveInput.x > 0.55f || moveInput.y > 0.55f) { CharMoveSpeed = 1; }
        else if(moveInput.x < 0 && moveInput.x > -0.55f || moveInput.y < 0 && moveInput.y > -0.55f) { CharMoveSpeed = 0.5f; }
        else if(moveInput.x < -0.55f || moveInput.y < -0.55f) { CharMoveSpeed = 1; }
        else { CharMoveSpeed = 0; }

        animator.SetFloat("Move", CharMoveSpeed, .1f, Time.deltaTime);
    }

    #endregion

    public void PlayJumpParticles()
    {
        landingParticles.Play();
    }
}