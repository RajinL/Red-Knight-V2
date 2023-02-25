using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAlt : MonoBehaviour
{
    private GameObject player;
    private float scale = 0.33f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x < this.transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
            if (transform.parent.tag == "platform")
            {
                transform.localScale = new Vector3(0.33f, 0.33f, 0.33f);
            }
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
            if (transform.parent.tag == "platform")
            {
                transform.localScale = new Vector3(-0.33f, 0.33f, 0.33f);
            }
        }
    }
}
