using UnityEngine;

public class basicEnemy : MonoBehaviour
{
    public PlayerController player;
    public Rigidbody2D rb;

    public int health = 3;

    public float range = 5;
    public float speed = 3;
    public float attackTime = 1f;
    public float attackCooldownTime = 1f;

    public bool isFollowing = false;
    public bool isAttacking = false;
    public bool canAttack = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(player.transform.position.x - transform.position.x) <= range &&
            Mathf.Abs(player.transform.position.y - transform.position.y) <= range)
            isFollowing = true;
        else
            isFollowing = false;


        if (isFollowing)
        {
            rb.rotation = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;

            rb.linearVelocity = (transform.right * speed);
        }
        else
            rb.linearVelocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            health--;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            //Stop the character

            //attack (do damage)

            //apply damge cooldown(probaly with a coroutine


            //resume movemnet

        }
    }
}
