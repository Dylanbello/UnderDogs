using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(CharacterController))]
public class BC_CharacterControllerMovement : MonoBehaviour
{
    CharacterController controller;
    [SerializeField] Transform cam;
    Animator animator;

    private Vector3 playerVelocity;
    private Vector3 moveInput;
    
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    [SerializeField] float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    public bool grounded;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        
        canJump = true;
    }

    private void Update()
    {
        CheckFalling();
        Movement();
        Rotation();
        SetAnimation();
    }

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
        if(moveInput == Vector3.zero) { return; }

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
        if (jumpCount == 1 || !canJump) return;

        canJump = false;
        jumpCount += 1;

        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3 * gravityValue);
        StartCoroutine(JumpCooldown());
    }

    IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(0.2f);
        canJump = true;
        Debug.Log("Can jump");
    }

    #endregion

    #region Applying Animation Values

    private float SetCorrectAnimation()
    {
        float currentAnimationSpeed = animator.GetFloat("Move");
        if (moveSpeed > 1 || moveSpeed < -1)    //moveSpeed isn't being changed anywhere
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

    //Experimenting with simpler animation code
    void SetAnimation()
    {
        float verticalMovement = moveInput.y;

        if(moveInput.z != 0 && moveInput.y == 0) { verticalMovement = moveInput.z / 2; }

        animator.SetFloat("Move", verticalMovement);
    }

    #endregion
}