using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoorTrigger : MonoBehaviour
{
    public Animator backDoor;
    public Animator boss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            backDoor.SetBool("Closed", true);

            boss.SetTrigger("StartFight");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
