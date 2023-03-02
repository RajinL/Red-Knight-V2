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
        animator = gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        player = GameManager.instance.player.transform;
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
        GameObject bat = GetComponent<ObjectPool>().GetPooledObject();
        if (bat != null)
        {
            AudioManagerScript.PlaySound("shoot");
            bat.transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);
            bat.SetActive(true);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == GameManager.instance.player)
        {
            animator.SetBool("Panicked", true);
        }
    }
}
