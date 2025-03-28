using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BatAI : MonoBehaviour
{
    [Header("References")]
    public ParticleSystem impactParticles;
    // private Collider batCollider;
    private Rigidbody batRigidBody;
    public BatWaypointManager batWaypointManager;
    private List<GameObject> batWaypointNodes;

    public AudioSource idleChirpAudioSource;


    [Header("Debug")]
    public bool debugPrint;

    [Header("Pathing")]
    private List<GameObject> currentPath;
    private int currentPathIndex = 0;

    [Header("Speeds")]
    public float patrolSpeed;
    public float chaseSpeed;
    public float rotationSpeed;

    [Header("Attacking")]
    public int lungeDamage = 30;
    private bool lethal = false;

    private float attackTimer;
    public float windbackTime;
    public float lungeTime;
    public float vulnerableTime;

    public float windbackSpeed;
    public float lungeForce;

    [Header("Timers")]
    public float circlingTimer = 0f;
    public float timeToCircle = 5f;

    [Header("State")]
    [SerializeField] private BatState state = BatState.WINDING_BACK;
    private enum BatState { PATROLLING, CHASING, WINDING_BACK, LUNGING, VULNERABLE, FOLLOWING_PATH, DEAD };

    [Header("Other")]
    // My idea for this is that it currently targets your belly, 
    // so it might be nice to have it target the head instead.
    public readonly Vector3 playerheadOffset = Vector3.up * 0.1f; 
    
    private const string playerPartTag = "PlayerPart";
    private const string playerTag = "Player";
    private Transform player;

    public float detectionRadius;
    public float attackRadius;
    public float waypointReachedDistance = 0.1f;

    // STATE: Simply selects a random waypoint, since the player's location is unknown.
    private void PatrolState() {
        if (debugPrint) Debug.Log("BAT: Patrolling, setting new path");
        PathRandom();

        // Without this function call, we would stop for one frame before following the path.
        FollowPathState();

        state = BatState.FOLLOWING_PATH;
    }

    // STATE: We have seen the player and are now chasing them. If we can't see them, move to their last known location.
    private void ChaseState() {
        if (!CanSeePlayer()) {
            PathPlayer();
            currentPathIndex = 0;
            state = BatState.FOLLOWING_PATH;
            if (debugPrint) Debug.Log("BAT: Player lost, pathing to last known node position: " + currentPath[currentPath.Count-1].ToString());
            return;
        }
        if (Vector3.Distance(player.transform.position, transform.position) < attackRadius) {
            state = BatState.WINDING_BACK;
            if (debugPrint) Debug.Log("BAT: Attacking player");
            return;
        }

        // Look and move towards the player
        FaceTarget(player.position);
        transform.position = Vector3.MoveTowards(transform.position, player.position+playerheadOffset, chaseSpeed * Time.deltaTime);
    }

    // STATE: First part of the bat's lunge. Slowly move back to telegraph the attack.
    private void WindBackState() { 
        // After a certain amount of time, stop winding back
        attackTimer += Time.deltaTime;
        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        FaceTarget(player.position);
        if (attackTimer >= windbackTime) {
            attackTimer = 0;
            batRigidBody.AddForce(dirToPlayer * lungeForce, ForceMode.Impulse);
            lethal = true;
            state = BatState.LUNGING;    
        }
        // Check if we have room behind us, move back if we do
        float moveDistance = windbackSpeed * Time.deltaTime;
        if (Physics.Raycast(transform.position, -dirToPlayer, out RaycastHit raycastResult, moveDistance*1.1f)) {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, -99f*dirToPlayer, moveDistance);
    }

    // STATE: After we have winded back, the transition to this function launches us forward. Now we are flying through the air, damaging on contact.
    private void LungeState() {
        // If we hit a wall, play effects
        float wallDetectionDistance = 1f;
        Debug.DrawLine(transform.position, transform.position+transform.forward*wallDetectionDistance, Color.cyan, 0.1f);
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastResult, wallDetectionDistance)) {
            batRigidBody.velocity = Vector3.zero;
            impactParticles.Play();
            attackTimer  = 2*lungeTime;
        }
        // The force is applied in the transition from wind back. We just wait.
        attackTimer += Time.deltaTime;
        if (attackTimer < lungeTime) {
            return;
        }

        batRigidBody.velocity *= 0.7f; // Slow down
        if (batRigidBody.velocity.magnitude > 0.03f) {
            return;
        }
        batRigidBody.velocity = Vector3.zero;
        // We have stopped, now we are vulnerable.
        attackTimer = 0;
        state = BatState.VULNERABLE;
        lethal = false;
        // Play stun particle effects?
    }

    // STATE: After lunging, remain still and vulnerable for a short period of time.
    private void VulnerableState() {
        attackTimer += Time.deltaTime;
        batRigidBody.velocity = Vector3.zero;
        if (attackTimer < vulnerableTime) {
            return;
        }
        attackTimer = 0;
        state = BatState.CHASING;
    }

    // STATE: Follow the currently held path (may be last seen location of player, may be random waypoint from patrol state)
    private void FollowPathState() {
        // If we can see the player, exit and start chasing
        if (CanSeePlayer()) {
            state = BatState.CHASING;
            if (debugPrint) Debug.Log("BAT: Player seen, chasing!");
            return;
        }

        // If we don't have a path defined, just start patrolling. Shouldn't happen.
        if (currentPath == null || currentPathIndex >= currentPath.Count - 1) {
            batWaypointNodes = batWaypointManager.batWaypointNodes;
            PathRandom();
            return;
        }

        // Check if we have reached the current node in our path, increase target node index if true
        Transform currentNodeTransform = currentPath[currentPathIndex].transform;
        if (Vector3.Distance(currentNodeTransform.position, transform.position) < waypointReachedDistance) {
            currentPathIndex++;
        }

        // Move and look towards next node by one step
        Transform nextNodeTransform = currentPath[currentPathIndex].transform;
        transform.position = Vector3.MoveTowards(transform.position, nextNodeTransform.position, patrolSpeed * Time.deltaTime);
        FaceTarget(nextNodeTransform.position);
    }

    // Quick function for BFS pathing to a random waypoint
    private void PathRandom() {
        if (batWaypointNodes.Count == 0) {
            Debug.LogError("BAT: No waypoints available.");
            return;
        }

        int randomWaypointIndex = UnityEngine.Random.Range(0, batWaypointNodes.Count);
        currentPath = batWaypointManager.BFS(batWaypointManager.GetClosestNode(transform.position), batWaypointNodes[randomWaypointIndex]).ToList<GameObject>();
        currentPathIndex = 0;
    }

    // Quick function for BFS pathing to player
    private void PathPlayer() {
        if (batWaypointNodes.Count == 0) {
            Debug.LogError("BAT: No waypoints available.");
            return;
        }

        currentPath = batWaypointManager.BFS(batWaypointManager.GetClosestNode(transform.position), batWaypointManager.GetClosestNode(player.position)).ToList<GameObject>();
        currentPathIndex = 0;
    }

    // Keeps our bat aligned with a target while not allowing for instantaneous, unnatural turns
    private void FaceTarget(Vector3 targetPos) {
        Vector3 dir = (targetPos - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        if (dir.sqrMagnitude < 0.0001f) return;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

    // Raycasts and returns true if player is seen
    private bool CanSeePlayer() {
        Vector3 dir = player.gameObject.GetComponent<Collider>().bounds.center - transform.position;
        Vector3 scaledDir = dir.normalized * detectionRadius;
        
        RaycastHit raycastResult;
        Physics.Raycast(transform.position, scaledDir, out raycastResult, detectionRadius);
        
        Debug.DrawLine(transform.position, transform.position + scaledDir, Color.red);

        if (raycastResult.collider == null) return false;
        GameObject hitGameObject = raycastResult.collider.gameObject;
        if (hitGameObject.tag.Equals(playerTag) || hitGameObject.tag.Equals(playerPartTag)) return true;

        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        batWaypointManager = GameObject.FindGameObjectWithTag("BatWaypointManager").GetComponent<BatWaypointManager>();
        batWaypointNodes = GameObject.FindGameObjectsWithTag("BatWaypointNode").ToList<GameObject>();
        // batCollider = GetComponent<CapsuleCollider>();
        batRigidBody = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag(playerTag).transform;
        idleChirpAudioSource.time = UnityEngine.Random.Range(0f, idleChirpAudioSource.clip.length - 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        float gizmoHeight = 2f;
        Color batColor = Color.magenta;
        Debug.DrawLine(transform.position-Vector3.up*gizmoHeight/2, transform.position+Vector3.up*gizmoHeight/2, batColor);
        Debug.DrawLine(transform.position-Vector3.left*gizmoHeight/2, transform.position+Vector3.left*gizmoHeight/2, batColor);
        Debug.DrawLine(transform.position-Vector3.forward*gizmoHeight/2, transform.position+Vector3.forward*gizmoHeight/2, batColor);
        if (state == BatState.DEAD) { // Not used, currently covered by enemy health script
            Destroy(gameObject);
        }
        else if (state == BatState.PATROLLING) {
            PatrolState();
        }
        else if (state == BatState.WINDING_BACK) {
            WindBackState();
        }
        else if (state == BatState.LUNGING) {
            LungeState();
        }
        else if (state == BatState.VULNERABLE) {
            VulnerableState();
        }
        else if (state == BatState.FOLLOWING_PATH) {
            FollowPathState();
        }
        else if (state == BatState.CHASING) {
            ChaseState();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && lethal)
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(lungeDamage, gameObject.name);
            // animator.SetTrigger("Attack");
            // audioSource.PlayOneShot(attackSounds[Random.Range(0, attackSounds.Length - 1)]);
            lethal = false;
        }
    }
}