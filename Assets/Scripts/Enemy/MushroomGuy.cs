using UnityEngine;
using UnityEngine.AI;

public class MushroomGuy : MonoBehaviour
{
    public Transform[] waypoints; 
    public Transform player; 
    public GameObject poisonCloudPrefab; // Assign poison cloud prefab

    public float visionRange = 10f;
    public float attackRange = 2f;
    public float poisonCooldown = 3f; // Time between poison attacks

    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;
    private bool chasing = false;
    private float lastPoisonTime = -999f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }

        PatrolToNextWaypoint();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= visionRange)
        {
            chasing = true;
        }
        else if (chasing && distanceToPlayer > visionRange + 2f)
        {
            chasing = false;
            PatrolToNextWaypoint();
        }

        if (chasing)
        {
            if (distanceToPlayer > attackRange)
            {
                agent.SetDestination(player.position);
            }
            else
            {
                Attack();
            }
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            PatrolToNextWaypoint();
        }
    }

    void PatrolToNextWaypoint()
    {
        if (waypoints.Length == 0) return;

        agent.SetDestination(waypoints[currentWaypointIndex].position);
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

    void Attack()
    {
        agent.ResetPath(); // Stop moving

        if (Time.time - lastPoisonTime >= poisonCooldown)
        {
            Instantiate(poisonCloudPrefab, transform.position, Quaternion.identity);
            lastPoisonTime = Time.time;
        }
    }
    }