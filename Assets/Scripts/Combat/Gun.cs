using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform gunBarrel;
    public GameObject bulletPrefab;
    public AudioSource shootAudioSource;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, gunBarrel.position, gunBarrel.rotation);
        shootAudioSource.Play();
    }
}
