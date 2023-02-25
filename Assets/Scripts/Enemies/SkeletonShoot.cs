using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonShoot : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    //private Rigidbody2D rb;

    public GameObject bone;
    public Transform bonePos;

    private float timer;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        anim.SetBool("isThrowing", false);

        if (timer > 2)
        {
            timer = 0;
            Shoot();
        }
        
    }

    void Shoot()
    {
        Instantiate(bone, bonePos.position, Quaternion.identity);
        anim.SetBool("isThrowing", true);
    }
}
