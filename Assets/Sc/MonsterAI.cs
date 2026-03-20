using UnityEngine;
using System.Collections;

public class MonsterAI : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;

    public int damage = 5;

    public float attackDelay = 2f;
    public float attackCooldown = 2f;

    [Header("Attack Range")]
    public Collider attackCollider;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip attackSound;

    bool playerInRange = false;
    bool isAttacking = false;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        if (attackCollider != null)
        {
            attackCollider.isTrigger = true;
        }
    }

    void Update()
    {
        if (player == null) return;

        if (!playerInRange)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                speed * Time.deltaTime
            );

            transform.LookAt(player);

            if (anim != null)
                anim.SetBool("Run", true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (!isAttacking)
            {
                StartCoroutine(Attack());
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;

        while (playerInRange)
        {
            if (anim != null)
            {
                anim.SetTrigger("Attack");
                anim.SetBool("Run", false);
            }

            // 🔊 เล่นเสียงโจมตี
            if (audioSource != null && attackSound != null)
            {
                audioSource.PlayOneShot(attackSound);
            }

            yield return new WaitForSeconds(attackDelay);

            if (playerInRange)
            {
                PlayerHealth ph = player.GetComponent<PlayerHealth>();

                if (ph != null)
                {
                    ph.TakeDamage(damage);
                }
            }

            yield return new WaitForSeconds(attackCooldown);
        }

        isAttacking = false;
    }
}