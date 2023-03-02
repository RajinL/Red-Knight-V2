using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    [Header("Respawn Info")]
    [SerializeField] private float respawnWaitTime = 3f;
    private float respawnTime;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

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
        //transform.position = GameManager.instance.spawnPoint.transform.position;
        //UIManager.instance.RestartCurrentScene();
        GameManager.instance.uiManager.RestartCurrentScene();

        if (GetComponent<PlayerMovementV2>())
        {
            GetComponent<PlayerMovementV2>().SetState(PlayerMovementV2.MovementState.idle);
        }

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

///////////////////// Make a dead state for the TopDownMovement and call it here. Should have a death
////////////// animation and turn the physics2d material off. Look at PlayerAnimator.cs for reference
        if (GetComponent<PlayerMovementV2>())
        {
            GetComponent<PlayerMovementV2>().SetState(PlayerMovementV2.MovementState.dead);
        }

        if (GameManager.CurrentLifeCount > 0)
        {
            if (respawnWaitTime == 0)
            {
                Respawn();
            }
            else
            {
                respawnTime = Time.time + respawnWaitTime;
            }
        }
        // no lives left
        else
        {
            GameOver();
        }
    }

    public override int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void GameOver()
    {
        //UIManager.instance.DisplayMessage("Player has lost all lives. Game Over!");
        //UIManager.instance.storyUIButton.onClick.AddListener(RestartGameButton_onClick); //subscribe to the onClick event
        GameManager.instance.uiManager.DisplayMessage("Player has lost all lives. Game Over!");
        GameManager.instance.uiManager.storyUIButton.onClick.AddListener(RestartGameButton_onClick); //subscribe to the onClick event
    }

    /// <summary>
    /// Loads the main menu.
    /// <a href = "https://answers.unity.com/questions/1448790/change-onclick-function-via-script.html"></a>
    /// </summary>
    void RestartGameButton_onClick()
    {
        //UIManager.instance.LoadSceneByName("0_MainMenu");
        GameManager.instance.uiManager.LoadSceneByName("0_MainMenu");
    }
}
