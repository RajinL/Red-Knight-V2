using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public GameObject projectile;
    public Transform firePoint;

    private bool isFlipped = false;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        //Vector3 dir = player.transform.position - transform.position;
        //dir = player.transform.InverseTransformDirection(dir);
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //firePoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
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

}
