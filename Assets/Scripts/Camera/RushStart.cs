using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushStart : MonoBehaviour
{
    [SerializeField] private CameraFollow mainCamera;
    [Tooltip("The collider 2D with trigger that dictates when to trigger the rush scene when the camera collides with it." +
        "Leave blank if the scene does not have a rush.")]
    [SerializeField] private GameObject rushObject;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == rushObject)
        {
            mainCamera.rushStarted = true;
            mainCamera.rushEnded = false;
        }
    }
}
