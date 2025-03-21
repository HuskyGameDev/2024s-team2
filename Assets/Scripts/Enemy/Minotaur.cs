using UnityEngine;
using UnityEngine.AI;

public class MinotaurAI : MonoBehaviour
{
    public Transform[] waypoints;
    public Transform player;
    public float sightRange = 15f;
    public float attackRange = 2f;
    public float chargeMinDistance = 5f; // Minimum distance required to charge
    public float patrolSpeed = 3f;
    public float chaseSpeed = 4.5f;
    public float chargeSpeed = 10f;
    public float attackCooldown = 2f;
    public float chargeCooldown = 10f;
    public float chargeDuration = 3f;
    public float stunDuration = 3f;
    public int attackDamage = 40;
    private bool lethal = false;
    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;
    private float lastAttackTime;
    private bool isChasing = false;
    private bool isCharging = false;
    private bool isStunned = false;
    private float lastChargeTime = -999f;
    private float chargeStartTime;
    private bool isAttacking = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }

        agent.speed = patrolSpeed;
        PatrolToNextWaypoint();
    }

    void Update()
    {
        if (isStunned || isCharging) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        bool playerInSight = distanceToPlayer <= sightRange;

        if (playerInSight && CanCharge(distanceToPlayer))
        {
            StartCharge();
        }
        else if (playerInSight)
        {
            StartChase();
        }
        else if (!isChasing && agent.remainingDistance < 0.5f)
        {
            PatrolToNextWaypoint();
        }

        if (isChasing && distanceToPlayer <= attackRange && Time.time > lastAttackTime + attackCooldown)
        {
            Attack();
        }
    }

    void PatrolToNextWaypoint()
    {
        if (waypoints.Length == 0) return;
        agent.speed = patrolSpeed;
        agent.SetDestination(waypoints[currentWaypointIndex].position);
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

    void StartChase()
    {
        isChasing = true;
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);
    }

    bool CanCharge(float distanceToPlayer)
    {
        if (Time.time - lastChargeTime < chargeCooldown) return false;
        if (distanceToPlayer < chargeMinDistance) return false; // Prevent charging when too close

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        if (Physics.Raycast(transform.position, directionToPlayer, sightRange))
        {
            return true;
        }
        return false;
    }

    void StartCharge()
    {
        isCharging = true;
        lastChargeTime = Time.time;
        chargeStartTime = Time.time;

        agent.speed = chargeSpeed;
        agent.SetDestination(player.position);
        Invoke(nameof(EndCharge), chargeDuration);
    }

    void EndCharge()
    {
        if (!isCharging) return; // Prevent multiple calls

        isCharging = false;
        Stun();
    }

    void Attack()
    {
        lastAttackTime = Time.time;
        Debug.Log("Minotaur attacks the player!");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isCharging && collision.gameObject.CompareTag("Wall"))
        {
            Stun();
        }
    }

    void Stun()
    {
        isCharging = false;
        isStunned = true;
        agent.isStopped = true;
        Debug.Log("Minotaur is stunned!");

        Invoke(nameof(RecoverFromStun), stunDuration);
    }

    void RecoverFromStun()
    {
        isStunned = false;
        agent.isStopped = false;
        agent.ResetPath(); // Prevents stuck movement
        PatrolToNextWaypoint(); // Resume normal behavior
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isAttacking)
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(attackDamage, gameObject.name);
            isAttacking = false; // Prevent multiple damage triggers in one attack
        }
    }
}