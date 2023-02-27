using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    public static PlayerHealth instance = null;

    [Header("Invincibility Info")]
    [Tooltip("The time the player is invincible for after being damaged.")]
    [SerializeField] private float invincibilityTime = 3f;
    [Tooltip("If the player is invincible or not")]
    [SerializeField] private bool isInvincible = false;

    [Header("Respawn Info")]
    [SerializeField] private float respawnWaitTime = 3f;
    private float respawnTime;
    private float timeToBecomeHurtAgain = 0;

    [Header("UI Manager")]
    [Tooltip("The UI manager to reference")]
    [SerializeField] private new UIManager uiManager;

    // https://answers.unity.com/questions/1174255/since-onlevelwasloaded-is-deprecated-in-540b15-wha.html
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckIfUIisReferenced();
        UpdateUI();
    }

    private void CheckIfUIisReferenced()
    {
        if (uiManager == null)
        {
            uiManager = GameObject.FindGameObjectWithTag("ui_manager").GetComponent<UIManager>();
        }
        else return;
    }

    void Update()
    {
        InvincibilityCheck();
        RespawnCheck();
    }

    private void RespawnCheck()
    {
        if (respawnWaitTime != 0 && currentHealth <= 0 && GameManager.CurrentLifeCount > 0)
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

    private void UpdateUI()
    {
        uiManager.SetPlayerHealth(currentHealth);
        GameManager.UpdateUI();
    }

    void Respawn()
    {
        // ******************************************************************************//////                
        // UNLOCK CONTROLS

        transform.position = GameObject.FindGameObjectWithTag("spawn_point").transform.position;

        currentHealth = initialHealth;
        UpdateUI();
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
        if (isInvincible || currentHealth <= 0)
        {
            return;
        }
        else
        {
            timeToBecomeHurtAgain = Time.time + invincibilityTime;
            isInvincible = true;
            currentHealth -= damageAmount;
            AudioManagerScript.PlaySound("enemyCollidesWithPlayer");
            damageEffect.Damage();

            if (uiManager != null)
            {
                UpdateUI();
            }
            CheckIfObjectIsDead();
        }
    }

    protected override void Die()
    {
        if (uiManager != null)
        {
            GameManager.CurrentLifeCount -= 1;
            UpdateUI();
        }

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
            if (uiManager != null)
            {
                GameOver();
            }
        }
    }

    public void GameOver()
    {
        Debug.Log("Player has lost all lives. Game Over!");
        Time.timeScale = 0; // freeze time - temporary fix for locking controls
        // Load Game Over menu
    }
}
