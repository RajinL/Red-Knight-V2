using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : BombDamage
{
    public override void InitializeBomb()
    {
        if (GameManager.instance.player.transform.position.x < this.transform.position.x)
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
