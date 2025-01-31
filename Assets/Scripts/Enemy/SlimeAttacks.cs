using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SlimeAttacks : MonoBehaviour
{
    [SerializeField] ParticleSystem impactParticles;
    public AudioSource audioSource;
    public AudioClip slamSound;

    public float slamDamage;
    public float slamCooldown;
    public float slamHoverDuration;
    public float slamHoverHeight;
    public float hoverSpeed;
    public float slamSpeed;
    public Transform Player;

    public float tweenTimeout;

    private bool isSlamming;
    private float tweenLeniency = 0.1f;
    private float slamTimer;
    private Rigidbody rb;
    private DamageScript damageScript;

    // Start is called before the first frame update
    void Start()
    {
        damageScript = gameObject.GetComponent<DamageScript>();
        rb = GetComponent<Rigidbody>();
        Player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Runs on fixed delta time
    private void FixedUpdate () {
        if (!isSlamming) {
            slamTimer += Time.fixedDeltaTime;
        }
        if (slamTimer >= slamCooldown && !isSlamming)
        {
            Debug.Log("SlimeAttack: Slam attack!");
            StartCoroutine(SlamAttack());
        }
    }

    private IEnumerator SlamAttack() {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if (agent != null) agent.enabled = false;  // I had inteference from the NavMeshAgent, so I temporarily disable it.
        isSlamming = true;
        rb.useGravity = false; // Disable gravity just to clean up the physics
        rb.freezeRotation = true;

        // Wait a few seconds to telegraph the attack more
        yield return new WaitForSeconds(0.5f);

        // Tween above player
        yield return StartCoroutine(Tween(Player, Vector3.up * slamHoverHeight, hoverSpeed, tweenTimeout));
        
        Vector3 savedPlayerPos = Player.position + Vector3.down * 0.2f; //This is saved so that we just slam straight down (0.2f down since Player.position is the center)

        // Wait a few seconds
        yield return new WaitForSeconds(slamHoverDuration);
        damageScript.slam = true;
        // Go up a bit, then slam down.
        yield return StartCoroutine(Tween(transform.position + Vector3.up * 0.2f, 20f, tweenTimeout));
        yield return StartCoroutine(Tween(savedPlayerPos, slamSpeed, tweenTimeout));

        impactParticles.Play();
        audioSource.PlayOneShot(slamSound);

        rb.useGravity = true;
        // rb.AddForce(Vector3.down * slamForce); // Would've been cool to use actual physics, but I don't think it would've worked with NavMeshAgent or whatever rigidbody settings.
        rb.freezeRotation = false;
        if (agent != null) agent.enabled = true;  // I had inteference from the NavMeshAgent, so I temporarily disable it.
        isSlamming = false;
        slamTimer = 0;
        damageScript.slam = false;
        Debug.Log("SlimeAttack: Slam attack finished");
    }

    // Overloaded tween for moving to a static point. Has a timeout to avoid getting stuck.
    private IEnumerator Tween(Vector3 targetPos, float speed, float timeoutSeconds) {
        float timeoutTime = Time.time + timeoutSeconds;
        while (Vector3.Distance(transform.position, targetPos) > tweenLeniency) {
            if (Time.time >= timeoutTime) {
                Debug.Log("SlimeAttack: Timout reached on Tween!");
                yield break;
            }
            rb.MovePosition(Vector3.MoveTowards(transform.position, targetPos, speed*Time.fixedDeltaTime));
            yield return new WaitForFixedUpdate();
        }
        yield break;
    }
    
    // Overloaded tween for transforms, allowing you to track a moving game object. Has a timeout to avoid getting stuck.
    private IEnumerator Tween(Transform targetTransform, Vector3 offset, float speed, float timeoutSeconds) {
        Vector3 targetPosition = targetTransform.position + offset;
        float timeoutTime = Time.time + timeoutSeconds;
        while (Vector3.Distance(transform.position, targetPosition) > tweenLeniency) {
            if (Time.time >= timeoutTime) {
                Debug.Log("SlimeAttack: Timout reached on Tween!");
                yield break;
            }
            targetPosition = targetTransform.position + offset;
            rb.MovePosition(Vector3.MoveTowards(transform.position, targetPosition, speed*Time.fixedDeltaTime));
            yield return new WaitForFixedUpdate();
        }
        yield break;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
