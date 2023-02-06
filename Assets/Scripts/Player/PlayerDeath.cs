using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerDeath : MonoBehaviour
{
    [Header("Custom Event")]
    public UnityEvent myEvents;

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "Player")
        {
            if (myEvents == null)
            {

            }
            else
            {
                myEvents.Invoke();
            }
        }
        
    }
}
