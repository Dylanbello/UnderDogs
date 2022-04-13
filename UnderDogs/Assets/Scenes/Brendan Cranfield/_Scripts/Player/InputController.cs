using UnityEngine;
using UnityEngine.InputSystem;

[AddComponentMenu("Player/Player Input Manager")]
public class InputController : MonoBehaviour
{
    DogManager dogManager;
    BC_CharacterControllerMovement characterControllerMovement;

    void Awake()
    {
        characterControllerMovement = GetComponent<BC_CharacterControllerMovement>();
        dogManager = GetComponent<DogManager>();
    }

    public void Attack(InputAction.CallbackContext context) { dogManager.Explode(); }
    public void Movement(InputAction.CallbackContext context) { characterControllerMovement.moveInput = context.ReadValue<Vector2>(); }
    public void Jump(InputAction.CallbackContext context) { characterControllerMovement.animator.SetTrigger("Jump"); }
    public void Pause(InputAction.CallbackContext context)
    { 
        if(!GameManager.Instance.GameIsPaused) { GameManager.Instance.Pause(); }
        else { GameManager.Instance.Resume(); }
    }
    public void Sprint(InputAction.CallbackContext context) 
    {
        if (context.started) characterControllerMovement.isSprinting = true;
        else if(context.canceled) characterControllerMovement.isSprinting = false;
    } //Causes the player to break into a sprint.
}