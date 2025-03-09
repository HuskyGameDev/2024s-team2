using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BatAIOld : MonoBehaviour
{
    public BatWaypointManager batWaypointManager;
    private List<GameObject> batWaypointNodes;

    public float patrolSpeed;
    public float chaseSpeed;
    public float rotationSpeed;

    private enum State { PATROLLING, CHASING, DEAD };
    private State state = State.PATROLLING;

    private string playerPartTag = "PlayerPart";
    private string playerTag = "Player";
    private Transform player;

    public float detectionRadius;
    public float waypointReachedDistance = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        batWaypointManager = GameObject.FindGameObjectWithTag("BatWaypointManager").GetComponent<BatWaypointManager>();
        batWaypointNodes = batWaypointManager.batWaypointNodes;
        player = GameObject.FindWithTag(playerTag).transform;
        StartCoroutine(FiniteStateMachine());
        Debug.Log("BAT ONLINE!!!!");
    }

    IEnumerator FiniteStateMachine() {
        while (!batWaypointManager.isInstantiated)
            yield return null;
        batWaypointNodes = batWaypointManager.batWaypointNodes;
        while (true) {
            Debug.Log("BAT: Patrolling...");
            // yield return StartCoroutine(PatrollingState());
            yield return StartCoroutine(ChaseState());
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator PatrollingState() {
        state = State.PATROLLING;
        int randomWaypointIndex = UnityEngine.Random.Range(0, batWaypointNodes.Count);
        Debug.Log("BAT: Moving to wp" + randomWaypointIndex);

        yield return FollowPath(batWaypointNodes[randomWaypointIndex]);
        
        Debug.Log("BAT: Reached WP!");
    }

    private IEnumerator ChaseState() {
        if (!CanSeePlayer()) yield break;
        state = State.CHASING;
        while (CanSeePlayer()) {
            Vector3 lookVec = (player.position - transform.position).normalized;
            FaceTarget(lookVec);
            transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        // We have lost sight of the player, navigate to nearest WP
        yield return FollowPath(batWaypointManager.GetClosestNode(player.position));
    }

    private IEnumerator FollowPath(GameObject waypointDestination) {
        GameObject[] path = batWaypointManager.BFS(batWaypointManager.GetClosestNode(transform.position), waypointDestination);
        
        for (int i = 0; i < path.Length - 1; i++) {
            Transform nextNodeTransform;
            nextNodeTransform = path[i].transform;
            Vector3 lookVec = (nextNodeTransform.position - transform.position).normalized;
            while (Vector3.Distance(nextNodeTransform.position, transform.position) > waypointReachedDistance) {
                transform.position = Vector3.MoveTowards(transform.position, nextNodeTransform.position, patrolSpeed * Time.deltaTime);
                FaceTarget(lookVec);
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private void FaceTarget(Vector3 dir) {
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        if (dir.sqrMagnitude < 0.0001f) return;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }


    private bool CanSeePlayer() {
        Vector3 dir = player.gameObject.GetComponent<Collider>().bounds.center - transform.position;
        Vector3 scaledDir = dir.normalized * detectionRadius;
        // Vector3 scaledDir = Vector3.down * detectionRadius;
        
        RaycastHit raycastResult;
        Physics.Raycast(transform.position, scaledDir, out raycastResult, detectionRadius);
        
        Debug.DrawLine(transform.position, transform.position + scaledDir, Color.red);

        GameObject hitGameObject = raycastResult.collider.gameObject;
        if (hitGameObject.tag == playerTag || hitGameObject.tag == playerPartTag) return true;

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}