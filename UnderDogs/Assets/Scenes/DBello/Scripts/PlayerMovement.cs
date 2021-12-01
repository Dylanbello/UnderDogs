using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    public float rotationSpeed, movementSpeed, gravity = 20;
    Vector3 movementVector = Vector3.zero;
    private float desiredRotationAngle = 0;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void HandleMovement(Vector2 input)
    {
        if (controller.isGrounded)
        {
            if(input.y > 0)
            {
                movementVector = transform.forward * movementSpeed;
            }
            else
            {
                movementVector = Vector3.zero;
            }
        }
    }

    public void HandleMovementDirection(Vector3 direction)
    {
        desiredRotationAngle = Vector3.Angle(transform.forward, direction);
        var crossProduct = Vector3.Cross(transform.forward, direction).y;
        if (crossProduct < 0)
        {
            desiredRotationAngle *= -1;
        }
    }

    private void RotatePlayer()
    {
        if(desiredRotationAngle > 10 || desiredRotationAngle < -10)
        {
            transform.Rotate(Vector3.up * desiredRotationAngle * rotationSpeed * Time.deltaTime);
        }
    }

    private void Update()
    {
        if(controller.isGrounded)
        {
            if (movementVector.magnitude > 0) 
            {
                RotatePlayer();
            }
        }
        movementVector.y -= gravity;
        controller.Move(movementVector * Time.deltaTime);
    }
}
