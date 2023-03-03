using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTutorial : MonoBehaviour
{

    public GameObject tutorialObject; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            tutorialObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            tutorialObject.SetActive(false);
        }
    }
}
