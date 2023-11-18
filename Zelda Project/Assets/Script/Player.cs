using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Componentes: ")]
    public Animator animator;
    public Rigidbody2D rb;
    public GameObject Arco;
    

    [Header("Variaveis int: ")]
    public int life;
    public  int keys;
    public int flechas;

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
    public bool Espada2Liberada = false;
    public bool ArcoLiberado = false;
    public bool AtackArco;
    public bool EstouComEspada = true;
    
    [Header("Arco e Flecha:")]
    public GameObject flechaPrefab; 
    public Transform firePoint;     
    public float flechaSpeedV;
    public float flechaSpeedH; 
    

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

            if (Input.GetMouseButton(0) && isAttacking == false && EstouComEspada == true)
            {
                StartCoroutine(AttackCoroutine());
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && ArcoLiberado == true)
        { 
            AtackArco = true;
            EstouComEspada = false;
            Arco.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            EstouComEspada = true;
            AtackArco = false;
            Arco.gameObject.SetActive(false);
        }
        if (Input.GetMouseButton(0) && AtackArco == true && isAttacking == false)
        {
            StartCoroutine(CorotinaAtaqueArco());
        }
    }

    void UpdateAnimationState()
    {
        if (walkLiberado == true)
        {
            if (horizontal > 0)
            {
               
                flechaSpeedH = 10f;
                flechaSpeedV = 0f;
                flechaPrefab.transform.eulerAngles = new Vector3(180f, 0f, 0f);
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
                animator.SetInteger("Transition", 5);
                AttackLeft = true;
                AttackRight = false;
                AttackUp = false;
                AttackDown = false;
            }
            else if (horizontal < 0)
            {
                
                flechaSpeedH = -10f;
                flechaSpeedV = 0f;
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                animator.SetInteger("Transition", 5);
                AttackLeft = false;
                AttackRight = true;
                AttackUp = false;
                AttackDown = false;
            }
            else if (vertical > 0)
            {
               
                flechaSpeedV = 10f;
                flechaSpeedH = 0f;
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
                flechaSpeedV = -10f;
                flechaSpeedH = 0f;
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
                flechaSpeedV = -10f;
                flechaSpeedH = 0f;
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
        if (Espada2Liberada == true)
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
        
            yield return new WaitForSeconds(0.2f);
            isAttacking = false;
        }
    }
    IEnumerator CorotinaAtaqueArco()
    {
        if (ArcoLiberado && flechas > 0)
        {
            flechas--;
            isAttacking = true;
            walkLiberado = false;
            rb.velocity = Vector2.zero;
            GameObject flecha = Instantiate(flechaPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D flechaRb = flecha.GetComponent<Rigidbody2D>();
            flechaRb.velocity = new Vector2(flechaSpeedH, flechaSpeedV);
            float angle = 0f;
            if (AttackUp)
            {
                angle = 0f;
            }
            else if (AttackDown)
            {
                angle = 180f;
            }
            else if (AttackLeft)
            {
                angle = -90f;
            }
            else if (AttackRight)
            {
                angle = 90f;
            }
            flecha.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Destroy(flecha, 2f);
            yield return new WaitForSeconds(0.2f);
            walkLiberado = true;
            animator.SetInteger("Transition", 0);
            yield return new WaitForSeconds(1);

            isAttacking = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Espada")
        {
            EspadaLiberada = true;
            Espada2Liberada = false;
            Destroy(other.gameObject, 0.5f);
        }
        if (other.gameObject.tag == "Espada2")
        {
            EspadaLiberada = false;
            Espada2Liberada = true;
            Destroy(other.gameObject, 0.5f);
        }
        if (other.gameObject.tag == "Arco")
        {
            ArcoLiberado = true;
            flechas += 10;
            Destroy(other.gameObject, 0.5f);
        }

        if (other.gameObject.tag == "Keys" && keys < 1 )
        {
            keys++;
            BauRandon bau= FindObjectOfType<BauRandon>();
            bau.SomarKeys();
            Destroy(other.gameObject, 0.2f);

        }
    }
    public void subtrairKeys(int value)
    {
        keys -= value;
    }
}