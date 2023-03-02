using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileHealth : Health
{
    [Header("Score Value")]
    [Tooltip("The score amount this object awards.")]
    [SerializeField] private int scoreValue = 1;

    public bool deathEffectOn = true;
    public Animator deathAnimation;

    private void OnEnable()
    {
        currentHealth = initialHealth;
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
            gameObject.SetActive(false);
            deathEffectInstance.SetActive(false); // use a coroutine with a timeForDeathEffectToDestroy
        }
        else
        {
            if (deathAnimation) deathAnimation.SetTrigger("isDead");
            gameObject.SetActive(false);
        }
    }
}