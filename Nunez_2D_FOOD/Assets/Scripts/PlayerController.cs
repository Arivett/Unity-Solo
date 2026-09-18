using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public int health = 5;
    public int maxHealth = 5;

    public float speed = 5.0f;
    public float attackTime = .5f;
    public float attackCooldownTime = 1f; 
    public float HazardDmgInterval = 1f;

    public bool isAttacking = false;
    public bool canAttack = false;
    public bool hazardDmg = false;

    public Vector2 moveInput = Vector2.zero;

    public GameObject currentWeaponObj;
    Transform weaponSlot;
    PlayerInput input;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        weaponSlot = transform.GetChild(0);

    }

    // Update is called once per frame
    void Update()
    {
        rb.rotation = Mathf.Atan2(Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y, Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x) * Mathf.Rad2Deg;

        if (health <= 0)
        {
            
        }

        rb.linearVelocity = moveInput * speed;
    }



    public void Move(InputAction.CallbackContext context)
    {
       moveInput = context.ReadValue<Vector2>();
    }

    public void Attack()
    {
        if (currentWeaponObj != null & canAttack )
        {
            isAttacking = true;
            currentWeaponObj.transform.GetChild(0).gameObject.SetActive(true);
            canAttack = false;
            StartCoroutine("attackDuration");
        }
    }

    IEnumerator attackDuration()
    {
        yield return new WaitForSeconds(attackTime);

        isAttacking = false;
        currentWeaponObj.transform.GetChild(0).gameObject.SetActive(false);
        StartCoroutine("attackCooldown");

    }

    IEnumerator attackCooldown()
    {
        yield return new WaitForSeconds(attackCooldownTime);

        isAttacking = false;
        canAttack = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            collision.gameObject.transform.SetPositionAndRotation(weaponSlot.position, Quaternion.identity);

            collision.gameObject.transform.SetParent(weaponSlot);

            collision.rigidbody.bodyType = RigidbodyType2D.Kinematic;
            collision.rigidbody.simulated = false;

            collision.collider.enabled = false;

            currentWeaponObj = collision.gameObject;
            canAttack = true;
        }

        if (collision.gameObject.tag == "Health")
        {
            health ++;

            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hazard")
        {
            health--;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
       if (collision.gameObject.tag == "Hazard")
        {
            if (!hazardDmg)
            { 
              StartCoroutine("hazardDamage");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       if (collision.gameObject.tag == "Hazard")
        {
           if (hazardDmg)
            {
                StopCoroutine("hazardDamage");
                hazardDmg = false;
            }
        }
    }

    IEnumerable hazardDamage()
    {
        hazardDmg = true;

        yield return new WaitForSeconds(HazardDmgInterval);

        health--;
        
        hazardDmg = false;
    }
}
