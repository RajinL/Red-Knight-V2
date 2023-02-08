using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicBomb : MonoBehaviour
{
    //public ParticleSystem bombTrail;
    public Transform bombDrop;
    public GameObject bombPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            DropBomb();
        }
    }

    //void CreateBombTrail()
    //{
    //    bombTrail.Play();
    //}

    void DropBomb()
    {
        //CreateBombTrail();
        Instantiate(bombPrefab, bombDrop.position, bombDrop.rotation);
    }
}
