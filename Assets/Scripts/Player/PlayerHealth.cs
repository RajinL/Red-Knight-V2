using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    [Header("Respawn Info")]
    [SerializeField] private float respawnWaitTime = 3f;
    private float respawnTime;

    void Update()
    {
        InvincibilityCheck();
        RespawnCheck();
    }

    private void RespawnCheck()
    {
        if (respawnWaitTime != 0 && GameManager.CurrentPlayerHealth <= 0 && GameManager.CurrentLifeCount > 0)
        {
            if (Time.time >= respawnTime)
            {
                Respawn();
            }
        }
    }

    void Respawn()
    {
        // ******************************************************************************//////                
        // UNLOCK CONTROLS

        transform.position = GameObject.FindGameObjectWithTag("spawn_point").transform.position;

        GameManager.instance.ResetHealth();
        GameManager.instance.UpdateUI();
    }

    public void AddLives(int additionalLife)
    {
        // *******************************************************************************
        //if score reaches threshold
        GameManager.CurrentLifeCount += additionalLife;
        if (GameManager.CurrentLifeCount > GameManager.MaxLifeCount)
        {
            GameManager.CurrentLifeCount = GameManager.MaxLifeCount;
        }
    }

    public override void TakeDamage(int damageAmount)
    {
        if (isInvincible || GameManager.CurrentPlayerHealth <= 0)
        {
            return;
        }
        else
        {
            timeToBecomeHurtAgain = Time.time + invincibilityTime;
            isInvincible = true;

            GameManager.CurrentPlayerHealth -= damageAmount;
            AudioManagerScript.PlaySound("enemyCollidesWithPlayer");
            damageEffect.Damage();

            GameManager.instance.UpdateUI();
            CheckIfObjectIsDead();
        }
    }

    protected override bool CheckIfObjectIsDead()
    {
        if (GameManager.CurrentPlayerHealth <= 0)
        {
            Die();
            return true;
        }
        return false;
    }

    protected override void Die()
    {
        GameManager.CurrentLifeCount -= 1;
        GameManager.instance.UpdateUI();

        if (GameManager.CurrentLifeCount > 0)
        {
            if (respawnWaitTime == 0)
            {
                Respawn();
            }
            else
            {
                // ******************************************************************************//////                
                // PLAY DEATH ANIMATION HERE
                // LOCK CONTROLS
                respawnTime = Time.time + respawnWaitTime;
            }
        }
        // no lives left
        else
        {
            if (UIManager.instance != null)
            {
                GameOver();
            }
        }
    }

    public override int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void GameOver()
    {
        Debug.Log("Player has lost all lives. Game Over!");
        Time.timeScale = 0; // freeze time - temporary fix for locking controls
        // Load Game Over menu
    }
}
