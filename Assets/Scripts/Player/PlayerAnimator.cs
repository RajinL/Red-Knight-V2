using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header("Collider Settings")]
    [SerializeField] private PhysicsMaterial2D physicsMaterial;

    private Animator animator;
    private PlayerMovementV2 playerMovement;
    private Collider2D bodyCollider;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovementV2>();
        bodyCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (animator == null)
        {
            return;
        }
        if (playerMovement.state == PlayerMovementV2.MovementState.idle)
        {
            bodyCollider.sharedMaterial = physicsMaterial;
            animator.SetBool("isIdle", true);
        }
        else
        {
            animator.SetBool("isIdle", false);
        }

        if (playerMovement.state == PlayerMovementV2.MovementState.jumping)
        {
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }

        if (playerMovement.state == PlayerMovementV2.MovementState.running)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if (playerMovement.state == PlayerMovementV2.MovementState.falling)
        {
            animator.SetBool("isFalling", true);
        }
        else
        {
            animator.SetBool("isFalling", false);
        }

        if (playerMovement.state == PlayerMovementV2.MovementState.dead)
        {
            bodyCollider.sharedMaterial = null;
            animator.SetBool("isDead", true);
        }
        else
        {
            animator.SetBool("isDead", false);
        }
    }
}
