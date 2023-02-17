using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{

    public GameObject playerGun;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;

    private float dirX = 0f;
    private float dirY = 0f;

    [SerializeField]
    float moveSpeed;

    [SerializeField]
    Vector3 dir;

    [SerializeField]
    float angle;

    private enum MovementState { idle, running }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Fetch the direction keys
        //movement.x = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");

        dirX = Input.GetAxisRaw("Horizontal");
        dirY = Input.GetAxisRaw("Vertical");

        UpdateAnimationState();
    }

    void FixedUpdate()
    {
        // Move the player
        rb.velocity = new Vector2(dirX * moveSpeed, dirY * moveSpeed);

        // Look towards the mouse
        dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        angle = Mathf.Clamp(Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg, -45, 45);
        playerGun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f || dirX < 0)
        {
            state = MovementState.running;
        }
        else if (dirY < 0f || dirY > 0f)
        {
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
        }

        anim.SetInteger("state", (int)state);
    }
}
