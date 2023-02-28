using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health Info")]
    [Tooltip("The health the object starts with after dying")]
    [SerializeField] protected int initialHealth = 3;
    [Tooltip("The current health the player has.")]
    [SerializeField] protected int currentHealth = 3;

    [Header("Invincibility Info")]
    [Tooltip("The time the player is invincible for after being damaged.")]
    [SerializeField] protected float invincibilityTime = 3f;
    [Tooltip("If the player is invincible or not")]
    [SerializeField] protected bool isInvincible = false;
    [Tooltip("The invincibilityTime plus the time at the beginning of being damaged")]
    protected float timeToBecomeHurtAgain = 0;

    [Header("Effects")]
    [SerializeField] protected DamageEffect damageEffect;
    [SerializeField] protected GameObject deathEffect;
    [SerializeField][Range(0, 5)] protected float timeForDeathEffectToDestroy = 1;

    protected Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentHealth = initialHealth;
        SetAllChildrenToParentTag();
    }

    private void SetAllChildrenToParentTag()
    {
        foreach (Transform t in gameObject.transform)
        {
            t.tag = gameObject.tag;
        }
    }

    public virtual int GetCurrentHealth()
    {
        return currentHealth;
    }

    protected virtual void InvincibilityCheck()
    {
        if (timeToBecomeHurtAgain <= Time.time)
        {
            isInvincible = false;
        }
    }

    public virtual void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (damageEffect != null)
        {
            damageEffect.Damage();
        }

        CheckIfObjectIsDead();
    }

    protected virtual bool CheckIfObjectIsDead()
    {
        if (currentHealth <= 0)
        {
            Die();
            return true;
        }
        return false;
    }

    protected virtual void Die()
    {
        if (deathEffect)
        {
            GameObject deathEffectInstance = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(deathEffectInstance, timeForDeathEffectToDestroy);
        }
    }
}
