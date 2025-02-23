﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // The global instance for other scripts to reference
    public static GameManager instance = null;
    public GameObject player = null;

    [SerializeField] private string currentSceneName;
    public UIManager uiManager;
    public bool isGameOver = false;

    public bool acceptPlayerInput = true;
    public bool isPlayerDead = false;
    public bool triggeredStoryAtScene = false;

    [Header("Health")]
    [SerializeField] private int gmInitialHealth = 0;
    [SerializeField] private int gmCurrentHealth = 0;
    [SerializeField] private int gmMaxHealth = 0; // if a health pickup is created

    [Header("Lives")]
    [SerializeField] private int gmInitialLifeCount = 0;
    [SerializeField] private int gmCurrentLifeCount = 0;
    [SerializeField] private int gmMaxLifeCount = 0;

    [Header("Score")]
    [SerializeField] private int gmCurrentScoreCount = 0;
    [SerializeField] private int gmInitialScoreCount = 0;
    [SerializeField] private int gmScoreLifeThreshold = 10;
    public int highScore = 0;

    [Header("Weapon Ammo")]
    [SerializeField] private int gmInitialGarlicBombCount = 0;
    [SerializeField] private int gmCurrentGarlicBombCount = 0;
    [SerializeField] private int gmMaxGarlicBombCount = 10;


    [Header("Keys")]
    [SerializeField] private int gmInitiaKeyCount = 0;
    [SerializeField] private int gmCurrentKeyCount = 0;

    public static int CurrentKeyCount
    {
        get { return instance.gmCurrentKeyCount; }
        set { instance.gmCurrentKeyCount = value; }
    }

    public static int CurrentGarlicBombCount
    {
        get { return instance.gmCurrentGarlicBombCount; }
        set { instance.gmCurrentGarlicBombCount = value; }
    }

    public static int MaxGarlicBombCount
    {
        get { return instance.gmMaxGarlicBombCount; }
        set { instance.gmMaxGarlicBombCount = value; }
    }

    public static int CurrentPlayerHealth
    {
        get { return instance.gmCurrentHealth; }
        set { instance.gmCurrentHealth = value; }
    }

    public static int MaxHealth
    {
        get { return instance.gmMaxHealth; }
        set { instance.gmMaxHealth = value; }
    }

    public static int CurrentLifeCount
    {
        get { return instance.gmCurrentLifeCount; }
        set { instance.gmCurrentLifeCount = value; }
    }

    public static int MaxLifeCount
    {
        get { return instance.gmMaxLifeCount; }
        set { instance.gmMaxLifeCount = value; }
    }

    public static int CurrentScoreCount
    {
        get { return instance.gmCurrentScoreCount; }
        set { instance.gmCurrentScoreCount = value; }
    }

    public static int ScoreLifeThreshold
    {
        get { return instance.gmScoreLifeThreshold; }
        set { instance.gmScoreLifeThreshold = value; }
    }



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
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {

        if (!IsMainMenuScene())
        {
            SetupGameParameters();
        }
    }

    /// <summary>
    /// Check if the current scene is the main menu or not.
    /// </summary>
    /// <returns>a boolelan</returns>
    private bool IsMainMenuScene()
    {
        Scene scene = SceneManager.GetActiveScene();

        // Main Menu Scene should be the first scene in the index - 0
        Scene mainMenuScene = SceneManager.GetSceneByBuildIndex(0);
        return scene == mainMenuScene;
    }

    /// <summary>
    /// Initializes and sets up settings for the game levels that are played, and not for the main menu.
    /// Anything that should be initialized before the level starts should be put in here.
    /// </summary>
    private void SetupGameParameters()
    {
        InitializePlayer();
        InitializeLifeCount();
        InitializeBombCount();
        InitializeKeyCount();
        InitializeScoreCount();
        InitializeCurrentScene();
        uiManager.SetPlayerMaxHealth(MaxHealth);
        UpdateUI();
        isGameOver = false;
    }

    private void InitializePlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().gameObject;
        uiManager = GameObject.FindGameObjectWithTag("ui_manager").GetComponent<UIManager>();
        ResetHealth();
    }

    private void InitializeBombCount()
    {
        gmCurrentGarlicBombCount = gmInitialGarlicBombCount;
    }

    private void InitializeKeyCount()
    {
        gmCurrentKeyCount = gmInitiaKeyCount;
    }

    private void InitializeScoreCount()
    {
        gmCurrentScoreCount = gmInitialScoreCount;
    }

    private void InitializeLifeCount()
    {
        gmCurrentLifeCount = gmInitialLifeCount;
    }

    private void InitializeCurrentScene()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    /// <summary>
    /// Calls the object tagged with "ui_manager" (In Game UI Canvas), and updates the UI by setting
    /// various UI properties. Does not update the player's health or the boss' health as there is only
    /// one player and one boss, and it is not necessary for this object to track their health.
    /// </summary>
    public void UpdateUI()
    {
        uiManager.SetPlayerBombCount(CurrentGarlicBombCount);
        uiManager.SetPlayerKeyCount(CurrentKeyCount);
        uiManager.SetScoreCount(CurrentScoreCount);
        uiManager.SetPlayerLifeCount(CurrentLifeCount);
        uiManager.SetPlayerHealth(CurrentPlayerHealth);
    }

    public void ResetHealth()
    {
        gmCurrentHealth = gmInitialHealth;
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
        Time.timeScale = 1;
        isPlayerDead = false;

        scene = SceneManager.GetActiveScene();
        if (scene.name == "0_MainMenu")
        {
            Destroy(gameObject);
        }
        else
        {
            //ResumeplayerInput();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().gameObject;
            uiManager = GameObject.FindGameObjectWithTag("ui_manager").GetComponent<UIManager>();
            uiManager.SetPlayerMaxHealth(MaxHealth);
            UpdateUI();
        }
    }

    public void StopPlayerInput()
    {
        acceptPlayerInput = false;
        if (player.GetComponent<Rigidbody2D>())
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        }
    }

    public void ResumeplayerInput()
    {
        acceptPlayerInput = true;
    }
}