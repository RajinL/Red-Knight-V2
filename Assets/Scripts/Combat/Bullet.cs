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
    public GameObject particleEffect;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        SetParentToObjWithTag("bullet_parent");
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
    /// <param name="collision">The object the bullet collides with.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Ignore"))
        {
            gameObject.SetActive(false);
        }

        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 10)
        {
            GameObject particleEffectInstance = Instantiate(particleEffect, transform.position, Quaternion.identity);
            Destroy(particleEffectInstance, 1);
        }  
    }

    /// <summary>
    /// Sets this object's parent to an object with a tag.
    /// </summary>
    /// <param name="tag">The tag name's as a string</param>
    private void SetParentToObjWithTag(string tag)
    {
        if (GameObject.FindGameObjectWithTag(tag))
        {
            Transform parent = GameObject.FindGameObjectWithTag(tag).transform;
            transform.SetParent(parent);
        }
        else
        {
            Debug.LogWarning("Unable to find " + tag + " tag to set the " + gameObject.name + " game objects's parent" +
                " because the Object Pool prefab is not included in this scene! Insert the Object Pool prefab into the scene" +
                " to organize pooled objects.");
        }
    }
}
