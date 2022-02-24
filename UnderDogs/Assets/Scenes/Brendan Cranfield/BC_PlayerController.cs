using UnityEngine;
using UnityEngine.InputSystem;

public class BC_PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    Animator animator;

    private Vector3 playerVelocity;
    Vector3 direction;


    public float moveSpeed = 5f;
    public float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    private bool isGrounded;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;

        if (TryGetComponent(out Animator anim)) animator = anim;
        //Check for controller reference
        if (!controller) 
        {
            Debug.LogError("Didn't find CharacterController");
        }
    }

    private void Update()
    {
        if (direction.magnitude <= 0) animator.SetFloat("Move", 0);

        //Stop Falling
        isGrounded = checkGrounded();
        if (playerVelocity.y < 0 && isGrounded)
        {
            playerVelocity.y = 0;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        //HandleMovement
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
        }

        if(direction.magnitude > 0)
        {
            animator.SetFloat("Move", 0.5f);
        }
        else if(direction.magnitude > 0 && Input.GetKeyDown(KeyCode.LeftShift))
            animator.SetFloat("Move", 1f);
    }

    //Recieves vector2 input from Player Input component.
    public void Movement(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }


    //Recieves jump input from Player Input component.
    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("jump");
        HandleJump();
    }

    bool checkGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.5f, 1 << LayerMask.NameToLayer("Environment"));
    }

    void HandleJump()
    {
        if (isGrounded)
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        Debug.Log("Jump2");
    }
}