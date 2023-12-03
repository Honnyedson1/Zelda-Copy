using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    public float detectionRadius = 5f;
    public int vida = 3;
    private bool isdead = false;
    private int dano = 1;
    public float knockbackForce = 10f;

    private Transform player;
    private bool isChasing = false;
    private Rigidbody2D rig;

    public float meleeRange = 0.08f;
    private float meleeCooldown = 1f;
    private bool canAttack = true;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        die();
        if (!isdead)
        {
            CheckPlayerDistance();

            if (isChasing)
            {
                ChasePlayer();
                if (canAttack && Vector2.Distance(transform.position, player.position) < meleeRange)
                {
                    StartCoroutine(MeleeAttack());
                }
            }
        }
    }

    void CheckPlayerDistance()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        isChasing = distanceToPlayer < detectionRadius;
    }

    void ChasePlayer()
    {
        if (rig != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rig.velocity = direction * speed;
        }
    }

    void die()
    {
        if (vida <= 0)
        {
            isdead = true;
            Destroy(gameObject, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            vida--;
            Vector2 adjustPositionDirection = (transform.position - other.transform.position).normalized;
            transform.position = new Vector2(transform.position.x + adjustPositionDirection.x * 1f, transform.position.y + adjustPositionDirection.y * 1f);
            if (rig != null)
            {
                rig.velocity = Vector2.zero;
            }
        }
        if (other.gameObject.tag == "Flecha")
        {
            vida--;
            Vector2 adjustPositionDirection = (transform.position - other.transform.position).normalized;
            transform.position = new Vector2(transform.position.x + adjustPositionDirection.x * 1f, transform.position.y + adjustPositionDirection.y * 1f);
            if (rig != null)
            {
                rig.velocity = Vector2.zero;
            }
        }
    }
    IEnumerator MeleeAttack()
    {
        speed = 0f;
        canAttack = false;
        yield return new WaitForSeconds(0.5f);
        if (Vector2.Distance(transform.position, player.position) > meleeRange)
        {
            StopCoroutine(MeleeAttack());
        }
        else
        {
            player.GetComponent<Player>()?.getHit(dano);
        }
        yield return new WaitForSeconds(meleeCooldown);
        canAttack = true;
        speed = 2f;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeRange);
    }
}
