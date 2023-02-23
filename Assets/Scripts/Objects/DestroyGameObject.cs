using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TO DO
// Update this class and integrate object pooling similar to GarBombExplosion.cs

public class DestroyGameObject : MonoBehaviour
{
    [SerializeField] private int lifetime = 3;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
