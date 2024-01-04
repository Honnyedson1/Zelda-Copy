using System;
using System.Collections;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{ 
    [Header("Componentes: ")]
    public Animator animator;
    public Rigidbody2D rb;
    public GameObject Arco;
    public Vector3 lastCheckpointPosition;
    private GameObject player;

    [Header("TelePorts")] 
    public GameObject TeleportVila;
    public GameObject TeleportCastelo;
    public GameObject TeleportevilaPricipal;
    public GameObject TeleportePraia;
    public GameObject PortaDoCastelo;
        
    [Header("Variaveis int: ")]
    public int life;
    public int keysInventory;
    public int flechas;
    public int coin;
    public  int Vida = 4;
    public int KeysDoors;
    

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
    public bool isdead;
    public bool trocou;
    
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
    public Text TextVida;

    void Start()
    {
        TextVida.text = Vida.ToString(); 
        UpdateCanvas();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        keysInventory = 0;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        
        if (isdead == false)
        {
            die();
            AtackETroca();
            UpdateCanvas();
        }
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
        TextVida.text = " " + Vida;
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

        Vector2 movement = new Vector2(horizontal, vertical)* speed;
        rb.velocity = movement;
        UpdateAnimationState();
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
        if (other.gameObject.tag == "Keydoors" && KeysDoors < 1 )
        {
            KeysDoors++;
            KeysDors.SetActive(true);
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
        else if (other.gameObject.tag == "Vida")
        {
            Vida++;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Checkpoint")
        {
            SaveCheckpoint(other.transform.position); 
            Destroy(other.GetComponent<BoxCollider2D>());
            other.GetComponent<Animator>().SetBool("Liberado", true);
        }
        if (other.gameObject.tag == "Trocadevila")
        {
            trocou = true;
        }
        if (other.gameObject.tag == "Espinhos")
        {
            life = 0;
        }

        if (other.gameObject.layer == 9)
        {
            player.transform.position = TeleportePraia.gameObject.transform.position;
        }
        if (other.gameObject.layer == 10)
        {
            player.transform.position = TeleportVila.gameObject.transform.position;
        }
        if (other.gameObject.layer == 11)
        {
            player.transform.position = TeleportevilaPricipal.gameObject.transform.position;
        }
        if (other.gameObject.layer == 12)
        {
            player.transform.position = TeleportCastelo.gameObject.transform.position;
        }
        if (other.gameObject.layer == 13)
        {
            player.transform.position = PortaDoCastelo.gameObject.transform.position;
        }
    }
    void die()
    {
        if (life <= 0)
        {
            respawn();
        }

        if (Vida <=0 )
        {
            SceneManager.LoadScene(1);
        }
    }
    void respawn()
    {
        StartCoroutine(RespawnCoroutine());
    }
    IEnumerator RespawnCoroutine()
    {
        animator.SetTrigger("Die");
        isdead = true;
        walkLiberado = false;
        Vida--;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(2f);
        isdead = false;
        walkLiberado = true;
        animator.SetTrigger("Respawn");
        player.transform.position = lastCheckpointPosition;
        life = 3;
    }
    void SaveCheckpoint(Vector3 checkpointPosition)
    {
        lastCheckpointPosition = checkpointPosition;
    }

    public void getHit(int dmg)
    {
        life -= dmg;
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