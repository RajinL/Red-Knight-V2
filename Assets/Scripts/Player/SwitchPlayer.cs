using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchPlayer : MonoBehaviour
{
    public static SwitchPlayer instance = null;

    [SerializeField] private GameObject playerSidescroller;
    [SerializeField] private GameObject playerTopDown;
    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        // Set up the singleton instance of this
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // https://answers.unity.com/questions/1174255/since-onlevelwasloaded-is-deprecated-in-540b15-wha.html
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        scene = SceneManager.GetActiveScene();
        if (scene.name == "6_TopDownTutorial" || scene.name == "7_BossFight")
        {
            playerSidescroller.SetActive(false);
            playerTopDown.SetActive(true);
            gameObject.transform.position = new Vector3(0, 0, 0);
            StartCoroutine(InitializeTopDownSpawnPoint(0.01f));
        }

        else if(scene.name == "0_MainMenu")
        {
            Destroy(gameObject);
        }

        else
        {
            playerSidescroller.SetActive(true);
            playerTopDown.SetActive(false);
            gameObject.transform.position = new Vector3(0, 0, 0);
            StartCoroutine(InitializeSidescrollerSpawnPoint(0.01f));
        }
    }

    IEnumerator InitializeSidescrollerSpawnPoint(float time)
    {
        yield return new WaitForSeconds(time);
        playerSidescroller.transform.position = gameManager.spawnPoint.transform.position;
    }

    IEnumerator InitializeTopDownSpawnPoint(float time)
    {
        yield return new WaitForSeconds(time);
        playerTopDown.transform.position = gameManager.spawnPoint.transform.position;
    }
}
