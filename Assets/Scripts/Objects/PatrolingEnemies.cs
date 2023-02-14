using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolingEnemies : MonoBehaviour
{
    public List<Transform> points;
    //public Transform enemy;
    int goalPoint = 0;
    public float moveSpeed = 2;

    /*[SerializeField]
    private GameObject enemySprite;*/

    public bool facingRight = true;

    private void Update()
    {
        MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        /*if (GameObject.Find(enemySprite.name) != null)
        {
            //it exists
            enemy.position = UnityEngine.Vector2.MoveTowards(enemy.position,
            points[goalPoint].position, Time.deltaTime * moveSpeed);
            if (UnityEngine.Vector2.Distance(enemy.position, points[goalPoint].position) < 0.1f)
            {
                if (goalPoint == points.Count - 1)
                {
                    goalPoint = 0;
                }
                else
                {
                    goalPoint++;
                }
            }
        }*/

        this.gameObject.transform.position = UnityEngine.Vector2.MoveTowards(this.gameObject.transform.position,
            points[goalPoint].position, Time.deltaTime * moveSpeed);
        if (UnityEngine.Vector2.Distance(this.gameObject.transform.position, points[goalPoint].position) < 0.1f)
        {
            if (goalPoint == points.Count - 1)
            {
                goalPoint = 0;
                Flip();
            }
            else
            {
                goalPoint++;
                Flip();
            }
        }

    }

    void Flip()
    {
        //CreateDustTrail();
        transform.Rotate(0f, 180f, 0f);

        //Vector3 currentScale = gameObject.transform.localScale;
        //currentScale.x *= -1;
        //gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
}
