using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public GameObject projectile;
    public Transform firePoint;

    private bool isFlipped = false;

    private Transform player;
    private Animator animator;



    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = gameObject.GetComponent<Animator>();
    }


    public void LookAtPlayer()
    {

        if (transform.position.x > player.position.x && isFlipped)
        {
            Flip();
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            Flip();
        }
    }

    private void Flip()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
        transform.localScale = flipped;
        transform.Rotate(0f, 180f, 0f);
        isFlipped = !isFlipped;

    }

    public void Attack()
    {
        Instantiate(projectile, firePoint.position, firePoint.rotation);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.SetBool("Panicked", true);

        }
    }


}
