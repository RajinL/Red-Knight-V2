using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // The global instance for other scripts to reference
    public static GameManager instance = null;
    public static UIManager uiManager = null;
    public GameObject player = null;

    [SerializeField] private string currentSceneName;
    [SerializeField] private GameObject gameUICanvas = null;
    public GameObject gmRespawnLocation = null;

    public bool gameIsOver = false;

    [Header("Players")]
    [SerializeField] public GameObject gmPlayerSidescroller;
    [SerializeField] public GameObject gmPlayerTopdown;
    [SerializeField] public GameObject spawnPoint;

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

    public static GameObject PlayerSidescroller
    {
        get { return instance.gmPlayerSidescroller; }
        //set { instance.gmPlayerSidescroller = value; }
    }

    public static GameObject PlayerTopDown
    {
        get { return instance.gmPlayerTopdown; }
        //set { instance.gmPlayerTopdown = value; }
    }

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
            DontDestroyOnLoad(gameObject); // MUST MAKE PLAYER DONTDESTROYONLOAD AS WELL BECUASE OBJECT POOLS ARE LOOKING FOR THE SAME INSTANCE OF GAMEMANAGER!!!
        }
        else
        {
            Destroy(this.gameObject);
        }

        if (!IsMainMenuScene())
        {
            SetupGameParameters();
            UpdateUI();
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
        InitializeInGameUI();
        InitializeCurrentScene();
        InitializeSpawnPoint();
        InitiailizeRespawnLocation();
    }
    private void InitializeSpawnPoint()
    {
        if (GameObject.FindGameObjectWithTag("spawn_point"))
        {
            spawnPoint = GameObject.FindGameObjectWithTag("spawn_point");
        }
    }


private void InitializePlayer()
    {
        if (player == null)
        {
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
        gmCurrentScoreCount = gmInitialScoreCount;
    }

    private void InitializeLifeCount()
    {
        gmCurrentLifeCount = gmInitialLifeCount;
    }

    private void InitializeInGameUI()
    {

        if (GameObject.FindGameObjectWithTag("ui_manager") != null)
        {
            uiManager = GameObject.FindGameObjectWithTag("ui_manager").GetComponent<UIManager>();
            gameUICanvas = uiManager.gameObject;
        }

        else
        {
            Debug.LogWarning("UI Manager cannot be found in scene. Make sure that a UI Canvas tagged with" +
                " \"ui_manager\" is present in the scene. Adding a Game UI Canvas...");
            Instantiate(gameUICanvas);
        }
    }

    private void InitializeCurrentScene()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    //    ****************************** UPDATE FOR TOP DOWN
    private void InitiailizeRespawnLocation()
    {
        if (gmRespawnLocation == null)
        {
            Debug.LogWarning("Player Respawn Location not found in scene. Defaulting respawn location to" +
                " player's location at start of scene.");
            gmRespawnLocation = new GameObject("Player Respawn Location");
            gmRespawnLocation.transform.position = player.transform.position;
            gmRespawnLocation.transform.SetParent(this.gameObject.transform);
        }
    }

    /// <summary>
    /// Calls the object tagged with "ui_manager" (In Game UI Canvas), and updates the UI by setting
    /// various UI properties. Does not update the player's health or the boss' health as there is only
    /// one player and one boss, and it is not necessary for this object to track their health.
    /// </summary>
    public static void UpdateUI()
    {
        uiManager.SetPlayerBombCount(CurrentGarlicBombCount);
        uiManager.SetPlayerKeyCount(CurrentKeyCount);
        uiManager.SetScoreCount(CurrentScoreCount);
        uiManager.SetPlayerLifeCount(CurrentLifeCount);
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
        // Set up the singleton instance of this
        if (scene.name == "0_MainMenu")
        {
            Destroy(gameObject);
        }
        else
        {
            InitializeSpawnPoint();
        }
    }
}
