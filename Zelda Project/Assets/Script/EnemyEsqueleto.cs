using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEsqueleto : MonoBehaviour
{
    public float velocidadeMovimento = 3f;
    public float campoDeVisao = 10f;
    public float alcanceDeAtaque = 2f;
    public float velocidadeAtaque = 2f;
    public Transform[] waypoints;
    private int waypointIndex = 0;
    public int vida = 4;
    public bool isdead;
    public Player controler;

    private Transform jogador;
    private Rigidbody2D rb;
    private bool podeAtacar = true;
    private Animator anim;

    void Start()
    {
        controler = FindObjectOfType<Player>();
        anim = GetComponent<Animator>();
        jogador = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        die();
        if (Vector2.Distance(transform.position, jogador.position) >= alcanceDeAtaque)
        {
            StopCoroutine(AtacarJogador());
        }
        if (isdead == false)
        {
            if (DetectarJogador())
            {
                MoverParaJogador();
                if (Vector2.Distance(transform.position, jogador.position) < alcanceDeAtaque)
                {
                    if (podeAtacar)
                    {
                        StartCoroutine(AtacarJogador());
                    }
                }
            }
            else
            {
                anim.SetInteger("Transition", 0);
                MoverEntreWaypoints();
            }
        }

    }

    void MoverEntreWaypoints()
    {
        // Verificar se o inimigo chegou ao waypoint atual
        if (Vector2.Distance(transform.position, waypoints[waypointIndex].position) < 0.1f)
        {
            // Mudar para o próximo waypoint na lista
            waypointIndex = (waypointIndex + 1) % waypoints.Length;

            // Dar um flip no eixo X
            Vector3 escala = transform.localScale;
            escala.x *= -1;
            transform.localScale = escala;
        }

        // Mover em direção ao waypoint atual
        Vector2 direcaoMovimento = (waypoints[waypointIndex].position - transform.position).normalized;
        rb.MovePosition(rb.position + direcaoMovimento * velocidadeMovimento * Time.deltaTime);
    }
    bool DetectarJogador()
    {
        // Verificar se o jogador está dentro do campo de visão
        return Vector2.Distance(transform.position, jogador.position) <= campoDeVisao;
    }

    void MoverParaJogador()
    {
        Vector2 direcao = (jogador.position - transform.position).normalized;
        rb.MovePosition(rb.position + direcao * velocidadeMovimento * Time.deltaTime);

        // Verificar a direção do movimento e dar flip se necessário
        if (direcao.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (direcao.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }


    IEnumerator AtacarJogador()
    {
        if (Vector2.Distance(transform.position, jogador.position) <= alcanceDeAtaque)
        {
            podeAtacar = false;
            anim.SetInteger("Transition", 1);
            yield return new WaitForSeconds(1.0f);
            controler.getHit(1);
            yield return new WaitForSeconds(0.8f);
            yield return new WaitForSeconds(1.0f);
            anim.SetInteger("Transition", 0);
            podeAtacar = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8 || other.gameObject.tag == "Flecha")
        {
            vida--;
            Vector2 adjustPositionDirection = (transform.position - other.transform.position).normalized;
            transform.position = new Vector2(transform.position.x + adjustPositionDirection.x * 1f, transform.position.y + adjustPositionDirection.y * 1f);
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    void die()
    {
        if (vida <= 0)
        {
            isdead = true;
            anim.SetInteger("Transition", 2);
            Destroy(gameObject.GetComponent<BoxCollider2D>());
        }
    }

    // Gizmos para visualizar o campo de visão e waypoints no Editor Unity
    private void OnDrawGizmos()
    {
        // Campo de Visão
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, campoDeVisao);

        // Alcance de Ataque
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, alcanceDeAtaque);

        // Waypoints
        Gizmos.color = Color.blue;
        foreach (Transform waypoint in waypoints)
        {
            Gizmos.DrawWireSphere(waypoint.position, 0.2f);
        }
    }
}
