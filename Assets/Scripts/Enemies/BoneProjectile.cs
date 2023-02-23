using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneProjectile : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float force;
    public float speed = 10f;
    public float rotationSpeed = 10f;
    [SerializeField] private int attackDamage = 1;

    // Start is called before the first frame update
    void Start()
    {       
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        //Vector3 direction = player.transform.position - transform.position;
        //rb.velocity = new Vector2(direction.x, direction.y).normalized * force; 

        //rb.velocity = new Vector2(direction.x, direction.y) * speed;

        if (player.transform.position.x < this.transform.position.x)
        {
            rb.velocity = -transform.right * speed;
        }
        else
        {
            rb.velocity = transform.right * speed;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag == "Player")
        {
            hitInfo.gameObject.GetComponent<Health>().TakeDamage(attackDamage);
            Debug.Log("Colliding with player");
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
