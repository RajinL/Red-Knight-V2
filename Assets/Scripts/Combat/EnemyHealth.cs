using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    [Header("Score Value")]
    [Tooltip("The score amount this object awards.")]
    [SerializeField] private int scoreValue = 1;

    [SerializeField] private PatrollingEnemies enemyPatrol;

    public override void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (damageEffect != null)
        {
            damageEffect.Damage();
        }

        if (uiManager != null && gameObject.name == "Vampire Boss")
        {
            uiManager.SetBossHealth(currentHealth);
        }
        CheckIfObjectIsDead();
    }

    protected override void Die()
    {
        if (uiManager != null)
        {
            GameManager.currentScoreCount += scoreValue;
            uiManager.SetScoreCount(GameManager.currentScoreCount);
        }
        GetComponentInChildren<Damage>().StopDamaging();
        KillEnemy();
    }

    private void KillEnemy()
    {
        if (deathEffect)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            if (animator.GetBool("IsDead")) animator.SetTrigger("isDead");

            if (enemyPatrol != null)
            {
                enemyPatrol.moveSpeed = 0;

            }
            else
            {
                Debug.LogWarning("Trying to set enemyPatrol.moveSpeed to 0, but " + gameObject.name + " at " + transform.position + " does not have a reference to PatrollingEnemies::enemyPatrol. " +
                    "If this object requres a patrol, make a reference.");
            }
            gameObject.layer = LayerMask.NameToLayer("DestroyedObjects");
            Destroy(gameObject, 5);
        }
    }
}