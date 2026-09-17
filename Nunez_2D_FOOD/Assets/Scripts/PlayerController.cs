using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float attackTime = .5f;
    public float attackCooldownTime = 1f; 

    public bool isAttacking = false;
    public bool canAttack = false;

    public Vector2 moveInput = Vector2.zero;

    public GameObject currentWeaponOb;
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
        rb.linearVelocity = moveInput * speed;
    }


    public void Move(InputAction.CallbackContext context)
    {
       moveInput = context.ReadValue<Vector2>();
    }

    public void Attack()
    {
        if (currentWeaponOb != null & canAttack )
        {
            isAttacking = true;
            currentWeaponOb.transform.GetChild(0).gameObject.SetActive(true);
            canAttack = false;
            StartCoroutine("attackDuration");
        }
    }

    IEnumerator attackDuration()
    {
        yield return new WaitForSeconds(attackTime);

        isAttacking = false;
        currentWeaponOb.transform.GetChild(0).gameObject.SetActive(false);
        StartCoroutine("attackCooldown");

    }

    IEnumerator attackCooldown()
    {
        yield return new WaitForSeconds(attackCooldownTime);

        isAttacking = true;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            collision.gameObject.transform.SetPositionAndRotation(weaponSlot.position, new Quaternion(0, 0, -90f, 90));

            collision.gameObject.transform.SetParent(weaponSlot);

            collision.rigidbody.bodyType = RigidbodyType2D.Kinematic;
            collision.rigidbody.simulated = false;

            collision.collider.enabled = false;

            currentWeaponOb = collision.gameObject;
            canAttack = true;

        }
    }

}
