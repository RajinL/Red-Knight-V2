using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopDownMovement : MonoBehaviour
{

    public GameObject playerGun;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;

    private int isRunning;
    private bool isFlipped = false;
    public Transform target;

    [SerializeField]
    float moveSpeed;

    [SerializeField]
    Vector3 dir;

    [SerializeField]
    float angle;

    public enum MovementState { idle, dead }
    public MovementState state = MovementState.idle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        UpdateTarget();
    }

    // https://answers.unity.com/questions/1174255/since-onlevelwasloaded-is-deprecated-in-540b15-wha.html
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateTarget();
    }

    private void UpdateTarget()
    {
        if (GameObject.FindGameObjectWithTag("Boss"))
        {
            target = GameObject.FindGameObjectWithTag("Boss").transform;
        }
        else if (GameObject.FindGameObjectWithTag("Finish"))
        {
            target = GameObject.FindGameObjectWithTag("Finish").transform;
        }
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
        if (state != MovementState.dead)
        {
            if (GameManager.instance.acceptPlayerInput)
            {
                // Move the player
                rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);

                isRunning = movement.x != 0 || movement.y != 0 ? 1 : 0;

                animator.SetInteger("state", isRunning);

                FindTarget();
                UpdateAnimationState();
            }
        }
    }


    private void FindTarget()
    {
        if (CheckIfBossExists())
        {
            LookAtBoss();
        }
        else if (GameObject.FindGameObjectWithTag("Finish"))
        {
            target = GameObject.FindGameObjectWithTag("Finish").transform;
        }
    }

    /// <summary>
    /// If the boss exists and has not been destroyed
    /// </summary>
    private bool CheckIfBossExists()
    {
        if (target != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void LookAtBoss()
    {
        // Fetch the mouse position and work out the angle
        dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float preAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Vector3 gunFlipped = playerGun.transform.localScale;

        // If the player is on the left of the boss
        if (transform.position.x < target.position.x)
        {
            // Ensure the gun is the right way up
            gunFlipped.y = 1f;

            angle = Mathf.Clamp(preAngle, -45, 45);

            // Flip the sprint
            if (isFlipped)
            {
                Flip();
            }
        }
        // If the player is on the right of the booss
        else if (transform.position.x > target.position.x)
        {
            // Flip the gun sprint
            gunFlipped.y = -1f;

            // If the mouse is in the top left quadrant of the screen
            if (preAngle > 0)
            {
                angle = Mathf.Clamp(preAngle, 135, 180);
            } else // The mouse is in the bottom right of the screen
            {
                angle = Mathf.Clamp(preAngle, -180, -135);
            }

            // Flip the sprit
            if (!isFlipped)
            {
                Flip();
            }
        }
        playerGun.transform.localScale = gunFlipped;
        playerGun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void Flip()
    {
        Vector3 playerFlipped = transform.localScale;
        playerFlipped.z *= -1f;
        transform.localScale = playerFlipped;
        transform.Rotate(0f, 180f, 0f);

        isFlipped = !isFlipped;
    }

    public void SetState(MovementState newState)
    {
        state = newState;
    }

    void UpdateAnimationState()
    {
        if (GameManager.CurrentPlayerHealth <= 0)
        {
            playerGun.SetActive(false);

            SetState(MovementState.dead);

        }


    }
}
