using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component on gameobjects with colliders which determines if there is
/// a collider overlapping them which is on a specific layer.
/// Used to check for ground.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class GroundCheck : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider2d = null;

    private void Start()
    {
        GetCollider();
    }
    public void GetCollider()
    {
        if (boxCollider2d == null)
        {
            boxCollider2d = gameObject.GetComponent<BoxCollider2D>();
        }
    }

    //https://www.youtube.com/watch?v=c3iEl5AwUF8
    public bool IsGrounded()
    {
        // Ensure the collider is assigned
        if (boxCollider2d == null)
        {
            GetCollider();
        }

        float extraHeightText = 0.1f;
        RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider2d.bounds.center, Vector2.down, boxCollider2d.bounds.extents.y + extraHeightText);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        } else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(boxCollider2d.bounds.center, Vector2.down * (boxCollider2d.bounds.extents.y + extraHeightText));
        return raycastHit.collider != null;
    }
}
