using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDamage : MonoBehaviour
{
    //public ParticleSystem explosion;

    public float speed = 10f;
    public int damage = 50;
    public Rigidbody2D rb;

    public float ExplodeRange;
    public float DetonationTime;
    public float TimeToDestroy;
    float ActualTimeToDestroy;

    public ParticleSystem explosionEffect;

    public LayerMask WhatToDestroy;

    void Start()
    {
        rb.velocity = transform.right * speed;

        ActualTimeToDestroy = DetonationTime + TimeToDestroy;
    }

    private void Update()
    {
        if (DetonationTime <= 0)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Detonate();
            //CreateExplosionEffect();
        }

        if (DetonationTime > 0)
        {
            DetonationTime -= Time.deltaTime;
        }

        if (ActualTimeToDestroy <= 0)
        {
            Destroy(gameObject);
        }

        if (ActualTimeToDestroy > 0)
        {
            ActualTimeToDestroy -= Time.deltaTime;
        }
    }

    void Detonate()
    {
        Collider2D[] ObjectsToDestroy = Physics2D.OverlapCircleAll(transform.position, ExplodeRange, WhatToDestroy);
        for (int i=0; i < ObjectsToDestroy.Length; i++)
        {
            ObjectsToDestroy[i].GetComponent<Enemy>().TakeDamage(damage);
        }
        Debug.Log("I'm exploding!");

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ExplodeRange);
    }

    //void CreateExplosionEffect()
    //{
    //    explosion.Play();
    //}

    //private void OnCollisionEnter2D(Collision2D hitInfo)
    //{
    //    //Debug.Log(hitInfo.name);
    //    Enemy enemy = hitInfo.collider.GetComponent<Enemy>();
    //    if (enemy != null)
    //    {
    //        enemy.TakeDamage(damage);
    //    }
    //    Destroy(gameObject);
    //}
}
