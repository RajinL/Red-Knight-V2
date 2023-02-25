using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    [Header("Scene To Load Settings")]
    [Tooltip("If any string is written in here including a space, the scene with this name will" +
        " load. Leave blank, if you want to just load the next scene in the build. Watch for spaces!")]
    [SerializeField] private string sceneName;

    [Header("Scene Fade Settings")]
    [Tooltip("The animator component located on the Crossfade game object in the menu.")]
    [SerializeField] private Animator sceneTransition;
    [Tooltip("The time the fade animation last for.")]
    [SerializeField] private float transitionTime = 1f;

    [Header("Key Settings")]
    [Tooltip("If this game object requires a key to load a scene.")]
    [SerializeField] private bool requiresKey;
    [Tooltip("The key prefab needed to load a scene.")]
    [SerializeField] CollectKey keyPrefab;

    /// <summary>
    /// If the player collides with this scene switch object (the door), then if it requires
    /// requires a key to unlock, it checks if the player has a key, and if so loads the next 
    /// scene in the build with a fade transition. If the player doesn't have a key, a UI message
    /// will pop up. However if the door does not require a key to unlock, then the next scene
    /// in the build will load when the player collides with it.
    /// </summary>
    /// <param name="collision">The player object.</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerHealth>())
        {
            // TO DO
            // if game manager has key instead of this object has key - UPDATE GM
            if (requiresKey)
            {
                // try to make it where it says if (player.hasKey)
                if (keyPrefab != null && keyPrefab.hasKey)
                {
                    StartCoroutine(LoadSceneWithFade(transitionTime));
                }
                else
                {
                    Debug.Log("Player does not have a key to open door");
                    //code here for alert box
                    //"oh no, the door seems to be locked!"
                }
            }
            else
            {
                StartCoroutine(LoadSceneWithFade(transitionTime));
            }
        }
    }

    /// <summary>
    /// https://www.youtube.com/watch?v=CE9VOZivb3I
    /// Creates a fade transition and loads the next scene.
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadSceneWithFade(float transitionTime)
    {
        sceneTransition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        CheckForSceneName();
    }

    /// <summary>
    /// Checks if the editor specifies if a string for the sceneName property is used indicating that
    /// the name written down will correspond with which scene to load. If used, it will load this scene
    /// instead of the next scene in the bulid index.
    /// </summary>
    private void CheckForSceneName()
    {
        if (sceneName != "")
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
