using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    [SerializeField]
    Vector2 forceDirection;

    [SerializeField]
    int torque;

    private Rigidbody2D rb;

    private void Start()
    {
        float randomTorque = Random.Range(-30, 30);
        float randomForceX = Random.Range(forceDirection.x - 30, forceDirection.x + 30);
        float randomForceY = Random.Range(forceDirection.y, forceDirection.y + 30);

        forceDirection.x = randomForceX;
        forceDirection.y = randomForceY;

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(forceDirection);
        rb.AddTorque(randomTorque);

        if (gameObject.CompareTag("skeleton"))
        {
            AudioManagerScript.PlaySound("skeletonDies");
        }
        else
        {
            AudioManagerScript.PlaySound("barrelBreaks");
        }

        Invoke("DestroySelf", Random.Range(1f, 3f));
    }

    void DestroySelf()
    {

        Destroy(gameObject);
    }
}


