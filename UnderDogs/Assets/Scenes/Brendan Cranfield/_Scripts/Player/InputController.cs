using UnityEngine;
using UnityEngine.InputSystem;

[AddComponentMenu("Player/Player Input Manager")]
public class InputController : MonoBehaviour
{
    DogManager dogManager;
    PlayerInput playerInput;
    BC_CharacterControllerMovement characterControllerMovement;

    void Awake()
    {
        characterControllerMovement = GetComponent<BC_CharacterControllerMovement>();
        dogManager = GetComponent<DogManager>();
        playerInput = GetComponent<PlayerInput>();
    }

    public void Attack(InputAction.CallbackContext context) { dogManager.Explode(); }
    public void Movement(InputAction.CallbackContext context) { characterControllerMovement.moveInput = context.ReadValue<Vector2>(); }
    public void Jump(InputAction.CallbackContext context) { characterControllerMovement.Jump(); }
    public void Pause(InputAction.CallbackContext context)
    { 
        if(!GameManager.Instance.GameIsPaused) 
        { 
            GameManager.Instance.Pause();
            //playerInput.SwitchCurrentActionMap("UI");
        }
        else 
        { 
            GameManager.Instance.Resume();
            //playerInput.SwitchCurrentActionMap("Player");
        }
    }
    public void Sprint(InputAction.CallbackContext context) 
    {
        if (context.started) characterControllerMovement.isSprinting = true;
        else if(context.canceled) characterControllerMovement.isSprinting = false;
    } //Causes the player to break into a sprint.
}