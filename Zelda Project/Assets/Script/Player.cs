using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Componentes: ")]
    public Animator animator;
    public Rigidbody2D rb;
    public GameObject Arco;

    [Header("Variaveis int: ")]
    public int life;
    public int keysInventory;
    public int flechas;
    public int coin;

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
    
    [Header("Arco e Flecha: ")]
    public GameObject flechaPrefab; 
    public Transform firePoint;     
    public float flechaSpeedV;
    public float flechaSpeedH;

    [Header("Canvas: ")] 
    public GameObject SwoordM;
    public GameObject SwoordF;
    public GameObject ArcoCanvas;
    public GameObject KeysDors;
    public GameObject KeysC;
    public GameObject IndicadorArco;
    public GameObject IndicadorEspada;
    public Text TextMoeda;
    public Text TextLife;
    public Text TextFlechas;

    void Start()
    {
        UpdateCanvas();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        keysInventory = 0;
    }
    void Update()
    {
        AtackETroca();
        UpdateCanvas();
    }
    private void FixedUpdate()
    {
        Move();
        if (flechas <= 0)
        {
            ArcoLiberado = false;
            Arco.SetActive(false);
            ArcoCanvas.SetActive(false);
            IndicadorArco.SetActive(false);
        }
    }
    void UpdateCanvas()
    {
        TextFlechas.text = " " + flechas;
        TextLife.text = " " + life;
        TextMoeda.text = " " + coin;
    }
    void AtackETroca()
    {
        if (Input.GetKeyDown(KeyCode.Q) && ArcoLiberado == true)
        { 
            IndicadorEspada.SetActive(false);
            IndicadorArco.SetActive(true);
            AtackArco = true;
            EstouComEspada = false;
            Arco.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            IndicadorEspada.SetActive(true);
            IndicadorArco.SetActive(false);
            EstouComEspada = true;
            AtackArco = false;
            Arco.gameObject.SetActive(false);
        }
        if (Input.GetMouseButton(0) && AtackArco == true && isAttacking == false)
        {
            StartCoroutine(CorotinaAtaqueArco());
        }
    }
    void Move()
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
            SwoordM.SetActive(true);
            SwoordF.SetActive(false);
            EspadaLiberada = true;
            Espada2Liberada = false;
            Destroy(other.gameObject, 0.5f);
        }
        if (other.gameObject.tag == "Espada2")
        {
            SwoordF.SetActive(true);
            SwoordM.SetActive(false);
            EspadaLiberada = false;
            Espada2Liberada = true;
            Destroy(other.gameObject, 0.5f);
        }
        if (other.gameObject.tag == "Arco")
        {
            ArcoCanvas.SetActive(true);
            ArcoLiberado = true;
            flechas += 10;
            Destroy(other.gameObject, 0.5f);
        }

        if (other.gameObject.tag == "Keys" && keysInventory < 1 )
        {
            AddKey();
            KeysC.SetActive(true);
            Destroy(other.gameObject, 0.2f);

        }
        if (other.gameObject.tag == "Coin")
        {
            coin++;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Healf")
        {
            life++;
            Destroy(other.gameObject);
        }
    }
    public bool HasKey()
    {
        return keysInventory > 0;
    }

    public void UseKey()
    {
        keysInventory--;
    }

    public void AddKey()
    {
        keysInventory++;
    }
}