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

    [Header("Effects")]
    [SerializeField] protected DamageEffect damageEffect;
    [SerializeField] protected GameObject deathEffect;
    [SerializeField][Range(0, 2)] protected float timeForDeathEffectToDestroy = 1;

    protected UIManager uiManager;
    protected Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        InitializeUISettings();
    }

    private void InitializeUISettings()
    {
        if (GameObject.FindGameObjectWithTag("ui_manager") != null)
        {
            uiManager = GameObject.FindGameObjectWithTag("ui_manager").GetComponent<UIManager>();
        }
        else
        {
            Debug.LogWarning("UI Manager cannot be found. Make sure that a UI Canvas tagged with \"ui_manager\" is present");
        }
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

    public int GetCurrentHealth()
    {
        return currentHealth;
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
