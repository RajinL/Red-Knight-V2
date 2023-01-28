using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public float horizontalMovement;
    public float verticalMovement;

    public float pauseButton = 0;

    // The global instance for other scripts to reference
    public static InputManager instance;

    private void Awake()
    {
        // Set up the singleton instance of this
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        ResetValuesToDefault();
    }

    void ResetValuesToDefault()
    {
        horizontalMovement = 0;
        verticalMovement = 0;

        pauseButton = 0;
    }

    public void GetMovementInput(InputAction.CallbackContext callbackContext)
    {
        Vector2 movementVector = callbackContext.ReadValue<Vector2>();
        horizontalMovement = movementVector.x;
        verticalMovement = movementVector.y;
    }

    public void GetPauseInput(InputAction.CallbackContext callbackContext)
    {
        pauseButton = callbackContext.ReadValue<float>();
    }
}