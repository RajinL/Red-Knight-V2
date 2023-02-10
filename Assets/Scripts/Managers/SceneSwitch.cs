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

    [SerializeField]
    public int sceneNumber;


    //if we want the scene to load with a delay
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        SceneManager.LoadScene(1);

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SceneManager.LoadScene(sceneNumber);

        }
        //StartCoroutine(ExecuteAfterTime(1));
        //StartCoroutine(ExecuteAfterTime(0));

    }
}
