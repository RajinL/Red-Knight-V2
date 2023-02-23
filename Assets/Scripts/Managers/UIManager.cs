using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static bool allowPause; // set to false across classes if you need to prevent pausing
    private bool isPaused = false;
    [SerializeField] private Slider playerHealthSlider;
    [SerializeField] private Slider bossHealthSlider;
    [SerializeField] private TextMeshProUGUI lifeCount;
    [SerializeField] private TextMeshProUGUI bombCount;
    [SerializeField] private TextMeshProUGUI scoreUI;

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

    private void Update()
    {
        if (allowPause)
        {
            //CheckPauseInput();
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

    public void SetScoreCount(int score)
    {
        scoreUI.text = ("Score: " + score.ToString());
    }

    private void OnEnable()
    {
        SetupGameManagerUIManager();
    }

    private void SetupGameManagerUIManager()
    {
        if (GameManager.instance != null && GameManager.instance.uiManager == null)
        {
            GameManager.instance.uiManager = this;
        }
    }

    //private void CheckPauseInput()
    //{

    //    if (Input.GetKeyDown("Menu"))
    //    {
    //        PauseMenu.GameIsPaused = true;
    //    }
    //}

    //public void TogglePause()
    //{
    //    if (isPaused)
    //    {
    //        Time.timeScale = 1;
    //        isPaused = false;
    //    }
    //    else
    //    {
    //        Time.timeScale = 0;
    //        isPaused = true;
    //    }
    //}

    /// <summary>
    /// //https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadScene.html
    /// </summary>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void RestartCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// https://docs.unity3d.com/ScriptReference/EditorApplication-isPlaying.html
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}