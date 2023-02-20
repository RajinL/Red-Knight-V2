using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    [SerializeField] public string sceneName;

    //if we want the scene to load with a delay
    IEnumerator ExecuteAfterTime(Collider2D collision, float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        if (collision.GetComponent<PlayerHealth>())
        {
            // CREATE FADE TRANSITION AND PLACE HERE - CONTROLS SHOULD BE LOCKED
            LoadNextSceneInBuild();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerHealth>())
        {
            LoadNextSceneInBuild();

            //SceneManager.LoadScene(sceneName);
        }
        StartCoroutine(ExecuteAfterTime(collision, 1));
        //StartCoroutine(ExecuteAfterTime(collision, 0));
    }

    /// <summary>
    /// Loads the next scene in the build.
    /// </summary>
    public void LoadNextSceneInBuild()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
