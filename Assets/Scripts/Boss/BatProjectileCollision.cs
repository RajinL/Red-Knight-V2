using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatProjectileCollision : MonoBehaviour
{
    // Enemy attack
    [SerializeField] private float attackDamage = 20f;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().UpdateHealth(-attackDamage);
            Destroy(gameObject);
        }
    }

}
