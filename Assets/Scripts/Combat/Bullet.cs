using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that sends a bullet flying through the scene at a velocity, and then turns off if it collides
/// with a specific tag
/// </summary>
// Bullet requires the GameObject to have an Rigodbody2D component
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [Tooltip("The speed the bullet travels at. Affects the rigidbody of this component.")]
    [SerializeField] private float speed = 20f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnEnable()
    {
        rb.velocity = transform.right * speed;
    }

    /// <summary>
    /// The bullet will be turned off if it hits any object with a collider2D.
    /// However, if the object with the collider2D has a tag of "Ignore",
    /// then the bullet should pass through. Put an "Ignore" tag on an object
    /// if you want the bullet to pass through.
    /// </summary>
    /// <param name="hitInfo">The object the bullet collides with.</param>
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (!hitInfo.CompareTag("Ignore"))
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
