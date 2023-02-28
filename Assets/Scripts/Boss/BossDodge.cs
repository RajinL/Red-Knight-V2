using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDodge : StateMachineBehaviour
{

    //private Transform transform;

    private Rigidbody2D rb;
    public float speed = 7f;

    public List<Transform> waypoints;
    private int randomPoint = 1;
    public Transform target;
    private Vector2 targetPos;
    private Transform player;
    private BossHealth bossHealth;

    public int panicThreshold = 50;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //transform = animator.GetComponent<Transform>();

        rb = animator.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bossHealth = animator.GetComponent<BossHealth>();

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Waypoint"))
        {
            waypoints.Add(go.transform);
        }

        target = waypoints[randomPoint];
        targetPos = new Vector2(target.position.x, target.position.y);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distanceFromPlayer = Vector2.Distance(rb.position, player.position);
        if (bossHealth.GetCurrentHealth() > panicThreshold && distanceFromPlayer > 1f)
        {
            //Debug.Log("Far enough away");
            animator.SetBool("Panicked", false);
        }
        
        Vector2 newPos = Vector2.MoveTowards(rb.position, targetPos, speed * Time.deltaTime);
        rb.MovePosition(newPos);

        if (Vector2.Distance(rb.position, newPos) < 0.01f)
        {
            randomPoint = randomPoint == 1 ? 0:1;
            target = waypoints[randomPoint];
            targetPos = new Vector2(target.position.x + Random.Range(-10, 5), target.position.y);
        }

        if (player.position.x < 0)
        {
            animator.Play("Idle");
        }
    }


}
