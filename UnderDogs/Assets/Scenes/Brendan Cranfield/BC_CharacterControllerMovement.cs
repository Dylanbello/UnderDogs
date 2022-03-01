using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BC_CharacterControllerMovement : MonoBehaviour
{
    CharacterController controller;
    Transform cam;
    Animator animator;

    private Vector3 playerVelocity;
    private Vector3 moveInput;
    
    public float moveSpeed = 5f;
    public float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    private bool isGrounded;
    

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        
        cam = GetComponentInChildren<Camera>().transform;
        if (!cam) Debug.LogError("Camera cannot be found in child");
        
        canJump = true;
    }

    private void Update()
    {
        Vector3 direction = moveInput.normalized;

        if (direction.magnitude <= 0) animator.SetFloat("Move", 0);
        
        //HandleMovement
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * direction;
            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);

            var animationSpeedMultiplyer = SetCorrectAnimation();
        }
        

        //Stop Falling
        isGrounded = checkGrounded();
        if (playerVelocity.y < 0 && isGrounded) { playerVelocity.y = 0; }

        //Jump
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (isGrounded) jumpCount = 0;
    }

    #region Inputs

    public void Movement(InputAction.CallbackContext context) { moveInput = context.ReadValue<Vector2>(); }
    public void Jump(InputAction.CallbackContext context) { if(context.performed) Jump(); }

    #endregion

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
            if (currentAnimationSpeed < 1)
            {
                currentAnimationSpeed += Time.deltaTime * 2;
            }
            else
            {
                currentAnimationSpeed = 1;
            }
            animator.SetFloat("Move", currentAnimationSpeed);
        }
        return currentAnimationSpeed;
    }



    bool checkGrounded() { return Physics.Raycast(transform.position, Vector3.down, .1f, 1 << LayerMask.NameToLayer("Environment")); }
 

    public float jumpTimer;
    public bool canJump;
    public int jumpCount;

    private void Jump()
    {
        if (jumpCount == 1 || !canJump) return;

        canJump = false;
        jumpCount += 1;

        /*  Spamming the jump button shoots the player up between +2 - +3 on the Y axis */ 
        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3 * gravityValue);

        StartCoroutine(JumpCooldown());
    }

    IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(0.2f);
        canJump = true;
    }
}