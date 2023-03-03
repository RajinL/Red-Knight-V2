using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    [Header("Score Value")]
    [Tooltip("The score amount this object awards.")]
    [SerializeField] private int scoreValue = 1;

    public bool deathEffectOn = true;
    [SerializeField] private PatrollingEnemies enemyPatrol;
    public Animator deathAnimation;

    Transform parallaxBackgroundLayer;

    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("parallax_background_layer"))
        {
            parallaxBackgroundLayer = GameObject.FindGameObjectWithTag("parallax_background_layer").transform;
        }
    }
    public override void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (damageEffect != null)
        {
            damageEffect.Damage();
            AudioManagerScript.PlaySound("enemyHit");
        }

        if (GameManager.instance.uiManager != null && gameObject.name == "Vampire Boss")
        {
            GameManager.instance.uiManager.SetBossHealth(currentHealth);
        }
        CheckIfObjectIsDead();
    }

    protected override void Die()
    {

        GameManager.CurrentScoreCount += scoreValue;
        GameManager.instance.UpdateUI();

        GetComponentInChildren<Damage>().StopDamaging();
        AudioManagerScript.PlaySound("enemyDead");
        KillEnemy();
    }

    private void KillEnemy()
    {
        if (deathEffectOn == true)
        {
            // Instead of instantiating and destroying particle effect, stop particle system, and reactivate it when needed
            // to improve performance
            // https://answers.unity.com/questions/1558312/how-to-destroy-particle-system-after-instantiating.html
            GameObject deathEffectInstance = Instantiate(deathEffect, transform.position, Quaternion.identity);

            if (parallaxBackgroundLayer != null)
            {
                deathEffectInstance.transform.SetParent(parallaxBackgroundLayer);
            }

            Destroy(gameObject);
            Destroy(deathEffectInstance, timeForDeathEffectToDestroy);
        }
        else
        {
            deathAnimation.SetTrigger("isDead");
            enemyPatrol.moveSpeed = 0;
            gameObject.layer = LayerMask.NameToLayer("DestroyedObjects");
            foreach (Transform child in transform)
            {
                child.gameObject.layer = LayerMask.NameToLayer("DestroyedObjects");
            }
            Destroy(gameObject, 5);
        }
    }
}