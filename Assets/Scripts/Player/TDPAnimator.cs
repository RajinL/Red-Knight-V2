using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDPAnimator : MonoBehaviour
{
   
    private Animator animator;
    private TopDownMovement playerMovement;



    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<TopDownMovement>();
    }

    void Update()
    {
        if (animator == null)
        {
            return;
        }
        

        if (playerMovement.state == TopDownMovement.MovementState.dead)
        {

            GameManager.instance.isPlayerDead = true;
            animator.SetBool("isDead", true);
        }
        else
        {
            animator.SetBool("isDead", false);
        }
    }
}
