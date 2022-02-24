using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    Animator animator;

    private Vector3 playerVelocity;
    
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
        cam = Camera.main.transform;

        //Check for controller reference
        if (!controller) 
        {
            Debug.LogError("Didn't find CharacterController");
        }
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude <= 0) animator.SetFloat("Move", 0);
        
        //HandleMovement
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);

            var animationSpeedMultiplyer = SetCorrectAnimation();
        }
        

        //Stop Falling
        isGrounded = checkGrounded();
        if (playerVelocity.y < 0 && isGrounded)
        {
            playerVelocity.y = 0;
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

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



    bool checkGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, .1f, 1 << LayerMask.NameToLayer("Environment"));
    }

    private void Jump()
    {
        if (isGrounded)
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        
        
    }

    
}
