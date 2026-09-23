using UnityEngine;

public class basicEnemy : MonoBehaviour
{
    public PlayerController player;
    public Rigidbody2D rb;

    public int health = 3;

    public float dectectionDistance = 5;
    public float stoppingDisatance = 1;
    public float speed = 3;
    public float damageTime = 1;
    public float damageCooldownTime = 1;

    public bool isFollowing = false;
    public bool isAttacking = false;
    public bool canAttack = false;
    public bool basicEnemyDmg = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(player.transform.position.x - transform.position.x) <= dectectionDistance)
            Mathf.Abs(player.transform.position.y - transform.position.y) <= stoppingDisatance)
            isFollowing = true;
        else
            isFollowing = false;


        if (isFollowing)
        {
            if (player.transform.position.x > transform.position.x)
            {
                rb.linearVelocityX = speed;
            }
            if (player.transform.position.x < transform.position.x)
            {
                rb.linearVelocityX = -speed;
            }
            if (dectectionDistance <= stoppingDisatance)
                rb.linearVelocityX = 0;
        }
        else
            rb.linearVelocityX = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            health--;
        }

        if (collision.gameObject.tag == "basicEnemy")
        { 
           if (!basicEnemyDmg)
                StartCorountine("basicEnemyDmg")
                basicEnemyDmg = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            //Stop the character

            //attack (do damage)

          //if 

            //apply damagecooldown(probaly with a coroutine

          //if 

            //resume movemnet

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "basicEnemy")
        {
            if (basicEnemyDmg)
        }
    }

}
