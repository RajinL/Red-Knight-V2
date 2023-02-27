using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : BombDamage
{
    private GameObject player;

    public override void InitializeBomb()
    {
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
