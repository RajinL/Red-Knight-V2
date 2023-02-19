using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGame : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        TopDownMovement tdm = player.GetComponent<TopDownMovement>();
        Transform finishPoint = GameObject.FindGameObjectWithTag("Finish").transform;
        tdm.target = finishPoint;
    }
}
