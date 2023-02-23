using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//https://www.youtube.com/watch?v=JivuXdrIHK0

/// <summary>
/// Class to handle all buttons and functionality when the user presses the pause menu button.
/// </summary>
public class PauseMenu : MonoBehaviour
{
    [Header("Pause Settings")]
    [Tooltip("Shows whether the game is paused or not. Necessary for if the user presses a button" +
        "to open the pause menu.")]
    public static bool GameIsPaused = false;

    [Header("Pause Menu Pages")]
    [Tooltip("The pause menu panel")]
    [SerializeField] private GameObject pauseMenuUI;
    [Tooltip("The main page of the pause menu")]
    [SerializeField] private GameObject mainPage;
    [Tooltip("The level select page of the pause menu")]
    [SerializeField] private GameObject levelSelectPage;

    private void Update()
    {
        if (pauseMenuUI != null && Input.GetButtonDown("Menu"))
        {
            if (!GameIsPaused)
            {
                Pause();
            }

            else
            {
                Resume();
            }
        }
    }

    /// <summary>
    /// Unfreezes the game, and closes the pause screen
    /// </summary>
    public void Resume()
    {
        if (pauseMenuUI != null) pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    /// <summary>
    /// Freezes the game, and opens the pause screen
    /// </summary>
    public void Pause()
    {
        if (pauseMenuUI != null) pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    /// <summary>
    /// Turns the main page off and opens the level select screen page
    /// </summary>
    public void OpenLevelSelectPage()
    {
        mainPage.SetActive(false);
        levelSelectPage.SetActive(true);
    }

    /// <summary>
    /// Turns the level select screen page off and opens the main page
    /// </summary>
    public void OpenMenuPage()
    {
        mainPage.SetActive(true);
        levelSelectPage.SetActive(false);
    }

    /// <summary>
    /// Loads the first scene in the build
    /// </summary>
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        Resume();
    }

    public void LoadScene1()
    {
        SceneManager.LoadScene(1);
        Resume();
    }

    public void LoadScene2()
    {
        SceneManager.LoadScene(2);
        Resume();
    }

    public void LoadScene3()
    {
        SceneManager.LoadScene(3);
        Resume();
    }

    public void LoadScene4()
    {
        SceneManager.LoadScene(4);
        Resume();
    }

    public void LoadScene5()
    {
        SceneManager.LoadScene(5);
        Resume();
    }

    public void LoadScene6()
    {
        SceneManager.LoadScene(6);
        Resume();
    }
}