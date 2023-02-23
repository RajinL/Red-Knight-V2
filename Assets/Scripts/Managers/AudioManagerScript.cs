using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    public static AudioClip jumpSound, shootSound, enemyHurtSound, enemyDeathSound;
    static AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        jumpSound = Resources.Load<AudioClip>("playerJump1");
        shootSound = Resources.Load<AudioClip>("playerShoot");
        enemyHurtSound = Resources.Load<AudioClip>("enemyHurtRetro");
        enemyDeathSound = Resources.Load<AudioClip>("enemyDead");

        audioSource = GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "shoot":
                audioSource.PlayOneShot(shootSound);
                break;
            case "jump":
                audioSource.PlayOneShot(jumpSound);
                break;
            case "enemyHit":
                audioSource.PlayOneShot(enemyHurtSound);
                break;
            case "enemyDead":
                audioSource.PlayOneShot(enemyDeathSound);
                break;
        }
    }
}
