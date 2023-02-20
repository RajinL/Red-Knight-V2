using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollidesWithPlayer : MonoBehaviour
{
    // Enemy attack
    [SerializeField] private int attackDamage = 20;
    [SerializeField] private float attackSpeed = 0.5f;
    private float allowAttack;
    private Transform target;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (attackSpeed <= allowAttack)
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(attackDamage);
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
        if (collision.gameObject.CompareTag("Player"))
        {
            target = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target = null;
        }
    }
}
