using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public InputManager inputManager = null;
    public GroundCheck groundCheck = null;
    public SpriteRenderer spriteRenderer = null;
    public Health playerHealth;

    private Rigidbody2D playerRigidbody = null;
    public float movementSpeed = 4.0f;

    public bool grounded
    {
        get
        {
            if (groundCheck != null)
            {
                return groundCheck.IsGrounded();
            }
            else
            {
                return false;
            }
        }
    }

    public float horizontalMovementInput
    {
        get
        {
            if (inputManager != null)
                return inputManager.horizontalMovement;
            else
                return 0;
        }
    }

    private void Start()
    {
        SetupRigidbody();
        SetUpInputManager();
    }

    private void LateUpdate()
    {
        ProcessInput();
    }

    private void SetupRigidbody()
    {
        if (playerRigidbody == null)
        {
            playerRigidbody = GetComponent<Rigidbody2D>();
        }
    }

    private void SetUpInputManager()
    {
        inputManager = InputManager.instance;
        if (inputManager == null)
        {
            Debug.LogError("There is no InputManager set up in the scene for the PlayerController to read from");
        }
    }

    private void ProcessInput()
    {
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        Vector2 movementForce = Vector2.zero;
        if (Mathf.Abs(horizontalMovementInput) > 0)
        {
            movementForce = transform.right * movementSpeed * horizontalMovementInput;
        }
        MovePlayer(movementForce);
    }


    private void MovePlayer(Vector2 movementForce)
    {
        if (grounded)
        {
            float horizontalVelocity = movementForce.x;
            float verticalVelocity = 0;
            playerRigidbody.velocity = new Vector2(horizontalVelocity, verticalVelocity);
        }
        else
        {
            float horizontalVelocity = movementForce.x;
            float verticalVelocity = playerRigidbody.velocity.y;
            playerRigidbody.velocity = new Vector2(horizontalVelocity, verticalVelocity);
        }
    }
}
