using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // The global instance for other scripts to reference
    public static GameManager instance = null;
    [HideInInspector] public UIManager uiManager = null;
    public GameObject player = null;

    [SerializeField] private string mainMenuScene;
    [SerializeField] private GameObject gameUICanvas = null;
    public GameObject gmRespawnLocation = null;

    public bool gameIsOver = false;

    [Header("Lives")]
    [SerializeField] private int gmLifeCount = 0;

    [Header("Score")]
    [SerializeField] private int gmScoreCount = 0;
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

    public static GameObject RespawnLocation
    {
        get { return instance.gmRespawnLocation; }
        set { instance.gmRespawnLocation = value; }
    }

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

    public static int CurrentLifeCount
    {
        get { return instance.gmLifeCount; }
        set { instance.gmLifeCount = value; }
    }
    public static int CurrentScoreCount
    {
        get { return instance.gmScoreCount; }
        set { instance.gmScoreCount = value; }
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
        }
        else
        {
            Destroy(this.gameObject);
        }

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
        InitializeBombCount();
        InitializeKeyCount();
        InitializeScoreCount();
        InitializeInGameUI();
        InitiailizeRespawnLocation();
    }

    private void InitializePlayer()
    {
        if (player == null)
        {
            Debug.Log("Initializing Game Manager's \"player\" reference.");
            player = GameObject.FindGameObjectWithTag("Player");
        }
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
        gmScoreCount = gmInitialScoreCount;
    }

    private void InitializeInGameUI()
    {

        if (GameObject.FindGameObjectWithTag("ui_manager") != null)
        {
            uiManager = GameObject.FindGameObjectWithTag("ui_manager").GetComponent<UIManager>();
        }

        else
        {
            Debug.LogWarning("UI Manager cannot be found in scene. Make sure that a UI Canvas tagged with \"ui_manager\" is present in the scene. Adding a Game UI Canvas...");
            Instantiate(gameUICanvas);
        }
    }

//    ****************************** UPDATE FOR TOP DOWN
    private void InitiailizeRespawnLocation()
    {
        if (gmRespawnLocation == null)
        {
            Debug.LogWarning("Player Respawn Location not found in scene. Defaulting respawn location to player's location at start of scene.");
            gmRespawnLocation = new GameObject("Player Respawn Location");
            gmRespawnLocation.transform.position = player.transform.position;
            gmRespawnLocation.transform.SetParent(this.gameObject.transform);
        }
    }












    ////////////////////////////////////////////////////////////////////////////////
    // Player Prefs for data persistence
    // WORK IN PROGRESS

    /***
     * Watch video by Unity to understand data persistence - singleton, player prefs, saving
     * https://www.youtube.com/watch?v=J6FfcJpbPXE
     * Keep track of player lives, health, score in Game Manager with player prefs
    */

    /*private void Start()
    {
        if (PlayerPrefs.HasKey("highscore"))
        {
            highScore = PlayerPrefs.GetInt("highscore");
        }
        if (PlayerPrefs.HasKey("score"))
        {
            score = PlayerPrefs.GetInt("score");
        }
        //InitilizeGamePlayerPrefs();
    }

    private void InitilizeGamePlayerPrefs()
    {
        if (player != null)
        {
            Health playerHealth = player.GetComponent<Health>();

            // Set lives accordingly
            if (PlayerPrefs.GetInt("lives") == 0)
            {
                PlayerPrefs.SetInt("lives", playerHealth.currentLives);
            }

            playerHealth.currentLives = PlayerPrefs.GetInt("lives");

            // Set health accordingly
            if (PlayerPrefs.GetInt("health") == 0)
            {
                PlayerPrefs.SetInt("health", playerHealth.currentHealth);
            }

            playerHealth.currentHealth = PlayerPrefs.GetInt("health");
        }
    }

    private void SetGamePlayerPrefs()
    {
        if (player != null)
        {
            Health playerHealth = player.GetComponent<Health>();
            PlayerPrefs.SetInt("lives", playerHealth.currentLives);
            PlayerPrefs.SetInt("health", playerHealth.currentHealth);
        }
    }

    public void GameOver()
    {
        gameIsOver = true;
    }

    public static void IncrementScore(int scoreAmount)
    {
        score += scoreAmount;
        if (score > instance.highScore)
        {
            SaveHighScore();
        }
    }
    public static void ResetScore()
    {
        PlayerPrefs.SetInt("score", 0);
        score = 0;
    }

    public static void ResetGamePlayerPrefs()
    {
        PlayerPrefs.SetInt("score", 0);
        score = 0;
        PlayerPrefs.SetInt("lives", 0);
        PlayerPrefs.SetInt("health", 0);
    }

    private void OnApplicationQuit()
    {
        SaveHighScore();
        ResetScore();
    }

    public static void SaveHighScore()
    {
        if (score > instance.highScore)
        {
            PlayerPrefs.SetInt("highscore", score);
            instance.highScore = score;
        }
    }

    public static void ResetHighScore()
    {
        PlayerPrefs.SetInt("highscore", 0);
        if (instance != null)
        {
            instance.highScore = 0;
        }
    }*/
}
