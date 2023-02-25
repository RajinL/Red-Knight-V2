using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorTrigger : MonoBehaviour
{
    public Animator backDoor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            backDoor.SetBool("Closed", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
