﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollidesWithPlayer : MonoBehaviour
{
    // Enemy attack
    [SerializeField] private int attackDamage = 20;
    [SerializeField] private float attackSpeed = 0.5f;
    private float allowAttack;
    private Transform target;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == GameManager.instance.player)
        {
            if (attackSpeed <= allowAttack)
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(attackDamage);
                allowAttack = 0f;
                AudioManagerScript.PlaySound("enemyCollidesWithPlayer");

            }
            else
            {
                allowAttack += Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameManager.instance.player)
        {
            target = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == GameManager.instance.player)
        {
            target = null;
        }
    }
}
