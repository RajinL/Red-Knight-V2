using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenChest : MonoBehaviour
{
    public Sprite ChestOpened;

    /// <summary>
    /// If an object with a PlayerHealth component (i.e. the Player) collides with this chest,
    /// then the chest will animate to appear as if it is opening, and bomb ammo will drop for
    /// the player to collect. However, if anything else such as a bullet or garlic bomb collides
    /// with it, then it will not execute.
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        //StartCoroutine(ExecuteAfterTime(1));
        //StartCoroutine(ExecuteAfterTime(0));
        if (collision.GetComponent<PlayerHealth>())
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = ChestOpened;
            AudioManagerScript.PlaySound("treasureChest");
            // TO DO
            // Instantiate a bomb for player to collect
        }
    }
}
