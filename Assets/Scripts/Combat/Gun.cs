using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that allows the player to shoot a bullet
/// </summary>
// Gun requires the GameObject to have an ObjectPool component
[RequireComponent(typeof(ObjectPool))]
public class Gun : MonoBehaviour
{
    [Header("Gun Settings")]
    [Tooltip("The location the bullet is turned on at.")]
    [SerializeField] private Transform gunBarrel;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    /// <summary>
    /// Uses the Object Pool Script. This function sets each individual pooled
    /// object's position and rotation to this transform, and sets each object
    /// as active. The Bullet class handles the behaviour of the bullet.
    /// </summary>
    void Shoot()
    {
        GameObject bullet = GetComponent<ObjectPool>().GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.SetPositionAndRotation(gunBarrel.transform.position, gunBarrel.transform.rotation);
            bullet.SetActive(true);
        }
    }
}
