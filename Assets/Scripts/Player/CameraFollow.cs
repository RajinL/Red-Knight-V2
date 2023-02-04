using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //NEW SCRIPT

    private GameObject player;
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
        float y = Mathf.Clamp(player.transform.position.y, yMin, yMax);
        gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);

    }

    /*OLD SCRIPT
     * 
     * private Vector3 offset = new Vector3(0f, 3f, -18f);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;
    
    [SerializeField] private Transform target;


    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }*/
}
