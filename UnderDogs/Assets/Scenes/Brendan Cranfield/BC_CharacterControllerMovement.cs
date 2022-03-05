using System.Collections;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
[RequireComponent(typeof(CharacterController))]
public class BC_CharacterControllerMovement : MonoBehaviour
{
    CharacterController controller;
    Transform cam;
    CinemachineFreeLook cmFreeLook;
    Animator animator;

    Vector3 playerVelocity;
    Vector3 moveInput;
    
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    [SerializeField] float turnSmoothTime = 0.1f;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        cam = GetComponentInChildren<Camera>().transform;
        cmFreeLook = GetComponentInChildren<CinemachineFreeLook>();
    }

    private void Start()
    {
        cmFreeLook.Follow = transform;
        cmFreeLook.LookAt = transform;

        if(Cursor.visible) { Cursor.lockState = CursorLockMode.Confined; Cursor.visible = false; }

        canJump = true;
    }

    private void Update()
    {
        CheckFalling();
        Movement();
        Rotation();
        
        animator.SetFloat("Move", SetCorrectAnimation());
    }

    public void OnEnableControls() { GetComponent<PlayerInput>().enabled = true; }

    public void OnDisableControls() { GetComponent<PlayerInput>().enabled = false; }

    #region Inputs

    public void Movement(InputAction.CallbackContext context) { moveInput = context.ReadValue<Vector2>(); }
    public void Jump(InputAction.CallbackContext context) { if(context.performed) Jump(); }

    #endregion

    #region Movement & Falling
    void Movement()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
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
        if (isGrounded) jumpCount = 0;

        //Stop Falling
        if (playerVelocity.y < 0 && isGrounded) { playerVelocity.y = 0; }

        //Applies gravity
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    bool isGrounded { get { return Physics.Raycast(transform.position, Vector3.down, .1f, 1 << LayerMask.NameToLayer("Environment")); } }

    #endregion

    #region Jumping

    bool canJump;
    int jumpCount;

    private void Jump()
    {
        if (jumpCount == 1 || !canJump) return;     //Guard clause for double jumping.
        //animator.SetTrigger("Jump");

        canJump = false;
        jumpCount += 1;


        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3 * gravityValue);
        StartCoroutine(JumpCooldown());
    }

    IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(0.2f);
        //animator.ResetTrigger("Jump");
        canJump = true;
    }

    #endregion

    #region Applying Animation Values
    /*
    private float SetCorrectAnimation()
    {
        float currentAnimationSpeed = animator.GetFloat("Move");
        if (moveSpeed > 1 || moveSpeed < -1)
        {
            if (currentAnimationSpeed < 0.2f)
            {
                currentAnimationSpeed += Time.deltaTime * 2;
                currentAnimationSpeed = Mathf.Clamp(currentAnimationSpeed, 0, 0.2f);
            }
            animator.SetFloat("Move", currentAnimationSpeed);
        }
        else
        {
            if (currentAnimationSpeed < 1) { currentAnimationSpeed += Time.deltaTime * 2; }
            else { currentAnimationSpeed = 1; }
            animator.SetFloat("Move", currentAnimationSpeed);
        }
        return currentAnimationSpeed;
    }
    */


    /// <summary>
    /// This method gets the movement input for both axis and return a float for the animator to use.
    /// </summary>
    /// <returns>Speed in which the player is animating betweek 0, 1</returns>
    private float SetCorrectAnimation()
    {
        float charMoveSpeed = 0;
        float moveX = moveInput.x;
        float moveY = moveInput.y;

        if (moveX == 0 && moveY == 0) charMoveSpeed = 0;
        else if (moveX > 0.8f || moveY > 0.8f) charMoveSpeed = 1;
        else if (moveX < -0.8f || moveY < -0.8f) charMoveSpeed = 1;
        else if (moveX < 0.7f || moveY < 0.7f) charMoveSpeed = 0.5f;
        else if (moveX > -0.7f || moveY > -0.7f) charMoveSpeed = 0.5f;

        //Sprinting
        //if(isSprinting) charMoveSpeed = 2;

        return charMoveSpeed;
    }

    #endregion
}