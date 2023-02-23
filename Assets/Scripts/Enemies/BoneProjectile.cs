using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneProjectile : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
