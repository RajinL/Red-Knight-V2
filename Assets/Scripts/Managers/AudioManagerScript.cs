using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    public static AudioClip menuSound, menuSelectSound, skullBombExplosion, skeletonThrowSound, barrelBreakSound, enemyCollisionSound, garlicBombExplosion, treasureChestSound, jumpSound, shootSound, enemyHurtSound, enemyDeathSound;
    static AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        jumpSound = Resources.Load<AudioClip>("playerJump1");
        shootSound = Resources.Load<AudioClip>("playerShoot");
        enemyHurtSound = Resources.Load<AudioClip>("enemyHurtRetro");
        enemyDeathSound = Resources.Load<AudioClip>("enemyDead");

        treasureChestSound = Resources.Load<AudioClip>("treasureChestOpenSound");
        garlicBombExplosion = Resources.Load<AudioClip>("garlicBombExplosion");
        skullBombExplosion = Resources.Load<AudioClip>("skullbomb2");


        enemyCollisionSound = Resources.Load<AudioClip>("enemyHurt2");
        barrelBreakSound = Resources.Load<AudioClip>("barrelBreak");

        skeletonThrowSound = Resources.Load<AudioClip>("skeletonthrow");

        menuSelectSound = Resources.Load<AudioClip>("menuSelect");
        menuSound = Resources.Load<AudioClip>("menuSound");

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
            case "menuSelect":
                audioSource.volume = 1f;
                audioSource.pitch = 1f;
                audioSource.PlayOneShot(menuSelectSound);
                break;
            case "menuSound":
                audioSource.volume = 1f;
                audioSource.pitch = 1f;
                audioSource.PlayOneShot(menuSound);
                break;
            case "shoot":
                audioSource.volume = 0.5f;
                audioSource.pitch = 1.8f;
                audioSource.PlayOneShot(shootSound);
                break;
            case "jump":
                audioSource.volume = 0.3f;
                audioSource.pitch = 1.8f;
                audioSource.PlayOneShot(jumpSound);
                break;
            case "enemyHit":
                audioSource.volume = 1;
                audioSource.pitch = 1.7f;
                audioSource.PlayOneShot(enemyHurtSound);
                break;
            case "enemyCollidesWithPlayer":
                audioSource.volume = 1;
                audioSource.pitch = 2;
                audioSource.PlayOneShot(enemyCollisionSound);
                break;
            case "enemyDead":
                audioSource.volume = 1;
                audioSource.pitch = 1;
                audioSource.PlayOneShot(enemyDeathSound);
                break;
            case "treasureChest":
                audioSource.volume = 1;
                audioSource.pitch = 1;
                audioSource.PlayOneShot(treasureChestSound);
                break;
            case "garlicBomb":
                audioSource.volume = 0.05f;
                audioSource.pitch = 1f;
                audioSource.PlayOneShot(garlicBombExplosion);
                break;
            case "skullBomb":
                audioSource.volume = 1f;
                audioSource.pitch = 1f;
                audioSource.PlayOneShot(skullBombExplosion);
                break;
            case "barrelBreaks":
                audioSource.volume = 0.5f;
                audioSource.pitch = 1f;
                audioSource.PlayOneShot(barrelBreakSound);
                break;
            case "throwbone":
                audioSource.volume = 0.5f;
                audioSource.pitch = 1f;
                audioSource.PlayOneShot(skeletonThrowSound);
                break;

        }
    }
}
