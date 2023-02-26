﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacksPlayer : MonoBehaviour
{
    public PlayerDetector playerDetect;
    public bool _hasTarget = false;

    Animator anim;

    internal static string hasTarget = "hasTarget";

    private int attackDamage = 3;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            anim.SetBool(hasTarget, value);
        }
    }

    public bool CanMove
    {
        get
        {
            return anim.GetBool("canMove");
        }
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = playerDetect.detectedColliders.Count > 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }
}
