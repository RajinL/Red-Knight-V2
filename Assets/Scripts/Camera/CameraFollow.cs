using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class that handles all camera functionality for the game. It follows an object, presumably the player, is clamped
/// between the bounds of a level, and can trigger a rush scene smoothing the camera to another target.
/// </summary>
public class CameraFollow : MonoBehaviour
{
    [Header("Object Camera Follows")]
    [Tooltip("The object the camera will follow.")]
    [SerializeField] private GameObject objToFollow;
    [Header("Rush Settings")]
    [Tooltip("The collider 2D with trigger that dictates when to trigger the rush scene when the camera collides with it." +
        "Leave blank if the scene does not have a rush.")]
    [SerializeField] private Collider2D startOfRush;
    [Tooltip("The time it takes for the camera to switch between having the object it is following in the middle and on the" +
        "left side of the screen.")]
    [SerializeField] float transitionDuration = 2.5f;
    [Tooltip("A boolean to dictate if the rush has started.")]
    [SerializeField] private bool rushStarted;


    // private variables shared between functions
    // The game objects holding the EdgeCollider2ds that make up the bounds of the level
    private GameObject leftEdgeLevel;
    private GameObject rightEdgeLevel;
    private GameObject topEdgeLevel;
    private GameObject bottomEdgeLevel;

    // the bounds of the level with the aspect ratio taken into account
    private float leftBounds;
    private float rightBounds;
    private float topBounds;
    private float bottomBounds;
    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

    // the greatest common factor between the width and height of the aspect ratio
    private float aspectGCD;
    // the width of the aspect ratio
    private float aspectWidth;
    // the height of the aspect ratio
    private float aspectHeight;
    // the position of the rush that the camera will lerp towards
    private Vector3 targetPosition;

    void Awake()
    {
        MakeEdgeReferences();
        InitializeLevelBounds();
    }

    /// <summary>
    /// Finds a game object in the scene with a "LevelBounds" tag that has the scenes 4 edge colliders
    /// that dictate the bounds of the map, and connect each gameObject to each variable.
    /// </summary>
    private void MakeEdgeReferences()
    {
        if (GameObject.FindGameObjectWithTag("LevelBounds") != null)
        {
            GameObject levelBounds = GameObject.FindGameObjectWithTag("LevelBounds");

            leftEdgeLevel = levelBounds.transform.GetChild(0).gameObject;
            rightEdgeLevel = levelBounds.transform.GetChild(1).gameObject;
            topEdgeLevel = levelBounds.transform.GetChild(2).gameObject;
            bottomEdgeLevel = levelBounds.transform.GetChild(3).gameObject;
        }
        else
        {
            Debug.LogError("The Camera Level Bounds prefab is not in the scene, or if it is, it is not tagged " +
                "as \"LevelBounds\"! Unable to determine the edges and bounds of the map to clamp the camera.");
        }
    }

    /// <summary>
    /// Calculates the aspect ratio of the scene camera and adds or subtracts these values from each
    /// edge collider that makes up the bounds of the map. These will be the values that the camera
    /// will be clamped between.
    /// </summary>
    private void InitializeLevelBounds()
    {
        aspectGCD = GCD(Screen.width, Screen.height);
        aspectWidth = Screen.width / aspectGCD;
        aspectHeight = Screen.height / aspectGCD;

        //Debug.Log("GCD: " + GCD(Screen.width, Screen.height) + ", aspectX: " + aspectX + ", aspectY: " + aspectY);

        if (leftEdgeLevel != null && rightEdgeLevel != null &&
            topEdgeLevel != null && bottomEdgeLevel != null)
        {
            leftBounds = (leftEdgeLevel.transform.position.x) + aspectWidth;
            rightBounds = (rightEdgeLevel.transform.position.x) - aspectWidth;
            topBounds = (topEdgeLevel.transform.position.y) - aspectHeight;
            bottomBounds = (bottomEdgeLevel.transform.position.y) + aspectHeight;

            xMin = leftBounds;
            xMax = rightBounds;
            yMin = bottomBounds;
            yMax = topBounds;

            //Debug.Log("leftBounds: " + (leftBounds) + ", rightBounds: " + (rightBounds) +
            //          ", topBounds: " + (topBounds) + ", bottomBounds" + (bottomBounds));
            //Debug.Log("Screen.width: " + Screen.width + ", Screen.Height: " + Screen.height);
            //Debug.Log("GCD: " + GCD(Screen.width, Screen.height));
        }
        else
        {
            Debug.LogError("Level Bounds are not set! The Camera Level Bounds prefab must have" +
                " four edgeCollider2Ds for the left, right, top, and bottom  sides of the level.");
        }
    }

    /// <summary>
    /// Calculates the greatest common denominator. Used in determining the aspect ratio of a scene.
    /// </summary>
    private int GCD(int num1, int num2)
    {
        int r;

        while (num2 != 0)
        {
            r = num1 % num2;
            num1 = num2;
            num2 = r;
        }

        return num1;
    }

    /// <summary>
    /// Clamps the camera between the bounds of the map while following the objToFollow gameObject.
    /// If the rush has started, then the camera will move to the right to give the appearance that
    /// the enemies are on the left edge of the camera. If the scene does not have a rush, then the
    /// camera will update to render the camera in the middle of the screen.
    /// </summary>
    void Update()
    {
        float x = Mathf.Clamp(objToFollow.transform.position.x, xMin, xMax);
        float y = Mathf.Clamp(objToFollow.transform.position.y, yMin, yMax);

        if (rushStarted)
        {
            targetPosition = new Vector3(x + aspectWidth, y, gameObject.transform.position.z);
            StartCoroutine(Transition());
        }
        else
        {
            //Debug.Log("Normal camera system");
            gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
        }
    }

    /// <summary>
    /// If this camera has an edge collider, and it collides with a 2d collider that's triggered, then
    /// if there is a startOfRush 2D collider to signify where in the scene the rush should take place,
    /// then this function dictates that the rush has started.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (startOfRush != null)
        {
            rushStarted = true;
        }
    }

    /// <summary>
    /// Smooths the camera between locations. The first is when the camera is showing the object to follow
    /// in the middle of the camera, and the second is when the rush has started and the camera should be
    /// moved to the right giving the allusion that the object it is following is on the left side of the
    /// screen.
    /// <a href="https://answers.unity.com/questions/49542/smooth-camera-movement-between-two-targets.html"></a>
    /// </summary>
    /// <returns></returns>
    IEnumerator Transition()
    {
        float t = 0.0f;
        Vector3 startingPos = transform.position;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / transitionDuration);


            transform.position = Vector3.Lerp(startingPos, targetPosition, t);
            yield return 0;
        }
    }
}
