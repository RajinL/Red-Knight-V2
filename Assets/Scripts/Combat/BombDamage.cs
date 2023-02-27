using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that launches a bomb flying through the scene at a velocity, explodes after a set time,
/// and then turns off after a set time.
/// </summary>
// BombDamage requires the GameObject to have ObjectPool and Rigidbody2D components
[RequireComponent(typeof(ObjectPool))]
[RequireComponent(typeof(Rigidbody2D))]
public class BombDamage : MonoBehaviour
{
    [Header("Bomb Settings")]
    [Tooltip("The speed that affects this object's rigidbody velocity.")]
    [SerializeField] public float speedThrown = 10f;
    [Tooltip("The amount of damage this object deals to another object with a Health component.")]
    [SerializeField] private int damageValue = 2;
    [Tooltip("The blast radius for this object when it explodes")]
    [SerializeField] private float explodeRangeRadius;

    [Header("Lifetime Settings")]
    [Tooltip("Time it takes for this object to explode and deal damage")]
    [SerializeField] public float detonationTime;
    [Tooltip("Time it takes for this object to turn off after it's already exploded")]
    [SerializeField] public float timeToTurnOffAfterDet;

    [Header("Layers to Damage")]
    [Tooltip("Which layers this object will damage.")]
    [SerializeField] private LayerMask WhatToDestroy;

    public Rigidbody2D rb;
    public float initialDetTime;
    public float totalTimeToTurnOff;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        initialDetTime = detonationTime;
    }

    void Start()
    {
        InitializeBomb();
    }

    private void OnEnable()
    {
        InitializeBomb();
    }

    /// <summary>
    /// Intializes this bomb at the Start and OnEnable functions - resets the detonation time,
    /// sets the bomb's rigidbody's velocity multiplied by a speed value, and sets the actual
    /// time that bomb is turned off
    /// </summary>
    public virtual void InitializeBomb()
    {
        detonationTime = initialDetTime;
        rb.velocity = transform.right * speedThrown;
        totalTimeToTurnOff = detonationTime + timeToTurnOffAfterDet;
    }

    private void Update()
    {
        HandlesDetonation();
        TurnsObjectOff();
    }

    /// <summary>
    /// Starts a timer counting down to 0, and then turns the game object off.
    /// </summary>
    private void HandlesDetonation()
    {
        if (detonationTime <= 0)
        {
            Detonate(damageValue, explodeRangeRadius);
        }

        if (detonationTime > 0)
        {
            detonationTime -= Time.deltaTime;
        }
    }
    /// <summary>
    /// Starts a timer counting down to 0, and then detonates the game object off by
    /// calling the <see cref="Detonate">Detonate</see> function. 
    /// </summary>
    private void TurnsObjectOff()
    {
        if (totalTimeToTurnOff <= 0)
        {
            gameObject.SetActive(false);
        }

        if (totalTimeToTurnOff > 0)
        {
            totalTimeToTurnOff -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Object deals damage and creates an explosion effect.
    /// </summary>
    /// <param name="damageValue">The amount of damage this object will do.</param>
    /// <param name="explodeRangeRadius">The radius of the explosion.</param>
    private void Detonate(int damageValue, float explodeRangeRadius)
    {
        DealDamage(damageValue, explodeRangeRadius);
        AudioManagerScript.PlaySound("garlicBomb");
        CreateExplosionEffect();
    }

    /// <summary>
    /// Deals damage to any object that has a Health component within this object's explodeRange parameter.
    /// </summary>
    /// <param name="damageValue">The amount of damage this object will do.</param>
    /// <param name="explodeRangeRadius">The radius of the explosion.</param>
    private void DealDamage(int damageValue, float explodeRangeRadius)
    {
        Collider2D[] ObjectsToDestroy = Physics2D.OverlapCircleAll(transform.position, explodeRangeRadius, WhatToDestroy);
        for (int i = 0; i < ObjectsToDestroy.Length; i++)
        {
            if (ObjectsToDestroy[i].GetComponent<Health>())
            {
                ObjectsToDestroy[i].GetComponent<Health>().TakeDamage(damageValue);
            }
        }
    }

    /// <summary>
    /// Draws an outline around this object to visualize in the scene window.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, explodeRangeRadius);
    }

    /// <summary>
    /// Creates explosion effect by using the Object Pool Script. This function sets each
    /// individual pooled object's position and rotation to this transform, and sets each object
    /// as active.
    /// </summary>
    void CreateExplosionEffect()
    {
        GameObject bombExplosion = GetComponent<ObjectPool>().GetPooledObject();
        if (bombExplosion != null)
        {
            bombExplosion.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);
            bombExplosion.SetActive(true);
        }
    }
}
