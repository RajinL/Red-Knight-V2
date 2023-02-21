using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectKey : MonoBehaviour
{
    public bool hasKey = false;

    /// <summary>
    /// If Player collides with this key object, the key will be updated
    /// in the game manager and display on the UI. It will then be set
    /// inactive
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // TO DO
            // update the game manager that a key has been collected
            // and update the UI with the gm
            hasKey = true;
            this.gameObject.SetActive(false);
        }
    }
}
