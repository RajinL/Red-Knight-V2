using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollidesWithPlayer : MonoBehaviour
{
    // Enemy attack
    [SerializeField] private float attackDamage = 20f;
    [SerializeField] private float attackSpeed = 0.5f;
    private float allowAttack;
    private Transform target;

    public AudioSource enemyHitAudioSource;


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (attackSpeed <= allowAttack)
            {
                collision.gameObject.GetComponent<Health>().UpdateHealth(-attackDamage);
                allowAttack = 0f;
            }
            else
            {
                allowAttack += Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            target = collision.transform;
            enemyHitAudioSource.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            target = null;
        }
    }
}
