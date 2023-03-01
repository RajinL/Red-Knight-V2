using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    public static bool allowPause; // set to false across classes if you need to prevent pausing
    [SerializeField] private Slider playerHealthSlider;
    [SerializeField] private Slider bossHealthSlider;
    [SerializeField] private TextMeshProUGUI lifeCount;
    [SerializeField] private TextMeshProUGUI bombCount;
    [SerializeField] private TextMeshProUGUI keyCount;
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] public GameObject storyUIPanel;
    [SerializeField] public Button storyUIButton;
    [SerializeField] public TextMeshProUGUI storyTextUI;
    [SerializeField] private GameObject crossFade;
    [SerializeField] private Animator sceneTransition;
    [SerializeField] private float transitionTime = 1f;

    private void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();
        // Set up the singleton instance of this
        if (scene.name != "0_MainMenu")
        {
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

        else
        {
            Destroy(gameObject);
        }

        crossFade.SetActive(false);
        crossFade.SetActive(true);
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
        if (scene.name == "0_MainMenu")
        {
            Destroy(gameObject);
        }

        if (scene.name != "7_BossFight")
        {
            bossHealthSlider.gameObject.SetActive(false);
        }

        else if (scene.name == "7_BossFight")
        {
            bossHealthSlider.gameObject.SetActive(true);
        }

        crossFade.SetActive(false);
        crossFade.SetActive(true);
    }

    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "0_MainMenu")
        {
            allowPause = false;
        }
        else
        {
            allowPause = true;
        }
    }

    /// <summary>
    /// Sets the player healthbar slider's max value to match with the player's health settings.
    /// Useful for when a scene first starts, and the slider's max value matches the player's
    /// max health.
    /// </summary>
    /// <param name="health"></param>
    public void SetPlayerMaxHealth(int health)
    {
        playerHealthSlider.maxValue = health;
        playerHealthSlider.value = health;
    }

    public void SetBossMaxHealth(int health)
    {
        bossHealthSlider.maxValue = health;
        bossHealthSlider.value = health;
    }

    public void SetPlayerHealth(int health)
    {
        playerHealthSlider.value = health;
    }

    public void SetBossHealth(int health)
    {
        bossHealthSlider.value = health;
    }

    public void SetPlayerLifeCount(int lives)
    {
        lifeCount.text = (": " + lives.ToString());
    }

    public void SetPlayerBombCount(int bombs)
    {
        bombCount.text = (": " + bombs.ToString());
    }

    public void SetPlayerKeyCount(int keys)
    {
        keyCount.text = (": " + keys.ToString());
    }

    public void SetScoreCount(int score)
    {
        scoreUI.text = ("Score: " + score.ToString());
    }

    /// <summary>
    /// Loads the scene with the same name as the sceneName paramater.
    /// <a herf = "https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadScene.html"></a>
    /// </summary>
    public void LoadSceneByName(string sceneName)
    {
        StartCoroutine(LoadSceneWithFade(sceneName));
    }

    /// <summary>
    /// Restarts the current scene. Will be used for when player dies.
    /// </summary>
    public void RestartCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Quits the game regardless if it's a build or in the engine editor.
    /// < a href = "https://docs.unity3d.com/ScriptReference/EditorApplication-isPlaying.html"></a>
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    /// <summary>
    /// https://www.youtube.com/watch?v=CE9VOZivb3I
    /// Creates a fade transition and loads the next scene.
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadSceneWithFade(string sceneName)
    {
        sceneTransition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
    }

    public void DisplayMessage(string message)
    {
        GameManager.instance.StopPlayerInput();
        //GameManager.instance.acceptPlayerInput = false;
        storyUIPanel.SetActive(true);
        storyTextUI.text = message;
    }
}