using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crossfade : MonoBehaviour
{
    [SerializeField] private GameObject crossfade;
    private void Awake()
    {
        crossfade.SetActive(true);
    }
}
