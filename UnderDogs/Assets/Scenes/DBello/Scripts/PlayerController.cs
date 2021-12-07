using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Active - In Use, Dyls Scene
public class PlayerController : MonoBehaviour
{
    IInput input;
    PlayerMovement movement;

    private void OnEnable()
    {
        input = GetComponent<IInput>();
        movement = GetComponent<PlayerMovement>();

        input.MovementDirectionInput += movement.HandleMovementDirection;
        input.MovementInput += movement.HandleMovement;
    }

    private void OnDisable()
    {
        input.MovementDirectionInput += movement.HandleMovementDirection;
        input.MovementInput += movement.HandleMovement;
    }
}
