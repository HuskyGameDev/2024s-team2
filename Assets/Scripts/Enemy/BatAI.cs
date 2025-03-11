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
    public bool debugPrint;
    public BatWaypointManager batWaypointManager;
    private List<GameObject> batWaypointNodes;

    private List<GameObject> currentPath;
    private int currentPathIndex = 0;

    public float patrolSpeed;
    public float chaseSpeed;
    public float rotationSpeed;

    private enum State { PATROLLING, CHASING, FOLLOWINGPATH, DEAD };
    [SerializeField] private State state = State.PATROLLING;

    // My idea for this is that it currently targets your belly, 
    // so it might be nice to have it target the head instead.
    public readonly Vector3 playerheadOffset = Vector3.up * 0.5f; 
    
    private const string playerPartTag = "PlayerPart";
    private const string playerTag = "Player";
    private Transform player;

    public float detectionRadius;
    public float waypointReachedDistance = 0.1f;

    private void PatrolState() {
        if (debugPrint) Debug.Log("BAT: Patrolling, setting new path");
        PathRandom();

        // Without this function call, we would stop for one frame before following the path.
        FollowPathState();

        state = State.FOLLOWINGPATH;
    }

    private void ChaseState() {
        if (!CanSeePlayer()) {
            PathPlayer();
            currentPathIndex = 0;
            state = State.FOLLOWINGPATH;
            if (debugPrint) Debug.Log("BAT: Player lost, pathing to last known node position: " + currentPath[currentPath.Count-1].ToString());
            return;
        }

        // Look and move towards the player
        FaceTarget(player.position);
        transform.position = Vector3.MoveTowards(transform.position, player.position+playerheadOffset, chaseSpeed * Time.deltaTime);
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
        else if (state == State.FOLLOWINGPATH) {
            FollowPathState();
        }
        else if (state == State.CHASING) {
            ChaseState();
        }
    }
}