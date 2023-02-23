using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that is attached to the main camera game object in a scene. It follows an object
/// and adjusts the points of the EdgeCollider2D on the camera. Used in the rush scene if
/// player falls behind and hits the camera.
/// </summary>
// CameraFollowRushScene requires the GameObject to have an EdgeCollider2D component
[RequireComponent(typeof(EdgeCollider2D))]
public class CameraFollowRushScene : MonoBehaviour
{
    [SerializeField] private GameObject objToFollow;
    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
    [SerializeField] private float yMin;
    [SerializeField] private float yMax;

    private void Awake()
    {
        AdjustColliderOnCamera();
    }

    /// <summary>
    /// Adjusts the EdgeCollider2D's points on the left side of the main camera.
    /// https://www.youtube.com/watch?v=-SlN5ekMLR8
    /// </summary>
    private void AdjustColliderOnCamera()
    {
        // Get or Add Edge Collider 2D component
        EdgeCollider2D edgeCollider = this.gameObject.GetComponent<EdgeCollider2D>();
        Camera cam = Camera.main;

        // Creating points
        Vector2 leftBottom = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector2 leftTop = cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane));
        Vector2[] edgePoints = new[] { leftBottom, leftTop };

        // Adding edge points
        edgeCollider.points = edgePoints;

        // Readjusting offset to account for camera's y position changing when locking
        // on to Bats for rush scene
        edgeCollider.offset = new Vector2(0, -this.transform.position.y);
    }

    void Update()
    {
        float x = Mathf.Clamp(objToFollow.transform.position.x, xMin, xMax);
        float y = Mathf.Clamp(objToFollow.transform.position.y, yMin, yMax);
        gameObject.transform.position = new Vector3(x+8, y, gameObject.transform.position.z);
    }
}
