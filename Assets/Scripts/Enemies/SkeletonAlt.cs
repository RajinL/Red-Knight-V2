using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAlt : MonoBehaviour
{
    //private float scale = 0.33f;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.player.transform.position.x < this.transform.position.x)
        {
            
            if (transform.parent.CompareTag("platform"))
            {
                transform.localScale = new Vector3(0.33f, 0.33f, 0.33f);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            
            if (transform.parent.CompareTag( "platform"))
            {
                transform.localScale = new Vector3(-0.33f, 0.33f, 0.33f);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
}
