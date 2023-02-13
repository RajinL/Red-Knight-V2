using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch2 : MonoBehaviour
{
    /*void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(1);
    }*/

    public CollectKey checkKey;

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
                if (checkKey.hasKey)
                {
                    SceneManager.LoadScene(sceneNumber);
                }
                else
                {
                    //code here for alert box
                    //"oh no, the door is seems to be locked!"
                }

        }
        //StartCoroutine(ExecuteAfterTime(1));
        //StartCoroutine(ExecuteAfterTime(0));

    }
}
