using JetBrains.Annotations;
using System.Collections;
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

    public GameObject enemyWeaponObj;
    Transform enemyWeaponSlot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float targetDistance = Vector2.Distance(player.transform.position, transform.position);

        isFollowing = targetDistance <= dectectionDistance;

        if (isFollowing)
        {
            rb.rotation = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;

            rb.linearVelocity = transform.right * speed;
        }
        else
            rb.linearVelocity = Vector2.zero;
    }

    public void Attack()
    {
        if (enemyWeaponObj != null && canAttack)
        { 
         isAttacking = true;
            enemyWeaponObj.transform.GetChild(0).gameObject.SetActive(false);
            StartCoroutine("damgeCooldownTime");
        }
    }

    IEnumerator damage()
    {
        yield return new WaitForSeconds(damageTime);
    }

    IEnumerator damageCooldown()
    {
        yield return new WaitForSeconds(damageCooldownTime);

        canAttack = true;
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
                StartCoroutine("basicEnemyDmg");
                basicEnemyDmg = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            //Stop the character

            //attack (do damage

            //apply damagecooldown(probaly with a coroutine

            //resume movemnet
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "basicEnemy")
        {
            if (basicEnemyDmg)
            {

            }
        }

    }

}
