using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEvilLaugh : MonoBehaviour
{
    AudioSource soundSource;
    public float startingTime;
    public int repeatTime;

    // Start is called before the first frame update
    void Start()
    {

        soundSource = GetComponent<AudioSource>();
        InvokeRepeating("PlaySound", startingTime, repeatTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlaySound()
    {
        soundSource.Play();
    }
}
