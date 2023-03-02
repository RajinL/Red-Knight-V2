using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://learn.unity.com/tutorial/introduction-to-object-pooling#5ff8d015edbc2a002063971d

/// <summary>
/// Class to use Object Pooling design pattern
/// </summary>
public class ObjectPool : MonoBehaviour
{
    //public static ObjectPool SharedInstance;
    [Header("Object Pool Settings")]
    [Tooltip("A list of objects that are stored in this pool.")]
    [SerializeField] private List<GameObject> pooledObjects;
    [Tooltip("The object to store in this pool.")]
    [SerializeField] private GameObject objectToPool;
    [Tooltip("The number of obects to be stored in this pool.")]
    [SerializeField] private int amountToPool;

    /// <summary>
    /// Instantiates a number of GameObjects to pool with the amountToPool parameter,
    /// sets their parent,
    /// turns all of them off,
    /// and then adds them to the pooledObjects list.
    /// </summary>
    void Awake()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    /// <summary>
    /// Returns any GameObjects that are stored in the pooledObjects list that are
    /// turned on or active in the hierarchy.
    /// </summary>
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
