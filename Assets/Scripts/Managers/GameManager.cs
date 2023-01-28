using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/***
 * Watch video by Unity to understand data persistence - singleton, player prefs, saving
 * https://www.youtube.com/watch?v=J6FfcJpbPXE
 * Keep track of player lives, health, score in Game Manager with player prefs
 */



public class GameManager : MonoBehaviour
{
    // The global instance for other scripts to reference
    public static GameManager instance = null;
    public GameObject player = null;

    public bool gameIsOver = false;

    public int score = 0;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        //https://docs.unity3d.com/ScriptReference/GameObject.FindWithTag.html
        if (player == null)
        {
            Debug.Log("Initializing Game Manager's \"player\" reference.");
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public void GameOver()
    {
        gameIsOver = true;
    }

    public static void IncrementScore(int scoreAmount)
    {
        instance.score += scoreAmount;
    }
}
