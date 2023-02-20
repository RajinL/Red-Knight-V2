using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    [Header("Additional Player Health Info")]
    [Tooltip("The max health the object has.")]
    [SerializeField] private int maxHealth = 5;

    [Header("Invincibility Info")]
    [Tooltip("The time the player is invincible for after being damaged.")]
    [SerializeField] private float invincibilityTime = 3f;
    [Tooltip("If the player is invincible or not")]
    [SerializeField] private bool isInvincible = false;

    [Header("Life Info")]
    [SerializeField] protected int initialLives = 3;
    [SerializeField] protected int currentLives = 3;
    [SerializeField] protected int maximumLives = 5;

    [Header("Respawn Info")]
    [SerializeField] private float respawnWaitTime = 3f;
    private float respawnTime;
    private float timeToBecomeHurtAgain = 0;
    private GameObject respawnLocation;


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

    public override void TakeDamage(int damageAmount)
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

    protected override void Die()
    {
        if (uiManager != null)
        {
            if (gameObject.CompareTag("Player"))
            {
                currentLives -= 1;
                uiManager.SetLifeCount(currentLives);
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
        }
    }

    public void GameOver()
    {
        Debug.Log("Player has lost all lives. Game Over!");
        Time.timeScale = 0; // freeze time - temporary fix for locking controls
        // Load menu
    }
}
