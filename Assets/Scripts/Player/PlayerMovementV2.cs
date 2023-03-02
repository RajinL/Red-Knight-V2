using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementV2 : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float playerSpeed = 4f;
    public bool facingRight = true;

    [Header("Player Jump Info")]
    [Range(1, 10)] [SerializeField] private float jumpPower;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    [SerializeField] private LayerMask jumpableGround;

    [Header("Player Effects")]
    [Tooltip("The dust particle effect that trails behind the player when running or jumping.")]
    public ParticleSystem dust;

    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D coll;
    //private SpriteRenderer sprite;
    private float dirX = 0f;
    public AudioSource jumpAudioSource;
    public enum MovementState { idle, running, jumping, falling, dead }
    public MovementState state = MovementState.idle;
    private bool isJumping;
    //private MovementState state = MovementState.idle;

    // Start is called before the first frame update
    private void Start()
    {
        // Search for this component once during start instead of searching every time you want to use it
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        //sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (state != MovementState.dead)
        {
            if (GameManager.instance.acceptPlayerInput)
            {
                //dirX = Input.GetAxis("Horizontal");
                dirX = Input.GetAxisRaw("Horizontal");
                //Debug.Log(dirX);

                rb.velocity = new Vector2(dirX * playerSpeed, rb.velocity.y);

                //if (Input.GetButtonDown("Jump") && IsGrounded())
                //{
                //    rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                //}

                HandleBetterJump();
                HandleFlipping();
                UpdateAnimationState();
            }

        }
    }

    /// <summary>
    /// https://www.youtube.com/watch?v=7KiK0Aqtmzc
    /// </summary>
    private void HandleBetterJump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            //isJumping = true;
            AudioManagerScript.PlaySound("jump");
            rb.velocity = Vector2.up * jumpPower;
            if (rb.velocity.y < 0.1 && IsGrounded())
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
        }

        else if (rb.velocity.y > 0.1 && !Input.GetButton("Jump"))
        {
            //isJumping = false;
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    public void SetState(MovementState newState)
    {
        state = newState;
    }

    void UpdateAnimationState()
    {
        if (GameManager.CurrentPlayerHealth <= 0)
        {
            SetState(MovementState.dead);
        }

        else if (IsGrounded())
        {
            if (dirX != 0f)
            {
                state = MovementState.running;
            }
            else
            {
                state = MovementState.idle;
            }
        }
        else
        {
            if (rb.velocity.y > .1f)
            {
                SetState(MovementState.jumping);
            }
            else if (rb.velocity.y < -.01f)
            {
                SetState(MovementState.falling);
                CreateDustTrail();
            }
        }
    }

    private void HandleFlipping()
    {
        if (dirX > 0f && !facingRight)
        {
            Flip();
        }
        else if (dirX < 0f && facingRight)
        {
            Flip();
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
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
