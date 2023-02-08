using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementV2 : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    public bool facingRight = true;

    private SpriteRenderer sprite;
    private float dirX = 0f;

    [SerializeField]
    private float playerSpeed = 4f;

    [SerializeField]
    private float jumpPower = 9f;

    public ParticleSystem dust;

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
        //dirX = Input.GetAxis("Horizontal");
        dirX = Input.GetAxisRaw("Horizontal");
        //Debug.Log(dirX);

        rb.velocity = new Vector2(dirX * playerSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        UpdateAnimationState();
    }

    void UpdateAnimationState()
    {

        MovementState state;

        if (dirX > 0f && !facingRight)
        {
            Flip();
        }
        else if (dirX < 0f && facingRight)
        {
            Flip();
        }

        if (dirX > 0f)
        { 
            state = MovementState.running;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
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
            CreateDustTrail();
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    void Flip()
    {
        //CreateDustTrail();
        transform.Rotate(0f, 180f, 0f);

        //Vector3 currentScale = gameObject.transform.localScale;
        //currentScale.x *= -1;
        //gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    void CreateDustTrail()
    {
        dust.Play();
    }
}
