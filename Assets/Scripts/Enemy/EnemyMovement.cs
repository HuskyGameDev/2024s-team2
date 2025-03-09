using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform Player;
    public int enemyType;
    public AudioSource audioSource;
    public AudioClip[] stepSounds;
    Animator animator; 
    private float footstepTimer = 0;
    private float aggroDistance = 20; // WIP distance for aggro


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy != null && enemy.enabled) {
            enemy.SetDestination(Player.position);
        }

        //aggro distance
        //WIP: Change animation to idle when stopped
        if (enemy.remainingDistance > aggroDistance)
        {
            enemy.isStopped = true;
            //animator.SetTrigger("Stop");
        }
        else
        {
            enemy.isStopped = false;
            //animator.SetTrigger("Run");
        }
    }

    void FixedUpdate()
    {
        move();
    }

    private void move()
    {
        // Play footstep sound
        footstepTimer -= Time.deltaTime;
        if (footstepTimer <= 0)
        {
            //audioSource.pitch = Random.Range(0.9f, 1f);
            audioSource.PlayOneShot(stepSounds[Random.Range(0, stepSounds.Length - 1)]);
            footstepTimer = Random.Range(3f, 5f);
        }
    }

}
