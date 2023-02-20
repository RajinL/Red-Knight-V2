using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Health : MonoBehaviour
{
    protected UIManager uiManager;

    [Header("Health Info")]
    [Tooltip("The health the object starts with after dying")]
    [SerializeField] protected int initialHealth = 3;
    [Tooltip("The current health the player has.")]
    [SerializeField] protected int currentHealth = 3;

    [Header("Effects")]
    [SerializeField] protected DamageEffect damageEffect;
    [SerializeField] protected GameObject deathEffect;

    protected Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentHealth = initialHealth;
    }

    public virtual void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        damageEffect.Damage();

        CheckIfPlayerIsDead();
    }

    protected virtual bool CheckIfPlayerIsDead()
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
        Debug.Log(gameObject.name + " has " + currentHealth + " current health. Destroying...");
        Destroy(gameObject);
    }
}
