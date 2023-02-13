using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenChest : MonoBehaviour
{
    public Sprite ChestOpened;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //StartCoroutine(ExecuteAfterTime(1));
        //StartCoroutine(ExecuteAfterTime(0));
        if (collision.tag == "Player")
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = ChestOpened;


        }
    }
}
