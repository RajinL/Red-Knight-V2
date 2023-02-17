using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth = 0f;
    public float maximumHealth = 100f;

    public float currentLives = 3f;
    public float maximumLives = 5f;

    [SerializeField] private DamageEffect damageEffect;
    //[SerializeField] private DamageEffect positiveEffect;
    //public GameObject deathEffect;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maximumHealth;
    }

    public void UpdateHealth(float modifier)
    {
        currentHealth += modifier;

        damageEffect.Damage();

        //if (currentHealth + modifier < currentHealth)
        //{
        //    damageEffect.Damage();
        //} else if (currentHealth + modifier > currentHealth)
        //{
        //    positiveEffect.Damage();
        //}

        if (currentHealth > maximumHealth)
        {
            currentHealth = maximumHealth;
        } else if (currentHealth <= 0f)
        {
            currentHealth = 0f;
        }
    }
}
