﻿using System.Collections;
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
    [Tooltip("The controls page of the pause menu")]
    [SerializeField] private GameObject controlsPage;
    [Tooltip("The credits page of the pause menu")]
    [SerializeField] private GameObject creditsPage;
    [Tooltip("The story page of the pause menu")]
    [SerializeField] private GameObject storyUIPanel;

    Scene scene;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    private void Update()
    {
        if (scene.name != "0_MainMenu")
        {
            if (pauseMenuUI != null && Input.GetButtonDown("Pause"))
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
    }

    /// <summary>
    /// Unfreezes the game, and closes the pause screen
    /// </summary>
    public void Resume()
    {
        if (pauseMenuUI != null)
        {
            mainPage.SetActive(false);
            controlsPage.SetActive(false);
            levelSelectPage.SetActive(false);
            storyUIPanel.SetActive(false);
            pauseMenuUI.SetActive(false);
            creditsPage.SetActive(false);
        }
        GameManager.instance.triggeredStoryAtScene = false;
        //GameManager.instance.acceptPlayerInput = true;
        GameManager.instance.ResumeplayerInput();
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    /// <summary>
    /// Freezes the game, and opens the pause screen
    /// </summary>
    public void Pause()
    {
        if (pauseMenuUI != null)
        {
            mainPage.SetActive(true);
            pauseMenuUI.SetActive(true);
        }
        GameManager.instance.StopPlayerInput();
        //GameManager.instance.acceptPlayerInput = false;
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    /// <summary>
    /// Turns the level select screen page off and opens the main page
    /// </summary>
    public void OpenMenuPage()
    {
        mainPage.SetActive(true);
        controlsPage.SetActive(false);
        levelSelectPage.SetActive(false);
        creditsPage.SetActive(false);
    }

    /// <summary>
    /// Turns the main page off and opens the level select screen page
    /// </summary>
    public void OpenLevelSelectPage()
    {
        mainPage.SetActive(false);
        controlsPage.SetActive(false);
        levelSelectPage.SetActive(true);
        creditsPage.SetActive(false);
    }

    /// <summary>
    /// Turns the main page off and opens the controls screen page
    /// </summary>
    public void OpenControlsPage()
    {
        mainPage.SetActive(false);
        controlsPage.SetActive(true);
        levelSelectPage.SetActive(false);
        creditsPage.SetActive(false);
    }

    /// <summary>
    /// Turns the main page off and opens the controls screen page
    /// </summary>
    public void OpenCreditsPage()
    {
        mainPage.SetActive(false);
        controlsPage.SetActive(false);
        levelSelectPage.SetActive(false);
        creditsPage.SetActive(true);
    }

    /// <summary>
    /// Loads the first scene in the build
    /// </summary>
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        Resume();
    }

    public void LoadScene(int buildNumber)
    {
        StartCoroutine(LoadSceneWithFade(buildNumber));
        Resume();
    }

    IEnumerator LoadSceneWithFade(int buildNumber)
    {
        SceneManager.LoadScene(buildNumber);
        yield return new WaitForSeconds(3);
    }
}