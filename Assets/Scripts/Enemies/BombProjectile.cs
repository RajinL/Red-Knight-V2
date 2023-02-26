using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : BombDamage
{
    private GameObject player;
    private Rigidbody2D rb;

    //[SerializeField] private float speed = 10f;

    // Start is called before the first frame update
    public override void InitializeBomb()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (player.transform.position.x < this.transform.position.x)
        {
            
            rb.velocity = -transform.right * speedThrown;
            

        }
        else
        {
            
            rb.velocity = transform.right * speedThrown;
            
        }
        detonationTime = initialDetTime;
        totalTimeToTurnOff = detonationTime + timeToTurnOffAfterDet;
    }
}
