using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the dealing of damage to health components.
/// </summary>
public class Damage : MonoBehaviour
{
    public int damageAmount = 1;
    public bool dealDamageOnTriggerEnter = false;
    public bool dealDamageOnTriggerStay = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dealDamageOnTriggerEnter)
        {
            DealDamage(collision.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (dealDamageOnTriggerStay)
        {
            DealDamage(collision.gameObject);
        }
    }

    private void DealDamage(GameObject collisionGameObject)
    {
        Health collidedHealth = collisionGameObject.GetComponent<Health>();
        if (collidedHealth != null)
        {

            collidedHealth.TakeDamage(damageAmount);
        }
    }
}
