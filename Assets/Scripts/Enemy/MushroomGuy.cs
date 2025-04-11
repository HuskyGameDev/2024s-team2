using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MushroomGuy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public GameObject poisonCloudPrefab; 

    public float visionRange = 10f;
    public float attackRange = 2f;
    public float poisonCooldown = 3f; 
    public float wanderTime = 2f; 
    public float aggroDistance = 10f; 
    public float stopDuration = 2f; 

    private bool chasing = false;
    private float lastPoisonTime = -999f;
    private float wanderTimer = 0f;
    private AudioSource audioSource;
    private float footstepTimer = 0f;
    public AudioClip[] stepSounds; 

    private bool isStoppedForAttack = false;
    private float stopTimer = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }

        WanderRandomly(); 
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Aggro range check
        if (distanceToPlayer <= visionRange && !chasing)
        {
            chasing = true;
            agent.isStopped = false; 
        }
        else if (chasing && distanceToPlayer > visionRange)
        {
            chasing = false;
            agent.isStopped = true; 
        }

        
        if (chasing)
        {
            if (distanceToPlayer <= attackRange)
            {
                Attack();
            }
            else
            {
                agent.SetDestination(player.position); // follow the player
            }
        }
        else
        {
            WanderRandomly(); // wander around when not chasing
        }

        PlayFootsteps();

        
        if (isStoppedForAttack)
        {
            stopTimer -= Time.deltaTime;
            if (stopTimer <= 0f)
            {
                
                agent.isStopped = false;
                isStoppedForAttack = false;
            }
        }
    }

    void WanderRandomly()
    {
        wanderTimer -= Time.deltaTime;

        if (wanderTimer <= 0f)
        {
            Vector3 randomDirection = Random.insideUnitSphere * 10f; // 10f is the range of wander area
            randomDirection += transform.position;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, 1.0f, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position); // move to random point
            }

            wanderTimer = wanderTime; // reset wander timer
        }
    }

    void Attack()
    {
        // check if enough time has passed since the last poison cloud release
        if (Time.time - lastPoisonTime >= poisonCooldown)
        {
            // stop the MushroomGuy from moving for the set stopDuration
            agent.isStopped = true;
            isStoppedForAttack = true;
            stopTimer = stopDuration;

            // release poison cloud at the current position
            Instantiate(poisonCloudPrefab, transform.position, Quaternion.identity);
            lastPoisonTime = Time.time;
            Debug.Log("Mushroom released poison cloud!");
        }
    }

    
    void PlayFootsteps()
    {
        if (agent.velocity.magnitude > 0.1f) // only play footsteps when moving
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0)
            {
                audioSource.PlayOneShot(stepSounds[Random.Range(0, stepSounds.Length)]);
                footstepTimer = Random.Range(0.3f, 0.7f); // randomize the time between footsteps
            }
        }
    }
}