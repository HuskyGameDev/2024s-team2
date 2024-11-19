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
    private float footstepTimer = 0;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        enemy.SetDestination(Player.position);
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
            audioSource.pitch = Random.Range(0.9f, 1f);
            audioSource.PlayOneShot(stepSounds[Random.Range(0, stepSounds.Length - 1)]);
            footstepTimer = Random.Range(3f, 5f);
        }
    }

}
