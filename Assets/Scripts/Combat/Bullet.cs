using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    /// <summary>
    /// The bullet will be destroyed if it hits any object with a collider2D.
    /// However, if the object with the collider2D has a tag of "Ignore",
    /// then the bullet should pass through. Put an "Ignore" tag on an object
    /// if you want the bullet to pass through.
    /// </summary>
    /// <param name="hitInfo"></param>
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (!hitInfo.CompareTag("Ignore"))
        {
            Destroy(gameObject);
        }
    }
}
