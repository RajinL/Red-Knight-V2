using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushEnd : MonoBehaviour
{
    [SerializeField] private CameraFollow mainCamera;
    [Tooltip("The collider 2D with trigger that dictates when to trigger the rush scene when the camera collides with it." +
        "Leave blank if the scene does not have a rush.")]
    [SerializeField] private GameObject player;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameManager.instance.player)
        {
            mainCamera.rushStarted = false;
            mainCamera.rushEnded = true;
        }
    }
}
