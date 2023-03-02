using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that allows the player to throw a bomb, play the bomb's trail system, and updates the UI ammo count
/// </summary>
public class GarBombExplosion : MonoBehaviour
{
    [Header("Lifetime Settings")]
    [Tooltip("The lifetime of this object before it is turned off.")]
    [SerializeField] private float timeToTurnOff;

    private float totalTimeToTurnOff;

    /// <summary>
    /// When a Garlic Bomb Explosion Object is instantiated in a scene, it immediately
    /// childs itself to an object with a tag string name.
    /// </summary>
    private void Awake()
    {
        SetParentToObjWithTag("gar_bomb_explo_parent");
    }

    private void OnEnable()
    {
        totalTimeToTurnOff = timeToTurnOff;
    }

    private void Update()
    {
        CheckForDestroyTime();
    }

    /// <summary>
    /// Starts a timer counting down to 0, and then turns the game object off.
    /// </summary>
    void CheckForDestroyTime()
    {
        if (totalTimeToTurnOff > 0)
        {
            totalTimeToTurnOff -= Time.deltaTime;
        }

        if (totalTimeToTurnOff <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Sets this object's parent to an object with a tag.
    /// </summary>
    /// <param name="tag">The tag name's as a string</param>
    private void SetParentToObjWithTag(string tag)
    {
        if (GameObject.FindGameObjectWithTag(tag))
        {
            Transform parent = GameObject.FindGameObjectWithTag(tag).transform;
            transform.SetParent(parent);
        }
        else
        {
            Debug.LogWarning("Unable to find " + tag + " tag to set the " + gameObject.name + " game objects's parent" +
                " because the Object Pool prefab is not included in this scene! Insert the Object Pool prefab into the scene" +
                " to organize pooled objects.");
        }
    }
}
