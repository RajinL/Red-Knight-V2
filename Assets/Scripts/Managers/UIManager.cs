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
    [SerializeField] private Slider healthSlider;
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
            CheckPauseInput();
        }
    }

    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    public void SetHealth(int health)
    {
        healthSlider.value = health;
    }

    public void SetLifeCount(int lives)
    {
        lifeCount.text = (": " + lives.ToString());
    }

    public void setBombCount(int bombs)
    {
        bombCount.text = (": " + bombs.ToString());
    }

    public void setScoreCount(int score)
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

    private void CheckPauseInput()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
        }
    }

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