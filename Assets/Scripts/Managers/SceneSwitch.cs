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
        if (collision.tag == "Player")
        {
            // CREATE FADE TRANSITION AND PLACE HERE - CONTROLS SHOULD BE LOCKED
            LoadNextScene();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            LoadNextScene();

            //SceneManager.LoadScene(sceneName);
        }
        StartCoroutine(ExecuteAfterTime(collision, 1));
        //StartCoroutine(ExecuteAfterTime(collision, 0));
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
