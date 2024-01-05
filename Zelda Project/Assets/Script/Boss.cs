using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float longRangeAttackCooldown = 3f;
    public float meleeAttackCooldown = 2f;
    public float movementRange = 3f;
    public float movementSpeed = 2f;
    public Transform player;

    private bool isAttacking = false;

    private void Start()
    {
        StartCoroutine(BossAI());
    }

    IEnumerator BossAI()
    {
        while (true)
        {
            if (!isAttacking)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);

                if (distanceToPlayer > movementRange)
                {
                    // Ataque de longa distância
                    StartCoroutine(LongRangeAttack());
                }
                else
                {
                    // Ataque melee
                    StartCoroutine(MeleeAttack());
                }

                // Movimento lateral
                StartCoroutine(MoveSideways());

                yield return null;
            }
        }
    }

    IEnumerator LongRangeAttack()
    {
        isAttacking = true;
        Debug.Log("Boss atacando de longe!");

        // Lógica de ataque de longa distância

        yield return new WaitForSeconds(longRangeAttackCooldown);

        isAttacking = false;
    }

    IEnumerator MeleeAttack()
    {
        isAttacking = true;
        Debug.Log("Boss atacando melee!");

        // Lógica de ataque melee

        yield return new WaitForSeconds(meleeAttackCooldown);

        isAttacking = false;
    }

    IEnumerator MoveSideways()
    {
        // Movimento lateral simples
        float direction = Mathf.Sign(Random.Range(-1f, 1f));
        Vector3 targetPosition = transform.position + Vector3.right * direction * movementRange;

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
            yield return null;
        }
    }
}