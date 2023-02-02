using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    private SpriteRenderer sprite;
    private float dirX = 0f;

    [SerializeField]
    private float playerSpeed = 4f;

    [SerializeField]
    private float jumpPower = 9f;

    private enum MovementState { idle, running, jumping, falling }
    //private MovementState state = MovementState.idle;

    // Start is called before the first frame update
    private void Start()
    {
        // Search for this component once during start instead of searching every time you want to use it
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(dirX * playerSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            //anim.SetBool("isJumping", true);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {

        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.01f)
        {
            state = MovementState.falling;
        } 

        anim.SetInteger("state", (int)state);
    }
}
