using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatProjectile : MonoBehaviour
{
    [SerializeField] private float delayDeactivation = 0f;

    private void Awake()
    {
        SetParentToObjWithTag("bat_projectile_parent");
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


    /// <summary>
    /// When this object an the player collides, start a coroutine to delay deactivating the object
    /// because the damage component, which should be a child, needs to do damage first upon collision,
    /// then this object can turn off.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == GameManager.instance.player)
        {
            //gameObject.SetActive(false);
            StartCoroutine(DelayDeactivation(delayDeactivation));
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject == GameManager.instance.player)
    //    {
    //        gameObject.SetActive(false);
    //    }
    //}

    IEnumerator DelayDeactivation(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

}
