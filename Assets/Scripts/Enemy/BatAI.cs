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
    private Collider batCollider;
    private Rigidbody batRigidBody;

    public bool debugPrint;
    public BatWaypointManager batWaypointManager;
    private List<GameObject> batWaypointNodes;

    private List<GameObject> currentPath;
    private int currentPathIndex = 0;

    public float patrolSpeed;
    public float chaseSpeed;
    public float rotationSpeed;

    public bool damaging = false;

    private float attackTimer;
    public float windbackTime;
    public float lungeTime;
    public float vulnerableTime;

    public float windbackSpeed;
    public float lungeForce;

    private enum State { PATROLLING, CHASING, WINDING_BACK, LUNGING, VULNERABLE, FOLLOWING_PATH, DEAD };
    [SerializeField] private State state = State.WINDING_BACK;

    // My idea for this is that it currently targets your belly, 
    // so it might be nice to have it target the head instead.
    public readonly Vector3 playerheadOffset = Vector3.up * 0.1f; 
    
    private const string playerPartTag = "PlayerPart";
    private const string playerTag = "Player";
    private Transform player;

    public float detectionRadius;
    public float attackRadius;
    public float waypointReachedDistance = 0.1f;

    private void PatrolState() {
        if (debugPrint) Debug.Log("BAT: Patrolling, setting new path");
        PathRandom();

        // Without this function call, we would stop for one frame before following the path.
        FollowPathState();

        state = State.FOLLOWING_PATH;
    }

    private void ChaseState() {
        if (!CanSeePlayer()) {
            PathPlayer();
            currentPathIndex = 0;
            state = State.FOLLOWING_PATH;
            if (debugPrint) Debug.Log("BAT: Player lost, pathing to last known node position: " + currentPath[currentPath.Count-1].ToString());
            return;
        }
        if (Vector3.Distance(player.transform.position, transform.position) < attackRadius) {
            state = State.WINDING_BACK;
            if (debugPrint) Debug.Log("BAT: Attacking player");
            return;
        }

        // Look and move towards the player
        FaceTarget(player.position);
        transform.position = Vector3.MoveTowards(transform.position, player.position+playerheadOffset, chaseSpeed * Time.deltaTime);
    }

    // First part of the bat's lunge. Slowly move back to telegraph the attack.
    private void WindBackState() { 
        // After a certain amount of time, stop winding back
        attackTimer += Time.deltaTime;
        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        FaceTarget(player.position);
        if (attackTimer >= windbackTime) {
            attackTimer = 0;
            batRigidBody.AddForce(dirToPlayer * lungeForce, ForceMode.Impulse);
            damaging = true;
            state = State.LUNGING;    
        }
        // Check if we have room behind us, move back if we do
        float moveDistance = windbackSpeed * Time.deltaTime;
        if (Physics.Raycast(transform.position, -dirToPlayer, out RaycastHit raycastResult, moveDistance*1.1f)) {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, -99f*dirToPlayer, moveDistance);
    }

    // Second part of the bat's lunge. Force has been applied, now we just wait.
    private void LungeState() {
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
        state = State.VULNERABLE;
        damaging = false;
        // Play stun particle effects?
    }

    // 
    private void VulnerableState() {
        attackTimer += Time.deltaTime;
        if (attackTimer < vulnerableTime) {
            return;
        }
        attackTimer = 0;
        state = State.CHASING;
    }

    private void FollowPathState() {
        // If we can see the player, exit and start chasing
        if (CanSeePlayer()) {
            state = State.CHASING;
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

    private void PathRandom() {
        if (batWaypointNodes.Count == 0) {
            Debug.LogError("BAT: No waypoints available.");
            return;
        }

        int randomWaypointIndex = UnityEngine.Random.Range(0, batWaypointNodes.Count);
        currentPath = batWaypointManager.BFS(batWaypointManager.GetClosestNode(transform.position), batWaypointNodes[randomWaypointIndex]).ToList<GameObject>();
        currentPathIndex = 0;
    }

    private void PathPlayer() {
        if (batWaypointNodes.Count == 0) {
            Debug.LogError("BAT: No waypoints available.");
            return;
        }

        currentPath = batWaypointManager.BFS(batWaypointManager.GetClosestNode(transform.position), batWaypointManager.GetClosestNode(player.position)).ToList<GameObject>();
        currentPathIndex = 0;
    }

    private void FaceTarget(Vector3 targetPos) {
        Vector3 dir = (targetPos - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        if (dir.sqrMagnitude < 0.0001f) return;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

    private bool CanSeePlayer() {
        Vector3 dir = player.gameObject.GetComponent<Collider>().bounds.center - transform.position;
        Vector3 scaledDir = dir.normalized * detectionRadius;
        
        RaycastHit raycastResult;
        Physics.Raycast(transform.position, scaledDir, out raycastResult, detectionRadius);
        
        Debug.DrawLine(transform.position, transform.position + scaledDir, Color.red);

        GameObject hitGameObject = raycastResult.collider.gameObject;
        if (hitGameObject.tag.Equals(playerTag) || hitGameObject.tag.Equals(playerPartTag)) return true;

        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        batWaypointManager = GameObject.FindGameObjectWithTag("BatWaypointManager").GetComponent<BatWaypointManager>();
        batWaypointNodes = batWaypointManager.batWaypointNodes;
        batCollider = GetComponent<CapsuleCollider>();
        batRigidBody = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag(playerTag).transform;
    }

    // Update is called once per frame
    void Update()
    {
        float gizmoHeight = 0.5f;
        Color batColor = Color.magenta;
        Debug.DrawLine(transform.position-Vector3.up*gizmoHeight/2, transform.position+Vector3.up*gizmoHeight/2, batColor);
        Debug.DrawLine(transform.position-Vector3.left*gizmoHeight/2, transform.position+Vector3.left*gizmoHeight/2, batColor);
        Debug.DrawLine(transform.position-Vector3.forward*gizmoHeight/2, transform.position+Vector3.forward*gizmoHeight/2, batColor);
        if (state == State.DEAD) {
            Destroy(gameObject);
        }
        else if (state == State.PATROLLING) {
            PatrolState();
        }
        else if (state == State.WINDING_BACK) {
            WindBackState();
        }
        else if (state == State.LUNGING) {
            LungeState();
        }
        else if (state == State.VULNERABLE) {
            VulnerableState();
        }
        else if (state == State.FOLLOWING_PATH) {
            FollowPathState();
        }
        else if (state == State.CHASING) {
            ChaseState();
        }
    }
}