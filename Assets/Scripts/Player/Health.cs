using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health Info")]
    [Tooltip("The health the object starts with after dying")]
    [SerializeField] private int initialHealth = 3;
    [Tooltip("The current health the player has.")]
    public int currentHealth = 1;
    [Tooltip("The max health the object has.")]
    [SerializeField] private int maxHealth = 5;
    private UIManager uiManager;

    [Header("Invincibility Info")]
    [Tooltip("The time the player is invincible for after being damaged.")]
    [SerializeField] private float invincibilityTime = 3f;
    [Tooltip("If the player is invincible or not")]
    [SerializeField] private bool isInvincible = false;

    [Header("Life Info")]
    [SerializeField] public int initialLives = 3;
    [SerializeField] public int currentLives = 3;
    [SerializeField] private int maximumLives = 5;


    [Header("Respawn Info")]
    [SerializeField] private float respawnWaitTime = 3f;
    private float respawnTime;
    private float timeToBecomeHurtAgain = 0;
    private GameObject respawnLocation;

    [Header("Score Value")]
    [Tooltip("The score amount this object awards.")]
    [SerializeField] private int scoreValue = 1;

    [Header("Effects")]
    [SerializeField] private DamageEffect damageEffect;
    [SerializeField] private GameObject enemyDeathEffect;

    private void Awake()
    {
        InitializeUISettings();
    }

    private void InitializeUISettings()
    {
        if (GameObject.FindGameObjectWithTag("ui_manager") != null)
        {
            uiManager = GameObject.FindGameObjectWithTag("ui_manager").GetComponent<UIManager>();
        }

        if (gameObject.CompareTag("Player"))
        {
            if (uiManager != null)
            {
                uiManager.SetMaxHealth(maxHealth);
                uiManager.SetLifeCount(3);
            }
            else
            {
                Debug.LogWarning("UI Manager cannot be found. Make sure that a UI Canvas tagged with \"ui_manager\" is present");
            }
        }
    }

    void Update()
    {
        InvincibilityCheck();
        RespawnCheck();
    }
    
    private void RespawnCheck()
    {
        if (respawnWaitTime != 0 && currentHealth <= 0 && currentLives > 0)
        {
            if (Time.time >= respawnTime)
            {
                Respawn();
            }
        }
    }

    private void InvincibilityCheck()
    {
        if (timeToBecomeHurtAgain <= Time.time)
        {
            isInvincible = false;
        }
    }

    void Respawn()
    {
        // ******************************************************************************//////                
        // UNLOCK CONTROLS

        transform.position = GameManager.instance.gmRespawnLocation.transform.position;
        currentHealth = initialHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        if (isInvincible || currentHealth <= 0)
        {
            return;
        }
        else
        {
            timeToBecomeHurtAgain = Time.time + invincibilityTime;
            isInvincible = true;
            currentHealth -= damageAmount;
            damageEffect.Damage();

            if (gameObject.CompareTag("Player"))
            {
                if (uiManager != null)
                {
                    uiManager.SetHealth(currentHealth);
                }
            }
            CheckIfPlayerIsDead();
        }
    }

    public void AddLives(int additionalLife)
    {
        // *******************************************************************************
        //if score reaches threshold

        if (gameObject.CompareTag("Player"))
        {
            currentLives += additionalLife;
            if (currentLives > maximumLives)
            {
                currentLives = maximumLives;
            }
        }
    }

    bool CheckIfPlayerIsDead()
    {
        if (currentHealth <= 0)
        {
            Die();
            return true;
        }
        return false;
    }

    void Die()
    {
        if (uiManager != null)
        {
            if (gameObject.CompareTag("Player"))
            {
                currentLives -= 1;
                uiManager.SetLifeCount(currentLives);
            }

            else if (gameObject.CompareTag("shootable"))
            {
                GameManager.currentScoreCount += scoreValue;
                uiManager.setScoreCount(GameManager.currentScoreCount);




// **************************************TO FIX - adds extra life upon reaching score threshold

                //if (GameManager.currentScoreCount == GameManager.scoreLifeThreshold)
                //{
                //    currentLives += 1;
                //    GameManager.currentLifeCount += currentLives;
                //    uiManager.SetLifeCount(currentLives);

                //    Debug.Log("Should add extra life here");
                //    if (gameObject.CompareTag("Player"))
                //    {
                //        Debug.Log("Adding 1 extra life");
                //        AddLives(1);
                //        uiManager.SetLifeCount(currentLives);
                //    }
                //}
            }
        }

        //if (uiManager != null)
        //{
        //    uiManager.SetLifeCount(currentLives);
        //}
        if (currentLives > 0)
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
            if (CompareTag("Player"))
            {
                if (uiManager != null)
                {
                    GameOver();
                }
            }
            else if (CompareTag("shootable"))
            {
                KillEnemy();
            }
        }
    }

    private void KillEnemy()
    {
        if (enemyDeathEffect != null) Instantiate(enemyDeathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void GameOver()
    {
        Debug.Log("Player has lost all lives. Game Over!");
        Time.timeScale = 0; // freeze time - temporary fix for locking controls
        // Load menu
    }
}
