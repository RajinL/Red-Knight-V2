using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{

    public GameObject playerGun;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;

    private int isRunning;
    private bool isFlipped = false;
    private Transform boss;

    [SerializeField]
    float moveSpeed;

    [SerializeField]
    Vector3 dir;

    [SerializeField]
    float angle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boss = GameObject.FindGameObjectWithTag("Boss").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Fetch the direction keys
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // Move the player
        rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);

        isRunning = movement.x != 0 || movement.y != 0 ? 1 : 0;

        animator.SetInteger("state", isRunning);

        LookAtBoss();

    }

    public void LookAtBoss()
    {
        dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float preAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (transform.position.x < boss.position.x)
        {
            angle = Mathf.Clamp(preAngle, -45, 45);
            if (isFlipped)
            {
                Flip();
            }
        }
        else if (transform.position.x > boss.position.x)
        {
            Vector3 gunFlipped = playerGun.transform.localScale;
            gunFlipped.y = -1f;
            playerGun.transform.localScale = gunFlipped;

            if (preAngle > 0)
            {
                angle = Mathf.Clamp(preAngle, 135, 180);
            } else
            {
                angle = Mathf.Clamp(preAngle, -180, -135);
            }

            if (!isFlipped)
            {
                Flip();
            }
        }
        playerGun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void Flip()
    {
        // Flip player
        Vector3 playerFlipped = transform.localScale;
        playerFlipped.z *= -1f;
        transform.localScale = playerFlipped;
        transform.Rotate(0f, 180f, 0f);

        isFlipped = !isFlipped;

    }

}
