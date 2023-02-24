using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    /*void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(1);
    }*/

    [SerializeField] private bool requiresKey;
    [SerializeField] CollectKey checkKey;

    [SerializeField] private string sceneName;

    //if we want the scene to load with a delay
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        SceneManager.LoadScene(1);

    }

    /// <summary>
    /// If the player collides with this scene switch object (the door), then if it requires
    /// requires a key to unlock, it checks if the player has a key, and if so loads the next 
    /// scene in the build. If the player doesn't have a key, a UI message will pop up. However
    /// if the door does not require a key to unlock, then the next scene in the build will load
    /// when the player collides with it.
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerHealth>())
        {
            // TO DO
            // if game manager has key instead of this object has key - UPDATE GM
            if (requiresKey)
            {
                // try to make it where it says if (player.hasKey)
                if (checkKey != null && checkKey.hasKey)
                {
                    CheckForSceneName();
                }
                else
                {
                    Debug.Log("Player does not have a key to open door");
                    //code here for alert box
                    //"oh no, the door seems to be locked!"
                }
            }
            else CheckForSceneName();
        }
        //StartCoroutine(ExecuteAfterTime(1));
        //StartCoroutine(ExecuteAfterTime(0));

    }

    private void CheckForSceneName()
    {
        if (sceneName != "")
        {
            LoadSceneByName(sceneName);
        }
        else
        {
            LoadNextSceneInBuild();
        }
    }

    /// <summary>
    /// Loads the next scene in the build.
    /// </summary>
    public void LoadNextSceneInBuild()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadSceneByName(string buildNumber)
    {
        SceneManager.LoadScene(buildNumber);
    }
}
