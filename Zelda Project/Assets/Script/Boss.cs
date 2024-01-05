using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float longRangeAttackCooldown = 3f;
    public float meleeAttackCooldown = 2f;
    public float movementRange = 3f;
    public float movementSpeed = 2f;
    public float attackDistanceThreshold = 2f; // Distância mínima para atacar
    public Transform player;
    public GameObject cloudPrefab; // Prefab da nuvem
    public Animator anim;

    private bool isAttacking = false;

    void OnDrawGizmosSelected()
    {
        // Desenha uma esfera vermelha representando o range de ataque melee
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, movementRange);

        // Desenha uma esfera azul representando o range de ataque de longa distância
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(player.position, movementRange);

        // Desenha uma esfera verde representando a distância mínima para atacar
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackDistanceThreshold);
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(BossAI());
    }

    IEnumerator BossAI()
    {
        while (true)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Verifica se o jogador está dentro do range do inimigo
            if (distanceToPlayer <= movementRange)
            {
                if (!isAttacking)
                {
                    // Verifica se a distância é menor que o limite para iniciar o ataque
                    if (distanceToPlayer <= attackDistanceThreshold)
                    {
                        if (distanceToPlayer > movementRange)
                        {
                            // Ataque de longa distância
                            yield return StartCoroutine(LongRangeAttack());
                        }
                        else
                        {
                            // Ataque melee
                            yield return StartCoroutine(MeleeAttack());
                        }
                    }
                }
            }
            else
            {
                // Fique parado se o jogador estiver fora do alcance
                isAttacking = false;
                anim.SetInteger("Transition", 0); // Defina a animação para parado
            }

            // Movimento lateral
            yield return StartCoroutine(MoveSideways());
            yield return null;
        }
    }

    IEnumerator LongRangeAttack()
    {
        isAttacking = true;
        Debug.Log("Boss atacando de longe!");
        anim.SetInteger("Transition", 2);
        if (cloudPrefab != null)
        {
            // Criação e instanciamento da nuvem sobre a cabeça do jogador
            GameObject cloud = Instantiate(cloudPrefab, player.position + Vector3.up * 2f, Quaternion.identity);
            Destroy(cloud, longRangeAttackCooldown); // Destroi a nuvem após o cooldown do ataque
        }

        yield return new WaitForSeconds(longRangeAttackCooldown);

        isAttacking = false;
    }

    IEnumerator MeleeAttack()
    {
        isAttacking = true;
        Debug.Log("Boss atacando melee!");
        anim.SetInteger("Transition", 1); // Defina a animação para o ataque melee

        // Lógica de ataque melee
        // Adicione aqui a lógica para o ataque de espada no corpo a corpo

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
