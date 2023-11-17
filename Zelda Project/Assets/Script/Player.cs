using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Componentes: ")]
    public Animator animator;
    public Rigidbody2D rb;

    [Header("Variaveis int: ")]
    public int life;

    [Header("Variaveis float: ")]
    public float speed = 5f;

    public float horizontal;
    public float vertical;

    [Header("Variaveis bool: ")]
    public bool isAttacking = false;
    public bool AttackUp = false;
    public bool AttackDown = false;
    public bool AttackLeft = false;
    public bool AttackRight = false;
    public bool walkLiberado = true;
    public bool EspadaLiberada = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (walkLiberado == true)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            Vector2 movement = new Vector2(horizontal * speed, vertical * speed);
            if (walkLiberado == true)
            {
                rb.velocity = movement;
                UpdateAnimationState();
            }

            if (Input.GetMouseButton(0) && isAttacking == false)
            {
                StartCoroutine(AttackCoroutine());
            }
        }
    }

    void UpdateAnimationState()
    {
        if (walkLiberado == true)
        {
            if (horizontal > 0)
            {
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
                animator.SetInteger("Transition", 5);
                AttackLeft = true;
                AttackRight = false;
                AttackUp = false;
                AttackDown = false;
            }
            else if (horizontal < 0)
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                animator.SetInteger("Transition", 5);
                AttackLeft = false;
                AttackRight = true;
                AttackUp = false;
                AttackDown = false;
            }
            else if (vertical > 0)
            {
                AttackUp = true;
                AttackDown = false;
                AttackLeft = false;
                AttackRight = false;
                animator.SetInteger("Transition", 4);
                if (Input.GetKeyUp(KeyCode.W))
                {
                    AttackUp = false;
                }
            }
            else if (vertical < 0)
            {
                AttackDown = true;
                AttackUp = false;
                AttackLeft = false;
                AttackRight = false;
                animator.SetInteger("Transition", 3);
                if (Input.GetKeyUp(KeyCode.S))
                {
                    AttackDown = false;
                }
            }
            else
            {
                animator.SetInteger("Transition", 0);
                AttackUp = false;
                AttackDown = true;
                AttackLeft = false;
                AttackRight = false;
            }
        }
    }

    IEnumerator AttackCoroutine()
    {
        if (EspadaLiberada == true)
        {
            isAttacking = true;
            walkLiberado = false;
            rb.velocity = Vector2.zero;
            if (AttackUp)
            {
                animator.SetInteger("Transition", 8);
            }
            else if (AttackDown)
            {
                animator.SetInteger("Transition", 6);
            }
            else if (AttackLeft || AttackRight)
            {
                animator.SetInteger("Transition", 7);
            }
            yield return new WaitForSeconds(0.3f);
            walkLiberado = true;
            animator.SetInteger("Transition", 0);
        
            yield return new WaitForSeconds(1f);
            isAttacking = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }
}
