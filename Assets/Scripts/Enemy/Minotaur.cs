using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Minotaur : MonoBehaviour
{
    public Transform player;
    public float aggroRange = 15f;
    public float attackRange = 2f;
    public float chaseSpeed = 4.5f;
    public float attackCooldown = 2f;
    public int attackDamage = 40;

    public AudioSource audioSource;
    public AudioClip[] stepSounds;
    public AudioClip[] idleSounds;
    public DamageScript damageScript;

    public float trackingInterval = 10f;
    public float attackArcAngle = 60f;  

    private NavMeshAgent enemyAgent;
    private Animator animator;
    private float footstepTimer = 0;

    private bool isChasing = false;
    private bool isAttacking = false;

    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }

        if (damageScript == null)
            damageScript = GetComponent<DamageScript>();

        if (enemyAgent != null)
            enemyAgent.speed = chaseSpeed;

        StartCoroutine(TrackPlayerPositionPeriodically());
    }

    void Update()
    {
        if (player == null || enemyAgent == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Switch between tracking and chase mode
        if (distanceToPlayer <= aggroRange)
        {
            if (!isChasing)
                Debug.Log("Minotaur has entered aggro mode!");

            isChasing = true;

            if (!isAttacking)
                enemyAgent.SetDestination(player.position);

            if (distanceToPlayer <= attackRange && !isAttacking)
            {
                StartCoroutine(PerformAttack());
            }
        }
        else
        {
            if (isChasing)
                Debug.Log("Minotaur has exited aggro mode, back to tracking.");

            isChasing = false;
        }

        UpdateMovementCondition();
    }

    void FixedUpdate()
    {
        PlayFootstepSound();
        PlayIdleSound();
    }

    IEnumerator PerformAttack()
    {
        isAttacking = true;
        enemyAgent.isStopped = true;

        animator.SetTrigger("Attack1");
        Debug.Log("Minotaur is attacking!");

        yield return new WaitForSeconds(0.5f); // Adjust based on animation timing

        // Check if the player is within the attack arc
        if (IsPlayerInAttackArc())
        {
            ApplyDamage();
        }

        // Play attack sound
        if (damageScript != null && damageScript.audioSource != null && damageScript.attackSounds.Length > 0)
        {
            damageScript.audioSource.PlayOneShot(damageScript.attackSounds[Random.Range(0, damageScript.attackSounds.Length)]);
        }

        // Wait out the rest of the cooldown
        yield return new WaitForSeconds(attackCooldown - 0.5f);
        isAttacking = false;
        enemyAgent.isStopped = false;
    }

    // Check if the player is within the Minotaur's attack arc
    bool IsPlayerInAttackArc()
    {
        Vector3 toPlayer = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, toPlayer);  

        if (angle < attackArcAngle / 2f)  // If the player is within the attack arc
        {
            float dist = Vector3.Distance(transform.position, player.position);
            if (dist <= attackRange)
            {
                return true;
            }
        }
        return false;
    }

    void ApplyDamage()
    {
        // Apply damage to the player directly
        if (damageScript != null)
        {
            damageScript.damage = attackDamage;  // Set the attack damage
            damageScript.TakeDamage(); 

            Debug.Log("Player hit by Minotaur!");
        }
    }

    IEnumerator TrackPlayerPositionPeriodically()
    {
        while (true)
        {
            if (!isChasing && enemyAgent != null && player != null)
            {
                enemyAgent.SetDestination(player.position);
            }
            yield return new WaitForSeconds(trackingInterval);
        }
    }

    void PlayFootstepSound()
    {
        footstepTimer -= Time.deltaTime;
        if (footstepTimer <= 0 && enemyAgent.velocity.magnitude > 0.1f && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(stepSounds[Random.Range(0, stepSounds.Length)]);
            footstepTimer = Random.Range(3f, 5f);
        }
    }

    void PlayIdleSound()
    {
        if (enemyAgent.velocity.magnitude < 0.1f && !isChasing)
        {
            if (idleSounds.Length > 0 && audioSource != null && !audioSource.isPlaying)
            {
                audioSource.PlayOneShot(idleSounds[Random.Range(0, idleSounds.Length)]);
                footstepTimer = Random.Range(3f, 5f);
            }
        }
    }

    void UpdateMovementCondition()
    {
        if (enemyAgent != null)
        {
            float speed = enemyAgent.velocity.magnitude;
            animator.SetFloat("Movement", speed < 0.8f ? 0f : speed);
        }
    }
}