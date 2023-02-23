using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Enemy health
    public int health = 100;
    [SerializeField] private DamageEffect damageEffect;
    public GameObject deathEffect;
    public bool deathEffectOn = true;
    public Animator animator;

    public PatrollingEnemies enemyPatrol;



    public void TakeDamage(int damage)
    {
        health -= damage;
        damageEffect.Damage();
        AudioManagerScript.PlaySound("enemyHit");

        if (health <= 0)
        {
            Die();
            AudioManagerScript.PlaySound("enemyDead");

        }
    }

    void Die()
    {
        if (deathEffectOn == true)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            animator.SetTrigger("isDead");
            enemyPatrol.moveSpeed = 0;
            gameObject.layer = LayerMask.NameToLayer("DestroyedObjects");
            Destroy(gameObject, 5);
        }   
        
    }
}
