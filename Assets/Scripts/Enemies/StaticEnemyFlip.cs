using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemyFlip : MonoBehaviour
{

    public bool isFlipped = false;
    // Start is called before the first frame update
    void Start()
    {
        if (isFlipped == true)
        {
            Flip();
        }
    }

    // Update is called once per frame
    void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
    }
}
